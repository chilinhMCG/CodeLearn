using CodeLearn.Data.Ordering;
using CodeLearn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Data.OrderingQuery
{
    public static class PostInfoUtils
    {
        public static OrderedOrderingQuery<PostInfo> CreatePostInfoOrderingQuery(
            PostOrderingOption orderingOption, OrderByOption orderByOption, OrderingQuery<PostInfo> orderingQuery)
        {
            var ordered = (orderingOption, orderByOption) switch
            {
                (PostOrderingOption.OverallRating, OrderByOption.Descending)
                    => orderingQuery.OrderByDescending(p => p.OverallRating),

                (PostOrderingOption.OverallRating, OrderByOption.Ascending)
                    => orderingQuery.OrderBy(p => p.OverallRating),

                (PostOrderingOption.DateCreated, OrderByOption.Descending)
                    => orderingQuery.OrderByDescending(p => p.DateCreated),

                (PostOrderingOption.DateCreated, OrderByOption.Ascending)
                    => orderingQuery.OrderBy(p => p.DateCreated),

                (PostOrderingOption.Ratings, OrderByOption.Descending)
                    => orderingQuery.OrderByDescending(p => p.RatingCount),

                (PostOrderingOption.Ratings, OrderByOption.Ascending)
                    => orderingQuery.OrderBy(p => p.RatingCount),

                (PostOrderingOption.Comments, OrderByOption.Descending)
                    => orderingQuery.OrderByDescending(p => p.CommentCount),

                (PostOrderingOption.Comments, OrderByOption.Ascending)
                    => orderingQuery.OrderBy(p => p.CommentCount),

                _ => throw new ArgumentOutOfRangeException(nameof(orderingOption), "Provided options are invalid."),
            };

            return ordered.ThenByDescending(p => p.DateCreated);
        }
    }
}
