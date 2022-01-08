using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Models
{
    //Information about post, without it's content  
    public class PostInfo
    {
        public Guid Id { get; set; }

        public string Author { get; set; }

        public Guid AuthorId { get; set; }

        public string Title { get; set; }

        public string Slug { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateLastEdited { get; set; }

        public float OverallRating { get; set; }

        public int RatingCount { get; set; }

        public int CommentCount { get; set; }
    }
}
