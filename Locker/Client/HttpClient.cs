using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Locker.Client
{
    class HttpClient
    {
        public static string Post<T>(T body, string url)
        {
            var json = JsonConvert.SerializeObject(body);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new System.Net.Http.HttpClient();
            var response = client.PostAsync(url, data).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        public static string Get(string url)
        {
            var client = new System.Net.Http.HttpClient();
            return client.GetStringAsync(url).Result;
        }
    }
}
