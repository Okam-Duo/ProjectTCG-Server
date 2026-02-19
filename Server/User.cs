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
        private GameServer.GameServer _server;
        private DBConnection _db;
        private object _lock = new object();
        private bool _isClosed = false;

        public User(ClientSession session, AccountInfo accountInfo, GameServer.GameServer server)
        {
            this.accountInfo = accountInfo;
            _session = session;
            _session.ChangePacketHandler(new UserPacketHandler(this, server));
            _server = server;
            _db = server.db;
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

        public async Task<int> GetGold()
        {
            bool canRead = false;
            int gold = -1;

            await _db.RunSql("SELECT * FROM UserInventory WHERE userId = @id AND resourceId = 1;",
                (reader) =>
                {
                    canRead = reader.Read();

                    if (canRead)
                    {
                        gold = (int)reader["resourceCount"];
                    }
                },
                new() { { "@id", accountInfo.userId } });
            if (canRead)
            {
                return gold;
            }
            else
            {
                return 0;
            }
        }
    }
}
