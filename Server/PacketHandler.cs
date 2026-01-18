using Shared.Network;
using Shared.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contents
{
    public class PacketHandler : IPacketHandler
    {
        public static PacketHandler Instance = new();

        public void C_BuyShopItemReq_Handle(Session session, C_BuyShopItemReq packet)
        {
            throw new NotImplementedException();
        }

        public void C_ResourceInfoReq_Handle(Session session, C_ResourceInfoReq packet)
        {
            throw new NotImplementedException();
        }

        public void C_ResourceInfoReq_Handler(Session session, C_ResourceInfoReq packet)
        {
            throw new NotImplementedException();
        }

        public void C_ShopInfoReq_Handle(Session session, C_ShopInfoReq packet)
        {
            throw new NotImplementedException();
        }

        public void S_BuyShopItemRes_Handle(Session session, S_BuyShopItemRes packet)
        {
            throw new NotImplementedException();
        }

        public void S_ResourceInfoRes_Handle(Session session, S_ResourceInfoRes packet)
        {
            throw new NotImplementedException();
        }

        public void S_ShopInfoRes_Handle(Session session, S_ShopInfoRes packet)
        {
            throw new NotImplementedException();
        }
    }
}
