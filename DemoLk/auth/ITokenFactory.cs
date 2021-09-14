using DemoLk.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoLk.auth
{
    public interface ITokenFactory
    {
        string GenerateToken(Token token);
        Token DecodeToken(string source);
    }
}