using DofusBot.Core.Network;

namespace DofusBot.Packet.Messages.Connection
{
    public class ServerSelectionMessage : NetworkMessage
    {
        public ClientPacketEnum PacketType
        {
            get { return ClientPacketEnum.ServerSelectionMessage; }
        }

        public const uint ProtocolId = 40;
        public override uint MessageID { get { return ProtocolId; } }

        private short _serverId;

        public ServerSelectionMessage(short serverId)
        {
            _serverId = serverId;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(_serverId);
        }

        public override void Deserialize(IDataReader reader)
        {
            _serverId = reader.ReadShort();
        }
    }
}
