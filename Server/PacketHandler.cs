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

        public void C_CheckIdAvailableReq_Handle(Session session, C_CheckIdAvailableReq packet)
        {
            throw new NotImplementedException();
        }

        public void C_ConnectServerReq_Handle(Session session, C_ConnectServerReq packet)
        {
            throw new NotImplementedException();
        }

        public void C_CurrencyInfoReq_Handle(Session session, C_CurrencyInfoReq packet)
        {
            throw new NotImplementedException();
        }

        public void C_DeckEditReq_Handle(Session session, C_DeckEditReq packet)
        {
            throw new NotImplementedException();
        }

        public void C_DeckInfoReq_Handle(Session session, C_DeckInfoReq packet)
        {
            throw new NotImplementedException();
        }

        public void C_LoginReq_Handle(Session session, C_LoginReq packet)
        {
            throw new NotImplementedException();
        }

        public void C_LogoutReq_Handle(Session session, C_LogoutReq packet)
        {
            throw new NotImplementedException();
        }

        public void C_ShopInfoReq_Handle(Session session, C_ShopInfoReq packet)
        {
            throw new NotImplementedException();
        }

        public void C_SignInReq_Handle(Session session, C_SignInReq packet)
        {
            throw new NotImplementedException();
        }

        public void C_SurrenderReq_Handle(Session session, C_SurrenderReq packet)
        {
            throw new NotImplementedException();
        }

        public void C_TryMatchingReq_Handle(Session session, C_TryMatchingReq packet)
        {
            throw new NotImplementedException();
        }

        public void C_TurnEndReq_Handle(Session session, C_TurnEndReq packet)
        {
            throw new NotImplementedException();
        }

        public void C_UseCardReq_Handle(Session session, C_UseCardReq packet)
        {
            throw new NotImplementedException();
        }

        public void S_BuyShopItemRes_Handle(Session session, S_BuyShopItemRes packet)
        {
            throw new NotImplementedException();
        }

        public void S_CheckIdAvailableRes_Handle(Session session, S_CheckIdAvailableRes packet)
        {
            throw new NotImplementedException();
        }

        public void S_ConnectServerRes_Handle(Session session, S_ConnectServerRes packet)
        {
            throw new NotImplementedException();
        }

        public void S_CurrencyInfoRes_Handle(Session session, S_CurrencyInfoRes packet)
        {
            throw new NotImplementedException();
        }

        public void S_DeckEditRes_Handle(Session session, S_DeckEditRes packet)
        {
            throw new NotImplementedException();
        }

        public void S_DeckInfoRes_Handle(Session session, S_DeckInfoRes packet)
        {
            throw new NotImplementedException();
        }

        public void S_GameResult_Handle(Session session, S_GameResult packet)
        {
            throw new NotImplementedException();
        }

        public void S_GameRoomStart_Handle(Session session, S_GameRoomStart packet)
        {
            throw new NotImplementedException();
        }

        public void S_IngameActionChain_Handle(Session session, S_IngameActionChain packet)
        {
            throw new NotImplementedException();
        }

        public void S_LoginRes_Handle(Session session, S_LoginRes packet)
        {
            throw new NotImplementedException();
        }

        public void S_MatchingSuccess_Handle(Session session, S_MatchingSuccess packet)
        {
            throw new NotImplementedException();
        }

        public void S_ShopInfoRes_Handle(Session session, S_ShopInfoRes packet)
        {
            throw new NotImplementedException();
        }

        public void S_SignInRes_Handle(Session session, S_SignInRes packet)
        {
            throw new NotImplementedException();
        }

        public void S_TryMatchingRes_Handle(Session session, S_TryMatchingRes packet)
        {
            throw new NotImplementedException();
        }

        public void S_TurnEnd_Handle(Session session, S_TurnEnd packet)
        {
            throw new NotImplementedException();
        }

        public void S_UseCardRes_Handle(Session session, S_UseCardRes packet)
        {
            throw new NotImplementedException();
        }
    }
}
