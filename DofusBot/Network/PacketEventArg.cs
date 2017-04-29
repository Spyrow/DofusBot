using DofusBot.Core.Network;
using DofusBot.Packet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusBot.Network
{
    public class PacketEventArg : EventArgs
    {
        private NetworkMessage _packet;

        public NetworkMessage Packet
        {
            get { return _packet; }
        }

        public PacketEventArg(NetworkMessage packet)
        {
            _packet = packet;
        }
    }
}
