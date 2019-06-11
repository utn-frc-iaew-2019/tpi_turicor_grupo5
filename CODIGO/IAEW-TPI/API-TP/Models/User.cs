using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_TP.Models
{
    public class User
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}