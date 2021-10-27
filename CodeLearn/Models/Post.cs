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
        [Column("id")]
        public Guid Id { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Required]
        [Column("title")]
        public string Title { get; set; }

        [Column("slug", TypeName = "varchar(100)")]
        [Required]
        public string Slug { get; set; }

        [Required]
        [Column("content")]
        public string Content { get; set; }

        [Column("date_created")]
        public DateTime DateCreated { get; set; }

        [Column("date_last_edited")]
        public DateTime DateLastEdited { get; set; }


        public User User { get; set; }

        public List<Comment> Comments { get; set; }

        public List<PostRating> PostRatings { get; set; }
    }
}
