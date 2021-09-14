using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoLk.Models.Response
{
    public class Response
    {
        public Response()
        {

        }
        public static ErrorResponse BadResponse(string error)
        {
            return new ErrorResponse() { IsOk = false, Message = error };
        }

        public static Response OK = new Response() { IsOk = true };
        public static ErrorResponse Forbidden = new ErrorResponse() { IsOk = false, Message = "Недостаточно прав для совершения этой операции" };
        public bool IsOk { get; set; }
    }

    public class ErrorResponse : Response
    {
        public string Message { get; set; }
    }

    public class DataResponse<T> : Response
    {
        public T Data { get; set; }
    }

    public class LoginResponse
    {
        public string Token { get; set; }
        public string Role { get; set; }
        public string User { get; set; }
        public string RefreshToken { get; set; }
    }
}