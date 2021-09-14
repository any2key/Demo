using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoLk.Models.Identity
{
    public class Token
    {
        public DateTime Expired { get; set; }
        public string User { get; set; }
        public string Role { get; set; }
    }
}