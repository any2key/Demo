using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.Models.Settings
{
   public class Settings
    {
        public string Server { get; set; }
        public Settings()
        {
            Server = "http://localhost:8778";
        }
    }
}
