using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServet.Models
{
    public class relation
    {
        [Key]
        public int relation_id { set; get; }
        public int actor_id { get; set; }
        [ForeignKey("actor_id")]
        public User User_actor { set; get; }
        public int object_id { set; get; }
        [ForeignKey("object_id")]
        public User User_object { set; get; }
    }
}
