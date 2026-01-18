using System;
using System.Text;
using System.Net;
using Shared.Network;
using Shared.Packets;
using Shared.Contents;

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
            int c = 0;

            ushort packetSize = BitConverter.ToUInt16(new ArraySegment<byte>(buffer.Array, buffer.Offset, buffer.Count));
            c += sizeof(ushort);

            PacketID packetId = (PacketID)BitConverter.ToUInt16(new ArraySegment<byte>(buffer.Array, buffer.Offset + c, packetSize - c));
            c += sizeof(ushort);

            ArraySegment<byte> packetData = new ArraySegment<byte>(buffer.Array, buffer.Offset + c, packetSize - c);
            IPacket packet = PacketFactory.GeneratePacket(packetId, packetData);

            Console.WriteLine($"[From Client] packetId : {packetId}");

            ((IPacketHandler)PacketHandler.Instance).RunPakcetHandle(this, packetId, packet);
        }

        public override void OnSend(int numOfByte)
        {
            Console.WriteLine($"Transferred byte : {numOfByte}");
        }
    }
}
