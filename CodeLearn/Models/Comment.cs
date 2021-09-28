using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Models
{
    public class Comment
    {
        public Comment()
        {
            Id = new Guid();
            CreateOn = DateTime.Now;
        }
        [Column("id")]
        [Key]
        public Guid Id { get; set; }
        [Column("create_on")]
        public DateTime CreateOn { get; set; }
        [Column("content")]
        public string Content { get; set; }
        [Column("user_id")]
        public Guid? UserId { get; set; }
        [Column("DiscussionId")]
        public Guid? DiscussionId { get; set; }
        public User User { get; set; }
        public Discussion Discussion { get; set; }
    }
}
