using CodeLearn.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ManageForum.Api.Entities
{
    public class DiscussionCreact
    {
        [Key]
        [Column("id")]
        public Guid Id { set; get; }
        [Column("discussion_id")]
        public Guid DiscussionId { set; get; }
        [ForeignKey(name: "DiscussionId")]
        public Discussion Discussion { set; get; }
        [Column("user_id")]
        public Guid UserId { set; get; }
        [ForeignKey(name: "UserId")]
        public User User { set; get; }
        [Column("is_like")]
        public bool IsLike { set; get; }

    }
}
