using DemoLk.auth;
using DemoLk.Context;
using DemoLk.Models.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace DemoLk.Controllers
{
    public class ControllerBaseEx: ApiController
    {
        public AppDbContext db = new AppDbContext();
        public TokenFactory tokenFactory = new TokenFactory();
        protected HttpResponseMessage SafeRun(Func<string, object> action)
        {
            try
            {
                var req = Request;
                var resp = action(User.Identity.Name);

                var response = new HttpResponseMessage();
                response.Content = new StringContent(JsonConvert.SerializeObject(resp), Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(Response.BadResponse(ex.Message)), Encoding.UTF8),
                };
            }
        }
    }
}