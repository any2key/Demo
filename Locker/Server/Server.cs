using Locker.Helpers;
using Locker.Models.TcpRequest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Locker.Server
{
    public class Server
    {

        public static Server instance;

        public delegate void Unlock(int seconds);
        public event Unlock Unlocked;

        public delegate void Lock();
        public event Lock Locked;

        private Server()
        {
            Thread thread = new Thread(Listen);
            thread.Start();
        }

        public static Server getServer()
        {
            if (instance == null)
                instance = new Server();
            return instance;
        }


        public void Listen()
        {
            int port = 7887;
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(Helper.getLocalIp()), port);
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenSocket.Bind(ipPoint);
            listenSocket.Listen(10);
            while (true)
            {
                Socket handler = listenSocket.Accept();
                StringBuilder builder = new StringBuilder();
                int bytes = 0; // количество полученных байтов
                byte[] data = new byte[256]; // буфер для получаемых данных

                do
                {
                    bytes = handler.Receive(data);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }

                while (handler.Available > 0);

                TcpRequest req = JsonConvert.DeserializeObject<TcpRequest>(builder.ToString());

                switch (req.Method.ToLower())
                {
                    case Constants.LOCK:
                        Locked?.Invoke();
                        break;
                    case Constants.UNLOCK:
                        Unlocked?.Invoke(req.Seconds);
                        break;
                    default: break;
                }


                var response = "";
                data = Encoding.Unicode.GetBytes(response);
                handler.Send(data);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
        }
    }
}
