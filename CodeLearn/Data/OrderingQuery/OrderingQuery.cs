using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CodeLearn.Data.OrderingQuery
{
    public class OrderingQuery<TSource>
    {
        private readonly IQueryable<TSource> _query;

        public OrderingQuery(IQueryable<TSource> query)
        {
            _query = query;
        }

        public OrderedOrderingQuery<TSource> OrderBy<TKey>(Expression<Func<TSource, TKey>> keySelector)
            => new(_query.OrderBy(keySelector));

        public OrderedOrderingQuery<TSource> OrderByDescending<TKey>(Expression<Func<TSource, TKey>> keySelector)
            => new(_query.OrderByDescending(keySelector));

    }
}
