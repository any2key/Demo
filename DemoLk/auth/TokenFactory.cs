using DemoLk.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoLk.auth
{
    public class TokenFactory : ITokenFactory
    {
        public static string secret = "Let_the_force_be_with_you";
        public Token DecodeToken(string source)
        {
            var decrypt = Crypto.Crypto.DecryptStringAES(source, secret);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Token>(decrypt);
        }

        public string GenerateToken(Token token)
        {
            var source = Newtonsoft.Json.JsonConvert.SerializeObject(token);
            return Crypto.Crypto.EncryptStringAES(source, secret);
        }
    }
}