using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Models
{
    public class PostComment
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("post_id")]
        public Guid PostId { get; set; }

        [Column("parent_comment_id")]
        public Guid? ParentCommentId { get; set; }

        [Column("content")]
        public string Content { get; set; }

        [Column("date_created")]
        public DateTime DateCreated { get; set; }

        [Column("date_last_edited")]
        public DateTime DateLastEdited { get; set; }

        public Post Post { get; set; }

        public PostComment ParentComment { get; set; }

        public List<PostComment> Replies { get; set; }

        public User User { get; set; }
    }
}
