using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Models
{
    public class Discussion
    {
        public Discussion()
        {
            Id = new Guid();
            CreateOn = DateTime.Now;
        }
        [Column("id")]
        [Key]
        public Guid Id { get; set; }
        [Column("create_on")]
        public DateTime CreateOn { get; set; }
        [Column("hashtag")]
        public List<string> HashTag { get; set; }
        [Column("question")]
        public string Question { get; set; }
        [Column("content")]
        public string Content { get; set; }
        [Column("user_id")]
        public Guid? UserId { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public User User { get; set; }

    }
}
