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
        static AccountServer server;

        static void Main(string[] args)
        {
            StartServer();
        }

        static void StartServer()
        {
            Console.WriteLine("ProjectTCG-Server Program\n\n============\n\n");

            Shared.Logger.OnAddLogData += Console.WriteLine;

            server = new AccountServer();

            while (true)
            {
            }
        }
    }
}