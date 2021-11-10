using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Data.OrderingQuery
{
    public delegate OrderedOrderingQuery<T> OrderingQueryDelegate<T>(OrderingQuery<T> orderingOptions);
}
