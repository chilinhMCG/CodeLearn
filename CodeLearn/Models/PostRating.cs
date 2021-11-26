using CodeLearn.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ManageForum.Api.Entities
{
    public class PostRating
    {
        [Key]
        [Column("id")]
        public Guid Id { set; get; }
        [Column("post_id")]
        public Guid PostId { set; get; }
        [ForeignKey(name: "PostId")]
        public Post Post { set; get; }
        [Column("user_id")]
        public Guid UserId { set; get; }
        [ForeignKey(name: "UserId")]
        public User User { set; get; }
        [Column("is_liked")]
        public bool IsLiked { set; get; }

    }
}
