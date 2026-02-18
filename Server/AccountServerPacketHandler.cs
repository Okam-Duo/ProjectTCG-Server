using Server.GameServer;
using Shared.Network;
using Shared.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class AccountServerPacketHandler : IPacketHandler
    {
        private AccountServer _server;

        public AccountServerPacketHandler(AccountServer server)
        {
            _server = server;
        }

        private void OnRecieveServerToClientPacket(Session session, IPacket packet)
        {
            Console.WriteLine($"Server->Client 패킷이 수신되었습니다, 패킷 타입 : {packet.GetType().Name}");
        }

        #region 패킷 핸들

        public void C_BuyShopItemReq_Handle(Session session, C_BuyShopItemReq packet)
        {
            throw new NotImplementedException();
        }

        public async void C_CheckIdAvailableReq_Handle(Session session, C_CheckIdAvailableReq packet)
        {
            bool idAvailable = await _server.CheckIdAvailable(packet.id);
            session.Send(new S_CheckIdAvailableRes(idAvailable).Write());
        }

        public void C_ConnectServerReq_Handle(Session session, C_ConnectServerReq packet)
        {
            Console.WriteLine($"{nameof(C_ConnectServerReq)} 수신");
            session.Send(new S_ConnectServerRes().Write());
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
            if (session is ClientSession clientSession)
            {
                _server.TryLogin(packet.id, packet.passward.GetHashCode().ToString(), clientSession);
            }
        }

        public void C_LogoutReq_Handle(Session session, C_LogoutReq packet)
        {
            throw new NotImplementedException();
        }

        public void C_ShopInfoReq_Handle(Session session, C_ShopInfoReq packet)
        {
            throw new NotImplementedException();
        }

        public async void C_SignInReq_Handle(Session session, C_SignInReq packet)
        {
            bool isSuccess = await _server.TrySignIn(packet.id, packet.passward.GetHashCode().ToString(), packet.nickName);
            session.Send(new S_SignInRes(isSuccess).Write());
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
            OnRecieveServerToClientPacket(session, packet);
        }

        public void S_CheckIdAvailableRes_Handle(Session session, S_CheckIdAvailableRes packet)
        {
            OnRecieveServerToClientPacket(session, packet);
        }

        public void S_ConnectServerRes_Handle(Session session, S_ConnectServerRes packet)
        {
            OnRecieveServerToClientPacket(session, packet);
        }

        public void S_CurrencyInfoRes_Handle(Session session, S_CurrencyInfoRes packet)
        {
            OnRecieveServerToClientPacket(session, packet);
        }

        public void S_DeckEditRes_Handle(Session session, S_DeckEditRes packet)
        {
            OnRecieveServerToClientPacket(session, packet);
        }

        public void S_DeckInfoRes_Handle(Session session, S_DeckInfoRes packet)
        {
            OnRecieveServerToClientPacket(session, packet);
        }

        public void S_GameResult_Handle(Session session, S_GameResult packet)
        {
            OnRecieveServerToClientPacket(session, packet);
        }

        public void S_GameRoomStart_Handle(Session session, S_GameRoomStart packet)
        {
            OnRecieveServerToClientPacket(session, packet);
        }

        public void S_IngameActionChain_Handle(Session session, S_IngameActionChain packet)
        {
            OnRecieveServerToClientPacket(session, packet);
        }

        public void S_LoginRes_Handle(Session session, S_LoginRes packet)
        {
            OnRecieveServerToClientPacket(session, packet);
        }

        public void S_MatchingSuccess_Handle(Session session, S_MatchingSuccess packet)
        {
            OnRecieveServerToClientPacket(session, packet);
        }

        public void S_ShopInfoRes_Handle(Session session, S_ShopInfoRes packet)
        {
            OnRecieveServerToClientPacket(session, packet);
        }

        public void S_SignInRes_Handle(Session session, S_SignInRes packet)
        {
            OnRecieveServerToClientPacket(session, packet);
        }

        public void S_TryMatchingRes_Handle(Session session, S_TryMatchingRes packet)
        {
            OnRecieveServerToClientPacket(session, packet);
        }

        public void S_TurnEnd_Handle(Session session, S_TurnEnd packet)
        {
            OnRecieveServerToClientPacket(session, packet);
        }

        public void S_UseCardRes_Handle(Session session, S_UseCardRes packet)
        {
            OnRecieveServerToClientPacket(session, packet);
        }

        #endregion
    }
}
