using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using ModelLib;
using Newtonsoft.Json;

namespace JsonServer
{
    public class Server
    {
        public void Start()
        {
            TcpListener server = new TcpListener(IPAddress.Loopback, 10002);
            server.Start();

            while (true)
            {
                TcpClient socket = server.AcceptTcpClient();
                Task.Run(() =>
                {
                    TcpClient tmpSocket = socket;
                    DoClient(tmpSocket);
                });
            }
        }

        public void DoClient(TcpClient socket)
        {
            using (StreamWriter sw = new StreamWriter(socket.GetStream()))
            using (StreamReader sr = new StreamReader(socket.GetStream()))
            {
                string line = sr.ReadLine();
                Console.WriteLine(line);
                AutoSale dealer = JsonConvert.DeserializeObject<AutoSale>(line);

                Console.WriteLine(dealer);
            }
        }
    }
}