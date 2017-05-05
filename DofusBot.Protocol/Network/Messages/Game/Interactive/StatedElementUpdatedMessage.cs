//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DofusBot.Protocol.Network.Messages.Game.Interactive
{
    using DofusBot.Protocol.Network.Types.Game.Interactive;
    using System.Collections.Generic;
    using DofusBot.Protocol.Network.Messages;
    using DofusBot.Protocol.Network.Types;
    using DofusBot.Protocol;
    
    
    public class StatedElementUpdatedMessage : NetworkMessage
    {
        
        public const int ProtocolId = 5709;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private StatedElement m_statedElement;
        
        public virtual StatedElement StatedElement
        {
            get
            {
                return m_statedElement;
            }
            set
            {
                m_statedElement = value;
            }
        }
        
        public StatedElementUpdatedMessage(StatedElement statedElement)
        {
            m_statedElement = statedElement;
        }
        
        public StatedElementUpdatedMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            m_statedElement.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_statedElement = new StatedElement();
            m_statedElement.Deserialize(reader);
        }
    }
}