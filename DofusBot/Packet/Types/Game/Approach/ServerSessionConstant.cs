namespace DofusBot.Packet.Types.Game.Approach
{
    public class ServerSessionConstant
    {
        public TypeEnum Type
        {
            get { return TypeEnum.ServerSessionConstant; }
        }

        public const short ProtocolId = 430;
        public virtual short TypeID { get { return ProtocolId; } }

        public ushort ObjectID { get; set; }

        public ServerSessionConstant() { }

        public ServerSessionConstant(ushort objectId)
        {
            ObjectID = objectId;
        }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarShort(ObjectID);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            ObjectID = reader.ReadVarUhShort();
        }
    }
}
