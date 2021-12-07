using NpgsqlTypes;
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

        [Required(ErrorMessage = "Bắt buộc phải có tiêu đề bài viết.")]
        [Column("title")]
        [MaxLength(300, ErrorMessage = "Tiêu đề độ dài tối đa là 300 ký tự.")]
        public string Title { get; set; }

        [Column("slug")]
        [Required]
        public string Slug { get; set; }

        [Required]
        [Column("content")]
        public string Content { get; set; }

        [Column("date_created")]
        public DateTime DateCreated { get; set; }

        [Column("date_last_edited")]
        public DateTime DateLastEdited { get; set; }

        [Column("unaccented_title")]
        [MaxLength(300)]
        public string UnaccentedTitle { get; set; }
        
        [Column("unaccented_content")]
        public string UnaccentedContent { get; set; }

        [Column("hashtag")]
        public List<string> HashTag { get; set; }

        int NumberInteract { set; get; }

        public NpgsqlTsVector TitleSearchVector { get; set; }

        public NpgsqlTsVector ContentSearchVector { get; set; }

        public User User { get; set; }

        public List<PostComment> Comments { get; set; }

        public List<PostRating> PostRatings { get; set; }
    }
}
