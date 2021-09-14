using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.Models.TcpRequest
{
    public class TcpRequest
    {

        /// <summary>
        /// Lock,Unlock
        /// </summary>
        public string Method { get; set; }
        public int Seconds { get; set; }
    }
}
