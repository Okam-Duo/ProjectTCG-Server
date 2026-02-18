using Server;
using Server.GameServer;
using Shared.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class User
    {
        public readonly AccountInfo accountInfo;
        private readonly ClientSession _session;
        private object _lock = new object();
        private bool _isClosed = false;
        private GameServer.GameServer _server;

        public User(ClientSession session, AccountInfo accountInfo, GameServer.GameServer server)
        {
            this.accountInfo = accountInfo;
            _session = session;
            _session.ChangePacketHandler(new UserPacketHandler(this, server));
            _server = server;
        }

        public void SendPacket(ArraySegment<byte> data)
        {
            _session.Send(data);
        }

        public bool TryCloseSession()
        {
            lock (_lock)
            {
                if (_isClosed) return false;
                _session.Disconnect();
                _isClosed = true;
            }
            return true;
        }

        public ClientSession Logout()
        {
            lock (_lock)
            {
                if (_isClosed) return _session;
                _isClosed = true;
            }
            _session.ChangePacketHandler(null);
            _server.TryRemoveUserSession(this);


            return _session;
        }
    }
}
