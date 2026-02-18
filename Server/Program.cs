using Shared.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Server.GameServer;

namespace Server
{

    class Program
    {
        private static AccountServer accountServer;
        private static GameServer.GameServer gameServer;

        static void Main(string[] args)
        {
            StartServer();
        }

        static void StartServer()
        {
            Console.WriteLine("ProjectTCG-Server Program\n\n============\n\n");

            Shared.Logger.OnAddLogData += Console.WriteLine;

            gameServer = new GameServer.GameServer();
            accountServer = new AccountServer((accountInfo, session) => { gameServer.TryAddUserSession(accountInfo, session); });

            while (true)
            {
            }
        }
    }
}