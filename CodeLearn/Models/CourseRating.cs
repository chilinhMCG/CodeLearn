using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Models
{
    public class CourseRating
    {
        [Column("course_id")]
        public Guid CourseId { get; set; } 

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("total_rating")]
        public int TotalRating { get; set; }

        [Column("rate_count")]
        public int RateCount { get; set; }
    }
}
