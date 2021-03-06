﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using TreasureGuide.Entities;
using TreasureGuide.Entities.Helpers;
using TreasureGuide.Entities.Interfaces;
using TreasureGuide.Web.Services;
using System.Net;
using TreasureGuide.Common.Constants;
using TreasureGuide.Common.Helpers;
using TreasureGuide.Common.Models;
using TreasureGuide.Web.Helpers;

namespace TreasureGuide.Web.Controllers.API.Generic
{
    public class EntityApiController<TEntityKey, TEntity, TKey, TStubModel, TDetailModel, TEditorModel> : GenericApiController<TKey, TStubModel, TDetailModel, TEditorModel>
        where TEntity : class, IIdItem<TEntityKey>
        where TEditorModel : IIdItem<TKey>
    {
        protected readonly TreasureEntities DbContext;
        protected readonly IMapper AutoMapper;
        protected readonly IThrottleService ThrottlingService;

        public bool Throttled { get; set; } = true;

        public EntityApiController(TreasureEntities dbContext, IMapper autoMapper, IThrottleService throttlingService)
        {
            DbContext = dbContext;
            AutoMapper = autoMapper;
            ThrottlingService = throttlingService;
        }

        protected override async Task<IActionResult> Get<TModel>(TKey id = default(TKey), bool required = false)
        {
            var result = await PerformGet<TModel>(id, required);
            return result as IActionResult ?? Ok(result); ;
        }

        protected override async Task<IActionResult> Post(TEditorModel model, TKey id = default(TKey))
        {
            var result = await PerformPost(model, id);
            return result as IActionResult ?? Ok(result); ;
        }

        protected override async Task<IActionResult> Delete(TKey id = default(TKey))
        {
            var result = await PerformDelete(id);
            return result as IActionResult ?? Ok(result); ;
        }

        protected virtual async Task<object> PerformGet<TModel>(TKey id = default(TKey), bool required = false)
        {
            if (required && IsUnspecified(id))
            {
                return BadRequest("Must specify an Id.");
            }
            if (!CanGet(id))
            {
                return Unauthorized();
            }
            var entities = FetchEntities(id);
            var transformed = typeof(TModel) == typeof(TEntity) ? entities.Cast<TModel>() : Project<TModel>(entities);
            if (!IsUnspecified(id))
            {
                var single = await transformed.SingleOrDefaultAsync();
                if (single != null)
                {
                    single = await SingleGetTransform(single, id);
                    return single;
                }
                return BadRequest("Could not find item with Id '" + id + "'.");
            }
            return await transformed.ToListAsync();
        }

        protected virtual bool CanGet(TKey id)
        {
            return true;
        }

        protected virtual IQueryable<TEntity> FetchEntities(TKey id = default(TKey))
        {
            var queryable = DbContext.Set<TEntity>().AsQueryable();
            if (!IsUnspecified(id))
            {
                queryable = queryable.FindId(id);
            }
            queryable = Filter(queryable);
            return queryable;
        }

        protected virtual async Task<TModel> SingleGetTransform<TModel>(TModel single, TKey id = default(TKey))
        {
            if (typeof(ICanEdit).IsAssignableFrom(typeof(TModel)))
            {
                ((ICanEdit)single).CanEdit = CanPost(id);
            }
            return single;
        }

        protected virtual async Task<object> PerformPost(TEditorModel model, TKey id = default(TKey))
        {
            if (Throttled && !ThrottlingService.CanAccess(User, Request))
            {
                return StatusCode(429, ThrottleService.Message);
            }
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ConcatErrors();
                return BadRequest(errors);
            }
            id = DefaultIfUnspecified(id, model.Id);
            if (!CanPost(id))
            {
                return Unauthorized();
            }
            if (!IsUnspecified(id))
            {
                var entities = FetchEntities(id);
                var single = entities.SingleOrDefault();
                if (single != null)
                {
                    return await CreateOrUpdate(model, single);
                }
            }
            return await CreateOrUpdate(model);
        }

        protected virtual bool CanPost(TKey id)
        {
            return User.IsInRole(RoleConstants.Administrator);
        }

        protected virtual async Task<object> PerformDelete(TKey id)
        {
            if (!CanDelete(id))
            {
                return Unauthorized();
            }
            if (!IsUnspecified(id))
            {
                var entities = FetchEntities(id);
                var target = entities.SingleOrDefault();
                return await Remove(target);
            }
            return BadRequest("No item specified.");
        }

        protected virtual bool CanDelete(TKey id)
        {
            return User.IsInRole(RoleConstants.Administrator);
        }

        protected virtual async Task<object> CreateOrUpdate(TEditorModel model, TEntity entity = null)
        {
            model = await PreProcess(model);
            var newItem = entity == null;
            if (newItem)
            {
                entity = Create(model);
                DbContext.Set<TEntity>().Add(entity);
            }
            else
            {
                entity = Update(model, entity);
            }
            entity = await PostProcess(entity);
            await DbContext.SaveChangesSafe();
            return new IdResponse<TEntityKey> { Id = entity.Id };
        }

        protected virtual async Task<object> Remove(TEntity single)
        {
            var removed = DbContext.Set<TEntity>().Remove(single);
            if (removed != null)
            {
                await DbContext.SaveChangesSafe();
                return removed.Id;
            }
            return 1;
        }

        protected virtual IQueryable<TModel> Project<TModel>(IQueryable<TEntity> entities)
        {
            return entities.ProjectTo<TModel>(AutoMapper.ConfigurationProvider);
        }

        protected virtual TEntity Create(TEditorModel model)
        {
            return AutoMapper.Map<TEntity>(model);
        }

        protected virtual TEntity Update(TEditorModel model, TEntity entity)
        {
            return AutoMapper.Map(model, entity);
        }

        protected virtual IQueryable<TEntity> Filter(IQueryable<TEntity> entities)
        {
            return entities;
        }

        protected virtual async Task<TEditorModel> PreProcess(TEditorModel model)
        {
            return model;
        }

        protected virtual async Task<TEntity> PostProcess(TEntity entity)
        {
            return entity;
        }
    }
}
