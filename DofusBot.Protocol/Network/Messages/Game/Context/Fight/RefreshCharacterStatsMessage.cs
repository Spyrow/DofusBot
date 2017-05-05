//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DofusBot.Protocol.Network.Messages.Game.Context.Fight
{
    using DofusBot.Protocol.Network.Types.Game.Context.Fight;
    using System.Collections.Generic;
    using DofusBot.Protocol.Network.Messages;
    using DofusBot.Protocol.Network.Types;
    using DofusBot.Protocol;
    
    
    public class RefreshCharacterStatsMessage : NetworkMessage
    {
        
        public const int ProtocolId = 6699;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private GameFightMinimalStats m_stats;
        
        public virtual GameFightMinimalStats Stats
        {
            get
            {
                return m_stats;
            }
            set
            {
                m_stats = value;
            }
        }
        
        private double m_fighterId;
        
        public virtual double FighterId
        {
            get
            {
                return m_fighterId;
            }
            set
            {
                m_fighterId = value;
            }
        }
        
        public RefreshCharacterStatsMessage(GameFightMinimalStats stats, double fighterId)
        {
            m_stats = stats;
            m_fighterId = fighterId;
        }
        
        public RefreshCharacterStatsMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            m_stats.Serialize(writer);
            writer.WriteDouble(m_fighterId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_stats = new GameFightMinimalStats();
            m_stats.Deserialize(reader);
            m_fighterId = reader.ReadDouble();
        }
    }
}