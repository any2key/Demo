using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoLk.Models.Request
{
    public class Request
    {
    }
    public class LoginRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
    public class RefreshRequest
    {
        public string Refresh { get; set; }
    }
    public class AddOrUpdateReq<T>
    {
        public AddOrUpdate Sign { get; set; }
        public T Data { get; set; }
    }

    public enum AddOrUpdate
    {
        Add,
        Update
    }

    public class RegisterRequest
    {
        public string Address { get; set; }
        public string Hwid { get; set; }
    }

    public class TcpRequest
    {

        /// <summary>
        /// Lock,Unlock
        /// </summary>
        public string Method { get; set; }
        public int Seconds { get; set; }
    }
}