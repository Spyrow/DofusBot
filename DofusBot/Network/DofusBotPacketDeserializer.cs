using DofusBot.Utilities.Extensions;
using DofusBot.Protocol;
using System;

namespace DofusBot.Network
{
    public class DofusBotPacketDeserializer
    {
        public event EventHandler<PacketEventArg> ReceivePacket;
        public delegate void ReceivePacketEventHandler(PacketEventArg e);
        public event EventHandler<NullPacketEventArg> ReceiveNullPacket;
        public delegate void ReceiveNullPacketEventHandler(NullPacketEventArg e);

        public void GetPacket(object obj, PacketBufferEventArg e)
        {
            ServerPacketEnum packetType = (ServerPacketEnum) e.PacketId;
            BigEndianReader reader = new BigEndianReader(e.Data);
            NetworkMessage msg = MessageReceiver.BuildMessage(e.PacketId, reader);

            if (msg == null)
                ReceiveNullPacket.Raise(this, new NullPacketEventArg(packetType));
            else
                ReceivePacket.Raise(this, new PacketEventArg(msg));
        }
    }
}
