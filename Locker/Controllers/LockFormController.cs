using Locker.Helpers;
using Locker.Models.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.Controllers
{
    class LockFormController
    {
        /*
         * По-хорошему, это все, чтобы не дублировать надо вынести в отдельную сущность, но пока нет времени, дедлайн
         * */
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

        private void CheckFolder()
        {
            if (!Directory.Exists(f_path))
                Directory.CreateDirectory(f_path);
        }
    }
}
