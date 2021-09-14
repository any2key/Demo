using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.Models.HttpRequest
{
    public class RegisterRequest
    {
        public string Address { get; set; }
        public string Hwid { get; set; }
    }
}
