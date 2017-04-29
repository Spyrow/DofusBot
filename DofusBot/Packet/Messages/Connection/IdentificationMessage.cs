﻿using DofusBot.Core.Network;
using DofusBot.Core.Network.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DofusBot.Packet.Messages.Connection
{
    public class IdentificationMessage : NetworkMessage
    {
        public ClientPacketEnum Type
        {
            get { return ClientPacketEnum.IdentificationMessage; }
        }

        public const uint ProtocolId = 4;
        public override uint MessageID { get { return ProtocolId; } }

        public bool Autoconnect { get; set; }
        public bool UseCertificate { get; set; }
        public bool UseLoginToken { get; set; }
        public Types.VersionExtended Version { get; set; }
        public string Lang { get; set; }
        public sbyte[] Credentials { get; set; }
        public short ServerId { get; set; }
        public long SessionOptionalSalt { get; set; }
        public ushort[] FailedAttempts { get; set; }

        public IdentificationMessage()
        {
        }

        public IdentificationMessage(bool autoconnect, bool useCertificate, bool useLoginToken, Types.VersionExtended version, string lang, sbyte[] credentials, short serverId, long sessionOptionalSalt, ushort[] failedAttempts)
        {
            this.Autoconnect = autoconnect;
            this.UseCertificate = useCertificate;
            this.UseLoginToken = useLoginToken;
            this.Version = version;
            this.Lang = lang;
            this.Credentials = credentials;
            this.ServerId = serverId;
            this.SessionOptionalSalt = sessionOptionalSalt;
            this.FailedAttempts = failedAttempts;
        }

        public override void Serialize(IDataWriter writer)
        {
            byte flag1 = 0;
            flag1 = BooleanByteWrapper.SetFlag(flag1, 0, Autoconnect);
            flag1 = BooleanByteWrapper.SetFlag(flag1, 1, UseCertificate);
            flag1 = BooleanByteWrapper.SetFlag(flag1, 2, UseLoginToken);
            writer.WriteByte(flag1);
            Version.Serialize(writer);
            writer.WriteUTF(Lang);
            writer.WriteVarInt(Credentials.Length);
            foreach (var entry in Credentials)
            {
                writer.WriteSByte(entry);
            }
            writer.WriteShort(ServerId);
            writer.WriteVarLong(SessionOptionalSalt);
            writer.WriteShort((short)FailedAttempts.Length);
            foreach (var entry in FailedAttempts)
            {
                writer.WriteVarShort(entry);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            byte flag1 = reader.ReadByte();
            Autoconnect = BooleanByteWrapper.GetFlag(flag1, 0);
            UseCertificate = BooleanByteWrapper.GetFlag(flag1, 1);
            UseLoginToken = BooleanByteWrapper.GetFlag(flag1, 2);
            Version = new Types.VersionExtended();
            Version.Deserialize(reader);
            Lang = reader.ReadUTF();
            var limit = reader.ReadVarInt();
            Credentials = new sbyte[limit];
            for (int i = 0; i < limit; i++)
            {
                Credentials[i] = reader.ReadSByte();
            }
            ServerId = reader.ReadShort();
            SessionOptionalSalt = reader.ReadVarLong();
            if (SessionOptionalSalt < -9007199254740990 || SessionOptionalSalt > 9007199254740990)
                throw new Exception("Forbidden value on SessionOptionalSalt = " + SessionOptionalSalt + ", it doesn't respect the following condition : sessionOptionalSalt < -9007199254740990 || sessionOptionalSalt > 9007199254740990");
            limit = reader.ReadUShort();
            FailedAttempts = new ushort[limit];
            for (int i = 0; i < limit; i++)
            {
                FailedAttempts[i] = reader.ReadVarUhShort();
            }
        }

    }
}