using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusBot.Network
{
    public class PacketBufferEventArg : EventArgs
    {
        private byte[] _data;
        private short _packetId;

        public byte[] Data
        {
            get { return _data; }
        }

        public short PacketId
        {
            get { return _packetId; }
        }

        public PacketBufferEventArg(short packetId, byte[] buffer)
        {
            _data = buffer;
            _packetId = packetId;
        }

    }
}
