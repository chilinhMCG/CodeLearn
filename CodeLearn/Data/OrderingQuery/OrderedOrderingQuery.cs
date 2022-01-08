using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodeLearn.Data.OrderingQuery
{
    public class OrderedOrderingQuery<TSource>
    {
        private readonly IOrderedQueryable<TSource> _query;

        public OrderedOrderingQuery(IOrderedQueryable<TSource> query)
        {
            _query = query;
        }

        public OrderedOrderingQuery<TSource> ThenBy<TKey>(Expression<Func<TSource, TKey>> keySelector)
            => new(_query.ThenBy(keySelector));

        public OrderedOrderingQuery<TSource> ThenByDescending<TKey>(Expression<Func<TSource, TKey>> keySelector)
            => new(_query.ThenByDescending(keySelector));

        public IOrderedQueryable<TSource> GetInnerQuery() => _query;
    }
}
