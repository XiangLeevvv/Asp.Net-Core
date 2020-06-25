using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebServet.Models
{
    public class User
    {
        [Key]
        public int user_id { set; get; }
        public string user_name { set; get; }
        public string password { set; get; }
        public string portrait { set; get; }
    }
}
