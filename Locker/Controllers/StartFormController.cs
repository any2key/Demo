using Locker.Client;
using Locker.Helpers;
using Locker.Models.HttpRequest;
using Locker.Models.Response;
using Locker.Models.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.Controllers
{
    public class StartFormController
    {
        string f_path = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "locker");

        public Settings GetSettings()
        {
            var path = Path.Combine(f_path, Constants.SETTING_FILE);
            CheckFolder();
            if (!File.Exists(path))
                File.WriteAllText(path, Crypto.Crypto.EncryptStringAES(new Settings().ToXML(), Constants.SECRET));

            var settings = Helper.FromXml<Settings>(Crypto.Crypto.DecryptStringAES(File.ReadAllText(path), Constants.SECRET));
            return settings;
        }

        public void SaveSettings(Settings s)
        {
            var path = Path.Combine(f_path, Constants.SETTING_FILE);
            CheckFolder();
            File.WriteAllText(path, Crypto.Crypto.EncryptStringAES(s.ToXML(), Constants.SECRET));
        }


        private void CheckFolder()
        {
            if (!Directory.Exists(f_path))
                Directory.CreateDirectory(f_path);
        }

        public Response Register()
        {
            try
            {

            RegisterRequest req = new RegisterRequest()
            {
                Address = $"{Helper.getLocalIp()}:7887",
                Hwid = Helper.GetHwid()
            };
                var server = GetSettings().Server;
                var url = $"{server}/api/workstation/register";
            var res = JsonConvert.DeserializeObject<Response>(HttpClient.Post<RegisterRequest>(req, url));
            return res;
            }
            catch(Exception ex)
            {
                return Response.BadResponse(ex.Message);
            }
        }
    }
}
