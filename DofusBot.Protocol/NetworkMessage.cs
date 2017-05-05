namespace DofusBot.Protocol
{

    public abstract class NetworkMessage
    {
        private const byte BIT_RIGHT_SHIFT_LEN_Packet_ID = 2;
        private const byte BIT_MASK = 3;

        public abstract int MessageID { get; }

        public void Unpack(IDataReader reader)
        {
            Deserialize(reader);
        }

        public void Pack(IDataWriter writer)
        {
            Serialize(writer);
            WritePacket(writer);
        }

        public abstract void Serialize(IDataWriter writer);
        public abstract void Deserialize(IDataReader reader);

        private void WritePacket(IDataWriter writer)
        {
            byte[] Packet = writer.Data;

            writer.Clear();

            byte typeLen = ComputeTypeLen(Packet.Length);
            var header = (short)SubComputeStaticHeader((uint)MessageID, typeLen);
            writer.WriteShort(header);

            switch (typeLen)
            {
                case 0:
                    break;
                case 1:
                    writer.WriteByte((byte)Packet.Length);
                    break;
                case 2:
                    writer.WriteShort((short)Packet.Length);
                    break;
                case 3:
                    writer.WriteByte((byte)(Packet.Length >> 16 & 255));
                    writer.WriteShort((short)(Packet.Length & 65535));
                    break;
                default:
                    throw new System.Exception("Packet's length can't be encoded on 4 or more bytes");
            }
            writer.WriteBytes(Packet);
        }

        private static byte ComputeTypeLen(int param1)
        {
            if (param1 > 65535)
                return 3;

            if (param1 > 255)
                return 2;

            if (param1 > 0)
                return 1;

            return 0;
        }

        private static uint SubComputeStaticHeader(uint id, byte typeLen)
        {
            return id << BIT_RIGHT_SHIFT_LEN_Packet_ID | typeLen;
        }

        public override string ToString()
        {
            return GetType().Name;
        }
    }

}
