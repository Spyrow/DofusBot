using System;

namespace DofusBot.Packet.Types.Game.Look
{
    public class SubEntity
    {
        public TypeEnum Type
        {
            get { return TypeEnum.SubEntity; }
        }

        public const short ProtocolId = 54;
        public virtual short TypeID { get { return ProtocolId; } }

        public byte BindingPointCategory;
        public byte BindingPointIndex;
        public EntityLook SubEntityLook;

        public SubEntity() { }

        public SubEntity(byte bindingPointCategory, byte bindingPointIndex, EntityLook subEntityLook)
        {
            BindingPointCategory = bindingPointCategory;
            BindingPointIndex = bindingPointIndex;
            SubEntityLook = subEntityLook;
        }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteByte(BindingPointCategory);
            writer.WriteByte(BindingPointIndex);
            SubEntityLook.Serialize(writer);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            BindingPointCategory = reader.ReadByte();
            BindingPointIndex = reader.ReadByte();
            SubEntityLook = new EntityLook();
            SubEntityLook.Deserialize(reader);
        }
    }
}
