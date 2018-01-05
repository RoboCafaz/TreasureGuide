﻿using System.Linq;
using AutoMapper;
using TreasureGuide.Entities;
using TreasureGuide.Web.Models.ProfileModels;
using TreasureGuide.Web.Models.ShipModels;
using TreasureGuide.Web.Models.StageModels;
using TreasureGuide.Web.Models.TeamModels;
using TreasureGuide.Web.Models.UnitModels;

namespace TreasureGuide.Web.Configurations
{
    public static class MapperConfig
    {
        private const string DefaultSubmitterName = "Monkey D. Luffy";

        public static IMapper Create()
        {
            var config = new MapperConfiguration(mapper =>
            {
                var ship = mapper.CreateControllerMapping<Ship, ShipDetailModel, ShipStubModel, ShipEditorModel>();
                var unit = mapper.CreateControllerMapping<Unit, UnitDetailModel, UnitStubModel, UnitEditorModel>();

                var teamUnit = mapper.CreateControllerMapping<TeamUnit, TeamUnitDetailModel, TeamUnitStubModel, TeamUnitEditorModel>();
                var teamSocket = mapper.CreateControllerMapping<TeamSocket, TeamSocketDetailModel, TeamSocketStubModel, TeamSocketEditorModel>();

                var team = mapper.CreateControllerMapping<Team, TeamDetailModel, TeamStubModel, TeamEditorModel>();
                team.DetailMapping.ForMember(x => x.Global, o => o.MapFrom(y => y.TeamUnits.All(z => z.Unit.Flags.HasFlag(UnitFlag.Global))));
                team.DetailMapping.ForMember(x => x.SubmittedByName, o => o.MapFrom(y => y.SubmittingUser != null ? y.SubmittingUser.UserName : DefaultSubmitterName));
                team.DetailMapping.ForMember(x => x.EditedByName, o => o.MapFrom(y => y.EditingUser != null ? y.EditingUser.UserName : DefaultSubmitterName));
                team.DetailMapping.ForMember(x => x.Score, o => o.MapFrom(y => y.TeamVotes.Count));
                team.DetailMapping.ForMember(x => x.CanEdit, o => o.Ignore());

                team.StubMapping.ForMember(x => x.Global, o => o.MapFrom(y => y.TeamUnits.All(z => z.Unit.Flags.HasFlag(UnitFlag.Global))));
                team.StubMapping.ForMember(x => x.SubmittedByName, o => o.MapFrom(y => y.SubmittingUser != null ? y.SubmittingUser.UserName : DefaultSubmitterName));
                team.StubMapping.ForMember(x => x.Score, o => o.MapFrom(y => y.TeamVotes.Count));

                var stage = mapper.CreateControllerMapping<Stage, StageDetailModel, StageStubModel, StageEditorModel>();
                stage.StubMapping.ForMember(x => x.TeamCount, o => o.MapFrom(y => y.Teams.Count));

                var user = mapper.CreateControllerMapping<UserProfile, ProfileDetailModel, ProfileStubModel, ProfileEditorModel>();
                user.EntityMapping.ForMember(x => x.UserRoles, o => o.Ignore()); // Handle this manually.
                user.EntityMapping.ForMember(x => x.UserName, o => o.Ignore()); // Don't allow thise to be changed.

                user.StubMapping.ForMember(x => x.TeamCount, o => o.MapFrom(y => y.SubmittedTeams.Count));

                user.DetailMapping.ForMember(x => x.TeamCount, o => o.MapFrom(y => y.SubmittedTeams.Count));
                user.DetailMapping.ForMember(x => x.UserRoles, o => o.MapFrom(y => y.UserRoles.Select(z => z.Name)));
                user.DetailMapping.ForMember(x => x.CanEdit, o => o.Ignore());

                user.EditorMapping.ForMember(x => x.UserRoles, o => o.MapFrom(y => y.UserRoles.Select(z => z.Name)));
            });
            config.AssertConfigurationIsValid();
            return config.CreateMapper();
        }

        private static MapperSet<TEntity, TDetailModel, TStubModel, TEditorModel> CreateControllerMapping<TEntity, TDetailModel, TStubModel, TEditorModel>(this IMapperConfigurationExpression config)
        {
            return new MapperSet<TEntity, TDetailModel, TStubModel, TEditorModel>
            {
                StubMapping = config.CreateMap<TEntity, TStubModel>(MemberList.Destination),
                DetailMapping = config.CreateMap<TEntity, TDetailModel>(MemberList.Destination),
                EditorMapping = config.CreateMap<TEntity, TEditorModel>(MemberList.Destination),
                EntityMapping = config.CreateMap<TEditorModel, TEntity>(MemberList.Source)
            };
        }

        private struct MapperSet<TEntity, TDetailModel, TStubModel, TEditorModel>
        {
            public IMappingExpression<TEntity, TStubModel> StubMapping { get; set; }
            public IMappingExpression<TEntity, TDetailModel> DetailMapping { get; set; }
            public IMappingExpression<TEntity, TEditorModel> EditorMapping { get; set; }
            public IMappingExpression<TEditorModel, TEntity> EntityMapping { get; set; }
        }
    }
}
