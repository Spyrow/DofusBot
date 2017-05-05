using DofusBot.Protocol.Network.Types.Game.Look;

namespace DofusBot.Protocol.Network.Types.Game.Character
{
    public class CharacterMinimalPlusLookInformations : CharacterMinimalInformations
    {
        public new const int ProtocolId = 163;
        public override int TypeID { get { return ProtocolId; } }

        public EntityLook EntityLook { get; set; }

        public CharacterMinimalPlusLookInformations() { }

        public CharacterMinimalPlusLookInformations(EntityLook entityLook)
        {
            EntityLook = entityLook;
        }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            EntityLook.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            EntityLook = new EntityLook();
            EntityLook.Deserialize(reader);
        }
    }
}
