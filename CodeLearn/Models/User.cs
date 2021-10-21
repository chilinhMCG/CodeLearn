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
        public ICollection<Lesson> Lessons { get; set; }
    }
}
