namespace DofusBot.Packet.Types.Game.Character
{
    public class AbstractCharacterInformation
    {
        public TypeEnum Type
        {
            get { return TypeEnum.AbstractCharacterInformation; }
        }

        public const short ProtocolId = 400;
        public virtual short TypeID { get { return ProtocolId; } }

        public double ObjectID { get; set; }

        public AbstractCharacterInformation() { }

        public AbstractCharacterInformation(double objectId)
        {
            ObjectID = objectId;
        }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarLong((long)ObjectID);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            ObjectID = reader.ReadVarUhLong();
        }
    }
}
