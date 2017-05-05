namespace DofusBot.Protocol.Network.Messages.Game.Character.Choice
{
    public class CharactersListRequestMessage : NetworkMessage
    {
        public const int ProtocolId = 150;
        public override int MessageID { get { return ProtocolId; } }

        public CharactersListRequestMessage() { }

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
