using System.ComponentModel;
using System.IO;
using System.Net.Sockets;
using ModelLib;
using Newtonsoft.Json;

namespace JsonClient
{
    public class Client
    {
        public void Start()
        {
            Car myCar = new Car("Tesla", "Green", "JsonCar4");
            Car myCar2 = new Car("Ford", "Blå", "JsonCar10");
            AutoSale dealer = new AutoSale("Jørgen", "Ebolavej 32");
            dealer.Cars.Add(myCar);
            dealer.Cars.Add(myCar2);

            using (TcpClient socket = new TcpClient("localhost", 10002))
            using (StreamWriter sw = new StreamWriter(socket.GetStream()))
            using (StreamReader sr = new StreamReader(socket.GetStream()))
            {
                string output = JsonConvert.SerializeObject(dealer);
                sw.WriteLine(output);
                sw.Flush();
            }
        }
    }
}