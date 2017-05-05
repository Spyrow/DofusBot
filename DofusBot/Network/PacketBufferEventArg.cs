using System;

namespace DofusBot.Network
{
    public class PacketBufferEventArg : EventArgs
    {
        private byte[] _data;
        private int _packetId;

        public byte[] Data
        {
            get { return _data; }
        }

        public int PacketId
        {
            get { return _packetId; }
        }

        public PacketBufferEventArg(int packetId, byte[] buffer)
        {
            _data = buffer;
            _packetId = packetId;
        }

    }
}
