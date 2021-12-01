using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Data.Ordering
{
    public static class PostOrderingOptionUtils
    {
        public static string ToUserFriendlyText(this PostOrderingOption option)
        {
            return option switch
            {
                PostOrderingOption.Relevance => "Độ phù hợp",
                PostOrderingOption.OverallRating => "Đánh giá sao",
                PostOrderingOption.DateCreated => "Ngày đăng",
                PostOrderingOption.Ratings => "Số lượng đánh giá sao",
                PostOrderingOption.Comments => "Số lượng bình luận",
                _ => throw new ArgumentOutOfRangeException(nameof(option), "Provided option is invalid."),
            };
        }
    }
}
