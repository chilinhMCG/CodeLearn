using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Models
{
    public class CourseType
    {
        [Column("id")]
        public Guid Id {get;set;}
        [Column("name")]
        public string Name { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
