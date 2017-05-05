 

namespace DofusBot.Protocol.Network.Messages.Game.Friend
{
    public class IgnoredGetListMessage : NetworkMessage
    {
        public const int ProtocolId = 5676;
        public override int MessageID { get { return ProtocolId; } }

        public IgnoredGetListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            //
        }

        public override void Deserialize(IDataReader reader)
        {
            //
        }
    }
}
