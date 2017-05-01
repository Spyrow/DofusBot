using DofusBot.Core.Network;
using System.Collections.Generic;

namespace DofusBot.Packet.Messages.Security
{
    public class CheckIntegrityMessage : NetworkMessage
    {
        public ClientPacketEnum Type
        {
            get { return ClientPacketEnum.CheckIntegrityMessage; }
        }

        public const uint ProtocolId = 6372;
        public override uint MessageID { get { return ProtocolId; } }


        public List<byte> Data { get; set; }


        public CheckIntegrityMessage() { }

        public CheckIntegrityMessage(List<byte> data)
        {
            Data = data;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarInt(Data.Count);
            for (int i = 0; i < Data.Count; i++)
            {
                writer.WriteByte(Data[i]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            int length = reader.ReadVarInt();
            Data = new List<byte>();
            for (int i = 0; i < length; i++)
            {
                Data.Add(reader.ReadByte());
            }
        }
    }
}
