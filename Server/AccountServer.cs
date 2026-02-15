using Shared.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class AccountServer
    {
        private Listener<ClientSession> _listener;
        private AccountServerPacketHandler _packetHandler;

        public AccountServer()
        {
            //DNS (Domain Name System)
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);


            _packetHandler = new AccountServerPacketHandler(this);
            _listener = new(
                endPoint,
                sessionFactory: () => { return new ClientSession(_packetHandler); },
                onAcceptAsync: OnAcceptAsync
                );


            _listener.StartListen();

            Console.WriteLine($"{nameof(AccountServer)} is constructed!");
        }

        private async void OnAcceptAsync(ClientSession session)
        {
            
        }
    }
}
