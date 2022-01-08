using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Models
{
    public class PostCommentInfo
    {
        public PostComment Comment { get; set; }

        public string UserName { get; set; }

        public int StarCount { get; set; }

        public int ReplyCount { get; set; }
    }
}
