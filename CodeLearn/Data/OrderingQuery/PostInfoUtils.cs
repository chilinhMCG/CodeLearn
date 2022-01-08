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
        //tạo query sắp xếp bài viết dựa trên lựa chọn của người dùng
        public static OrderedOrderingQuery<PostInfo> CreatePostInfoOrderingQuery(
            PostOrderByOption orderingOption, OrderOption orderByOption, OrderingQuery<PostInfo> orderingQuery)
        {
            var ordered = (orderingOption, orderByOption) switch
            {
                (PostOrderByOption.OverallRating, OrderOption.Descending)
                    => orderingQuery.OrderByDescending(p => p.OverallRating),

                (PostOrderByOption.OverallRating, OrderOption.Ascending)
                    => orderingQuery.OrderBy(p => p.OverallRating),

                (PostOrderByOption.DateCreated, OrderOption.Descending)
                    => orderingQuery.OrderByDescending(p => p.DateCreated),

                (PostOrderByOption.DateCreated, OrderOption.Ascending)
                    => orderingQuery.OrderBy(p => p.DateCreated),

                (PostOrderByOption.Ratings, OrderOption.Descending)
                    => orderingQuery.OrderByDescending(p => p.RatingCount),

                (PostOrderByOption.Ratings, OrderOption.Ascending)
                    => orderingQuery.OrderBy(p => p.RatingCount),

                (PostOrderByOption.Comments, OrderOption.Descending)
                    => orderingQuery.OrderByDescending(p => p.CommentCount),

                (PostOrderByOption.Comments, OrderOption.Ascending)
                    => orderingQuery.OrderBy(p => p.CommentCount),

                _ => throw new ArgumentOutOfRangeException(nameof(orderingOption), "Provided options are invalid."),
            };

            return ordered.ThenByDescending(p => p.DateCreated);
        }
    }
}
