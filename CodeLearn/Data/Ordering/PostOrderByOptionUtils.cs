using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Data.Ordering
{
    public static class PostOrderByOptionUtils
    {
        public static string ToUserFriendlyText(this PostOrderByOption option)
        {
            return option switch
            {
                PostOrderByOption.Relevance => "Độ phù hợp",
                PostOrderByOption.OverallRating => "Đánh giá sao",
                PostOrderByOption.DateCreated => "Ngày đăng",
                PostOrderByOption.Ratings => "Số lượng đánh giá sao",
                PostOrderByOption.Comments => "Số lượng bình luận",
                _ => throw new ArgumentOutOfRangeException(nameof(option), "Provided option is invalid."),
            };
        }

        public static string ToUrlQueryValue(this PostOrderByOption option)
        {
            return option switch
            {
                PostOrderByOption.Relevance => "Relevance",
                PostOrderByOption.OverallRating => "OverallRating",
                PostOrderByOption.DateCreated => "DateCreated",
                PostOrderByOption.Ratings => "Ratings",
                PostOrderByOption.Comments => "Comments",
                _ => throw new ArgumentOutOfRangeException(nameof(option), "Provided option is invalid."),
            };
        }

        public static PostOrderByOption ConvertFromUrlQueryParamValue(string value)
        {
            return value switch
            {
                "Relevance" => PostOrderByOption.Relevance,
                "OverallRating" => PostOrderByOption.OverallRating,
                "DateCreated" => PostOrderByOption.DateCreated,
                "Ratings" => PostOrderByOption.Ratings,
                "Comments" => PostOrderByOption.Comments,
                _ => throw new ArgumentOutOfRangeException(nameof(value), "Provided query value is invalid."),
            };
        }
    }
}
