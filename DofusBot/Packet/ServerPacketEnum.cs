using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusBot.Packet
{
    public enum ServerPacketEnum
    {
        ProtocolRequired = 1,
        HelloConnectMessage = 3,
        LoginQueueStatusMessage = 10,
        IdentificationFailedMessage = 20,
        IdentificationSuccessMessage = 22,
        ServerListMessage = 30,
        CredentialsAcknowledgementMessage = 6314,
    }
}
