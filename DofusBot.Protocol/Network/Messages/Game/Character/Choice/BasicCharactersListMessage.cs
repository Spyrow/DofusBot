using DofusBot.Protocol.Network.Types.Game.Character.Choice;
using System.Collections.Generic;

namespace DofusBot.Protocol.Network.Messages.Game.Character.Choice
{
    public class BasicCharactersListMessage : NetworkMessage
    {
        public const int ProtocolId = 6475;
        public override int MessageID { get { return ProtocolId; } }

        public List<CharacterBaseInformations> Characters;

        public BasicCharactersListMessage() { }

        public BasicCharactersListMessage(List<CharacterBaseInformations> characters)
        {
            Characters = characters;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(((short)(Characters.Count)));
            int charactersIndex;
            for (charactersIndex = 0; (charactersIndex < Characters.Count); charactersIndex = (charactersIndex + 1))
            {
                CharacterBaseInformations objectToSend = Characters[charactersIndex];
                writer.WriteUShort(((ushort)(objectToSend.TypeID)));
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            int charactersCount = reader.ReadUShort();
            Characters = new List<CharacterBaseInformations>();
            for (int i = 0; i < charactersCount; i++)
            {
                CharacterBaseInformations objectToAdd = ProtocolManager.GetTypeInstance<CharacterBaseInformations>(reader.ReadUShort());
                objectToAdd.Deserialize(reader);
                Characters.Add(objectToAdd);
            }
        }
    }
}
