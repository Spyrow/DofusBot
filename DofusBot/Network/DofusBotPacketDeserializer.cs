using DofusBot.Core;
using DofusBot.Core.Network;
using DofusBot.Packet;
using DofusBot.Packet.Messages.Connection;
using DofusBot.Packet.Messages.Handshake;
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

            switch (packetType)
            {
                case ServerPacketEnum.ProtocolRequired:
                    msg = (ProtocolRequired) msg;
                    break;
                case ServerPacketEnum.HelloConnectMessage:
                    msg = (HelloConnectMessage) msg;
                    break;
                default:
                    Console.WriteLine("Packet id : {0} is not implemented", e.PacketId);
                    break;
            }

            OnReceivePacket(new PacketEventArg(msg));
        }


    }
}
