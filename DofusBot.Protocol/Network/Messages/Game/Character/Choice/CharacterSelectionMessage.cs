namespace DofusBot.Protocol.Network.Messages.Game.Character.Choice
{
    public class CharacterSelectionMessage : NetworkMessage
    {
        public const int ProtocolId = 152;
        public override int MessageID { get { return ProtocolId; } }

        public ulong ID { get; set; }

        public CharacterSelectionMessage() { }

        public CharacterSelectionMessage(ulong id)
        {
            ID = id;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarLong(ID);
        }

        public override void Deserialize(IDataReader reader)
        {
            ID = reader.ReadVarUhLong();
        }
    }
}
