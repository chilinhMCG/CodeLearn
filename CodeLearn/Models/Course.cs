using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Models
{
    public class Course
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("thumbnail")]
        public string Thumbnail { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("author")]
        public string Author { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("status")]
        public CourseStatusEnum Status { get; set; }
        [Column("course_type_id")]
        public Guid CourseTypeId { get; set; }
        public CourseType CourseTypeNavigation { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
    public enum CourseStatusEnum
    {
        Free = 0,
        Block = 1,
        Updating = 2,
        Cancel = 3
    }
}
