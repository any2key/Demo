using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Web;

namespace DemoLk
{
    public class TcpClient
    {
        public string ip { get; set; }
        public int port { get; set; }

        System.Net.Sockets.TcpClient client;
        public TcpClient(string address)
        {
            var a=address.Split(':');
            ip = a[0];
            port = int.Parse(a[1]);
        }

        public string SendData<T>(T request)
        {
            try
            {

            client = new System.Net.Sockets.TcpClient(ip, port);
            NetworkStream stream = client.GetStream();
            var req = JsonConvert.SerializeObject(request);
            var data= Encoding.Unicode.GetBytes(req);
            stream.Write(data, 0, data.Length);
            data = new byte[256];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (stream.DataAvailable);

            return builder.ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                client.Close();
            }

        }

    }
}