using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Models
{
    public class CourseDetail
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("course_id")]
        public Guid CourseId { get; set; }
        [Column("creator_id")]
        public Guid CreatorId { get; set; }
        public Course CourseNavigation { get; set; }
        public User UserNavigation { get; set; }
    }
}
