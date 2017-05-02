using DofusBot.Core.Network;
using DofusBot.Packet.Types.Game.Character.Choice;
using System.Collections.Generic;

namespace DofusBot.Packet.Messages.Game.Character.Choice
{
    public class BasicCharactersListMessage : NetworkMessage
    {
        public ServerPacketEnum PacketType
        {
            get { return ServerPacketEnum.BasicCharactersListMessage; }
        }

        public const uint ProtocolId = 6475;
        public override uint MessageID { get { return ProtocolId; } }

        public List<CharacterBaseInformations> Characters;

        public BasicCharactersListMessage() { }

        public BasicCharactersListMessage(List<CharacterBaseInformations> characters)
        {
            Characters = characters;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)this.Characters.Count);
            uint _loc2_ = 0;
            while ((_loc2_ < this.Characters.Count))
            {

                writer.WriteShort((this.Characters[(int)_loc2_]).TypeID);
                this.Characters[(int)_loc2_].Serialize(writer);
                _loc2_ += 1;
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            uint _loc4_ = 0;
            int charactersCount = reader.ReadUShort();
            Characters = new List<CharacterBaseInformations>();
            for (int i = 0; i < charactersCount; i++)
            {
                _loc4_ = reader.ReadByte();
                CharacterBaseInformations objectToAdd = (CharacterBaseInformations)ProtocolTypeManager.GetInstance(reader.ReadUShort());
                objectToAdd.Deserialize(reader);
                Characters.Add(objectToAdd);
            }
        }
    }
}
