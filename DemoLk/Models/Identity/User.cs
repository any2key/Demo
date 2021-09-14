using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoLk.Models.Identity
{
    public class User : IdentityUser
    {
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PassUI { get; set; }
        public string Base64Image { get; set; }
    }
}