using Shared.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.GameServer
{
    public class LobbyUser
    {
        public readonly AccountInfo accountInfo;
        private readonly ClientSession _session;

        public LobbyUser(ClientSession session, AccountInfo accountInfo)
        {
            this.accountInfo = accountInfo;
            _session = session;
            _session.ChangePacketHandler(new LobbyUserPacketHandler(this));
        }

        public void SendPacket(IPacket packet)
        {

        }
    }
}
