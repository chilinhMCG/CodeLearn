using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Models
{
    public class User
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("email")]
        public string Email { get; set; }

        [Column("role")]
        public string Role { get; set; }

        [Column("is_blocked")]
        public bool IsBlocked { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Discussion> Discussions { get; set; }
        public ICollection<Post> Posts { get; set; }
        public User()
        {
            this.Posts = new HashSet<Post>();
            this.Discussions = new HashSet<Discussion>();
            this.Comments = new HashSet<Comment>();
        }
        public ICollection<Lesson> Lessons { get; set; }
    }
}
