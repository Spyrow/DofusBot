//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DofusBot.Protocol.Network.Messages.Game.Context.Roleplay.Job
{
    using DofusBot.Protocol.Network.Types.Game.Context.Roleplay.Job;
    using System.Collections.Generic;
    using DofusBot.Protocol.Network.Messages;
    using DofusBot.Protocol.Network.Types;
    using DofusBot.Protocol;
    
    
    public class JobCrafterDirectoryDefineSettingsMessage : NetworkMessage
    {
        
        public const int ProtocolId = 5649;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private JobCrafterDirectorySettings m_settings;
        
        public virtual JobCrafterDirectorySettings Settings
        {
            get
            {
                return m_settings;
            }
            set
            {
                m_settings = value;
            }
        }
        
        public JobCrafterDirectoryDefineSettingsMessage(JobCrafterDirectorySettings settings)
        {
            m_settings = settings;
        }
        
        public JobCrafterDirectoryDefineSettingsMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            m_settings.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_settings = new JobCrafterDirectorySettings();
            m_settings.Deserialize(reader);
        }
    }
}