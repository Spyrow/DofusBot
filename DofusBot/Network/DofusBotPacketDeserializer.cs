using DofusBot.Core;
using DofusBot.Core.Network;
using DofusBot.Packet;
using DofusBot.Packet.Messages.Connection;
using DofusBot.Packet.Messages.Handshake;
using DofusBot.Packet.Messages.Queues;
using System;

namespace DofusBot.Network
{
    public class DofusBotPacketDeserializer
    {
        public event EventHandler<PacketEventArg> ReceivePacket;
        public delegate void ReceivePacketBufferEventHandler(PacketEventArg e);

        protected virtual void OnReceivePacket(PacketEventArg e)
        {
            ReceivePacket.Raise(this, e);
        }

        public void GetPacket(object obj, PacketBufferEventArg e)
        {
            ServerPacketEnum packetType = (ServerPacketEnum) e.PacketId;
            BigEndianReader reader = new BigEndianReader(e.Data);
            NetworkMessage msg = MessageReceiver.BuildMessage((uint) packetType, reader);
            OnReceivePacket(new PacketEventArg(msg));
        }
    }
}
