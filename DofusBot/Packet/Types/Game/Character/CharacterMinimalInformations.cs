﻿using System;

namespace DofusBot.Packet.Types.Game.Character
{
    public class CharacterMinimalInformations : CharacterBasicMinimalInformations
    {
        public new TypeEnum Type
        {
            get { return TypeEnum.CharacterMinimalInformations; }
        }

        public new const short ProtocolId = 110;
        public new virtual short TypeID { get { return ProtocolId; } }

        public byte Level { get; set; }

        public CharacterMinimalInformations() { }

        public CharacterMinimalInformations(byte level)
        {
            Level = level;
        }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte(Level);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            _levelFunc(reader);
        }
        public void _levelFunc(IDataReader reader)
        {
            Level = reader.ReadByte();
        }
    }
}