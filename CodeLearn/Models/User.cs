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
        [Column("is_blocked")]
        public bool IsBlocked { get; set; }
        public ICollection<CourseDetail> CourseDetails { get; set; }

        [Column("profile_picture_path", TypeName = "varchar(260)")]
        public string ProfilePicturePath { get; set; }

        public List<Post> Posts { get; set; }

        public List<Comment> Comments { get; set; }

        public List<PostRating> PostRatings { get; set; }

        public List<CommentStar> CommentStars { get; set; }
    }
}
