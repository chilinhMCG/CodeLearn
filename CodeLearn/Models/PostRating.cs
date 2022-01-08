using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Models
{
    public class PostRating
    {
        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("post_id")]
        public Guid PostId { get; set; }

        [Column("value", TypeName = "smallint")]
        [Range(minimum: 1, maximum: 5)]
        public int Value { get; set; }

        public User User { get; set; }

        public Post Post { get; set; }
    }
}
