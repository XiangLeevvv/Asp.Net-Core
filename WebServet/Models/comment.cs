using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServet.Models
{
    public class comment
    {
        [Key]
        public int comment_id { set; get; }
        public int original_id { set; get; }
        [ForeignKey("original_id")]
        public post post { set; get; }
        public int actor_id { set; get; }
        [ForeignKey("actor_id")]
        public User User_acotr { set; get; }
        public DateTime time { set; get; }
        public string content { set; get; }
        public int at_id { set; get; }
        [ForeignKey("at_id")]
        public User User_at { set; get; }
    }
}
