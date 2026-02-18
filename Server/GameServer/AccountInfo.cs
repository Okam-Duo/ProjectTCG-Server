using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.GameServer
{
    public struct AccountInfo
    {
        public readonly int userId;
        public readonly string nickName;

        public AccountInfo(int userId, string nickName)
        {
            this.userId = userId;
            this.nickName = nickName;
        }
    }
}
