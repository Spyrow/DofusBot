 

namespace DofusBot.Protocol.Network.Messages.Game.Context
{
    public class GameContextCreateRequestMessage : NetworkMessage
    {
        public const int ProtocolId = 250;
        public override int MessageID { get { return ProtocolId; } }

        public GameContextCreateRequestMessage() { }

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
