using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServet.Models
{
    public class likes
    {
        public int user_id { set; get; }
        [ForeignKey("user_id")]
        public User User { set; get; }
        public int post_id { set; get; }
        [ForeignKey("post_id")]
        public post post { set; get; }
    }
}
