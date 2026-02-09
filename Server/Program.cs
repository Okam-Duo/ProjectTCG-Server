using Shared.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static Listener _listener = new();

        static void Main(string[] args)
        {
            StartServer();
        }

        static void StartServer()
        {
            Console.WriteLine("ProjectTCG-Server Program\n\n============\n\n");

            Shared.Logger.OnAddLogData += Console.WriteLine;

            //DNS (Domain Name System)
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);


            _listener.Init(endPoint, () => { return new ClientSession(); });
            while (true)
            {
            }
        }
    }
}