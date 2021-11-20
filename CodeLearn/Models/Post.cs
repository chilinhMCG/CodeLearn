using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Models
{
    public class Post
    {
        public Post()
        {
            Id = new Guid();
            CreateAt = DateTime.Now;
        }
        [Column("id")]
        [Key]
        public Guid Id { get; set; }
        [Column("create_at")]
        public DateTime CreateAt { get; set; }
        [Column("last_updated")]
        public DateTime LastUpdated { get; set; }
        [Column("hashtag")]
        public List<string> HashTag { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("content")]
        public string Content { get; set; }
        [Column("user_id")]
        public Guid? UserId { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public User User { get; set; }
    }
}
