﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TreasureGuide.Entities.Helpers
{
    public static class EnumerableHelper
    {
        public static readonly IEnumerable<UnitFlag> PayToPlay = new[]
        {
            UnitFlag.RareRecruitExclusive,
            UnitFlag.RareRecruitLimited
        };

        public static IQueryable<TEntity> FindId<TNullable, TEntity>(this IQueryable<TEntity> queryable, TNullable id = default(TNullable))
        {
            var arg = Expression.Parameter(typeof(TEntity), "i");
            var predicate =
                Expression.Lambda<Func<TEntity, bool>>(
                    Expression.Equal(
                        Expression.Property(arg, "Id"),
                        Expression.Constant(id)),
                    arg);
            return queryable.Where(predicate);
        }

        public static IOrderedQueryable<T> OrderBy<T, TKey>(this IQueryable<T> queryable, Expression<Func<T, TKey>> selector, bool desc)
        {
            if (desc)
            {
                return queryable.OrderByDescending(selector);
            }
            return queryable.OrderBy(selector);
        }

        public static IOrderedQueryable<T> ThenBy<T, TKey>(this IOrderedQueryable<T> queryable, Expression<Func<T, TKey>> selector, bool desc)
        {
            if (desc)
            {
                return queryable.ThenByDescending(selector);
            }
            return queryable.ThenBy(selector);
        }

        public static TValue SafeGet<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
        {
            if (dict.ContainsKey(key))
            {
                return dict[key];
            }
            return default(TValue);
        }

        private static readonly Regex AlphaNumericRegex = new Regex(@"/[^\w\d]/");
        private static readonly char[] Splitters = { ' ' };

        public static IEnumerable<string> SplitSearchTerms(this string term)
        {
            return AlphaNumericRegex.Replace(term, " ").Split(Splitters, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
