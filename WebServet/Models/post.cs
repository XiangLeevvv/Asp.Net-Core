using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServet.Models
{
    public class post
    {
        [Key]
        public int post_id { set; get; }
        public int user_id { set; get; }
        [ForeignKey("user_id")]
        public User User { set; get; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime time { set; get; }
        public int delete_flag { set; get; }
        public string content { set; get; }
    }
}
