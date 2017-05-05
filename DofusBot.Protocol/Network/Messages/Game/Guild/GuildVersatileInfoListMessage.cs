//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DofusBot.Protocol.Network.Messages.Game.Guild
{
    using DofusBot.Protocol.Network.Types.Game.Social;
    using DofusBot.Protocol.Network;
    using System.Collections.Generic;
    using DofusBot.Protocol.Network.Messages;
    using DofusBot.Protocol.Network.Types;
    using DofusBot.Protocol;
    
    
    public class GuildVersatileInfoListMessage : NetworkMessage
    {
        
        public const int ProtocolId = 6435;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private List<GuildVersatileInformations> m_guilds;
        
        public virtual List<GuildVersatileInformations> Guilds
        {
            get
            {
                return m_guilds;
            }
            set
            {
                m_guilds = value;
            }
        }
        
        public GuildVersatileInfoListMessage(List<GuildVersatileInformations> guilds)
        {
            m_guilds = guilds;
        }
        
        public GuildVersatileInfoListMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(((short)(m_guilds.Count)));
            int guildsIndex;
            for (guildsIndex = 0; (guildsIndex < m_guilds.Count); guildsIndex = (guildsIndex + 1))
            {
                GuildVersatileInformations objectToSend = m_guilds[guildsIndex];
                writer.WriteUShort(((ushort)(objectToSend.TypeID)));
                objectToSend.Serialize(writer);
            }
        }
        
        public override void Deserialize(IDataReader reader)
        {
            int guildsCount = reader.ReadUShort();
            int guildsIndex;
            m_guilds = new System.Collections.Generic.List<GuildVersatileInformations>();
            for (guildsIndex = 0; (guildsIndex < guildsCount); guildsIndex = (guildsIndex + 1))
            {
                GuildVersatileInformations objectToAdd = ProtocolManager.GetTypeInstance<GuildVersatileInformations>(reader.ReadUShort());
                objectToAdd.Deserialize(reader);
                m_guilds.Add(objectToAdd);
            }
        }
    }
}