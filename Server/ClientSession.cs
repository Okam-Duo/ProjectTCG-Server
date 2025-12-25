using System;
using System.Text;
using System.Net;
using Shared.Network;

namespace Server
{
    class ClientSession : PacketSession
    {
        public override void OnConnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnConnected : {endPoint}");
        }

        public override void OnDisconnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnDisconnected : {endPoint}");
        }

        public override void OnRecvPacket(ArraySegment<byte> buffer)
        {
            Console.WriteLine($"[From Client] {Encoding.UTF8.GetString(buffer)}\n");

            //테스트용 데이터 하나만 받고 연결 끊기
            Disconnect();
        }

        public override void OnSend(int numOfByte)
        {
            Console.WriteLine($"Transferred byte : {numOfByte}");
        }
    }
}
