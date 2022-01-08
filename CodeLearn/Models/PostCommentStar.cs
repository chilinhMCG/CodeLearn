using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Models
{
    public class PostCommentStar
    {
        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("comment_id")]
        public Guid CommentId { get; set; }

        public User User { get; set; }

        public PostComment Comment { get; set; }
    }
}
