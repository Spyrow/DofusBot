using DofusBot.Core.Network;

namespace DofusBot.Packet.Messages.Queues
{
    public class LoginQueueStatusMessage : NetworkMessage
    {
        public ServerPacketEnum Type
        {
            get { return ServerPacketEnum.LoginQueueStatusMessage; }
        }

        public const uint ProtocolId = 10;
        public override uint MessageID { get { return ProtocolId; } }

        public ushort position;
        public ushort total;

        public LoginQueueStatusMessage() { }

        public LoginQueueStatusMessage(ushort position, ushort total)
        {
            this.position = position;
            this.total = total;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short) this.position);
            writer.WriteShort((short) this.total);
        }

        public override void Deserialize(IDataReader reader)
        {
            this.position = reader.ReadUShort();
            this.total = reader.ReadUShort();
        }
    }
}
