﻿using DofusBot.Core.Network;
using DofusBot.Packet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DofusBot.Packet.Messages.Handshake
{
    public class ProtocolRequired : NetworkMessage
    {
        public ServerPacketEnum Type
        {
            get { return ServerPacketEnum.ProtocolRequired; }
        }

        public const uint ProtocolId = 1;
        public override uint MessageID { get { return ProtocolId; } }

        public uint RequiredVersion { get; set; }
        public uint CurrentVersion { get; set; }

        public ProtocolRequired()
        {
        }

        public ProtocolRequired(uint requiredVersion, uint currentVersion)
        {
            this.RequiredVersion = requiredVersion;
            this.CurrentVersion = currentVersion;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUInt(RequiredVersion);
            writer.WriteUInt(CurrentVersion);
        }

        public override void Deserialize(IDataReader reader)
        {
            RequiredVersion = reader.ReadUInt();
            if (RequiredVersion < 0)
                throw new Exception("Forbidden value on RequiredVersion = " + RequiredVersion + ", it doesn't respect the following condition : requiredVersion < 0");
            CurrentVersion = reader.ReadUInt();
            if (CurrentVersion < 0)
                throw new Exception("Forbidden value on CurrentVersion = " + CurrentVersion + ", it doesn't respect the following condition : currentVersion < 0");
        }

    }
}
