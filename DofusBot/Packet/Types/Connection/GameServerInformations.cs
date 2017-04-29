using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusBot.Packet.Types.Connection
{
    public class GameServerInformations
    {
        public TypeEnum Type
        {
            get { return TypeEnum.GameServerInformations; }
        }

        public const short ProtocolId = 25;
        public virtual short TypeID { get { return ProtocolId; } }

        public ushort ObjectID { get; set; }
        public byte Status { get; set; }
        public byte Completion { get; set; }
        public bool IsSelectable { get; set; }
        public byte CharactersCount { get; set; }
        public byte CharactersSlots { get; set; }
        public double Date { get; set; }

        public GameServerInformations()
        {
        }

        public GameServerInformations(ushort objectId, byte status, byte completion, bool isSelectable, byte charactersCount, byte charactersSlots, double date)
        {
            ObjectID = objectId;
            Status = status;
            Completion = completion;
            IsSelectable = isSelectable;
            CharactersCount = charactersCount;
            CharactersSlots = charactersSlots;
            Date = date;
        }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteUShort(ObjectID);
            writer.WriteByte(Status);
            writer.WriteByte(Completion);
            writer.WriteBoolean(IsSelectable);
            writer.WriteByte(CharactersCount);
            writer.WriteByte(CharactersSlots);
            writer.WriteDouble(Date);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            ObjectID = reader.ReadUShort();
            Status = reader.ReadByte();
            Completion = reader.ReadByte();
            IsSelectable = reader.ReadBoolean();
            CharactersCount = reader.ReadByte();
            CharactersSlots = reader.ReadByte();
            Date = reader.ReadDouble();
        }

    }
}
