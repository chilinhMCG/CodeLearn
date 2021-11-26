using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Models
{
    public class Comment : ICloneable
    {
        public Comment()
        {
            Id = new Guid();
            CreateAt = DateTime.Now;
        }
        [Column("id")]
        [Key]
        public Guid Id { get; set; }
        [Column("create_at")]
        public DateTime CreateAt { get; set; }
        [Column("content")]
        public string Content { get; set; }
        [Column("user_id")]
        public Guid? UserId { get; set; }
        [Column("discussion_id")]
        public Guid? DiscussionId { get; set; }
        [Column("post_id")]
        public Guid? PostId { get; set; }
        public User User { get; set; }
        public Discussion Discussion { get; set; }
        public Post Post { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public object DeepCopy()
        {
            object other = (object)this.MemberwiseClone();
            return other;
        }
    }
}
