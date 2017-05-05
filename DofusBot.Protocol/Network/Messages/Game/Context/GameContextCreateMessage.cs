//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DofusBot.Protocol.Network.Messages.Game.Context
{
    using System.Collections.Generic;
    using DofusBot.Protocol.Network.Messages;
    using DofusBot.Protocol.Network.Types;
    using DofusBot.Protocol;
    
    
    public class GameContextCreateMessage : NetworkMessage
    {
        
        public const int ProtocolId = 200;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private byte m_context;
        
        public virtual byte Context
        {
            get
            {
                return m_context;
            }
            set
            {
                m_context = value;
            }
        }
        
        public GameContextCreateMessage(byte context)
        {
            m_context = context;
        }
        
        public GameContextCreateMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte(m_context);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_context = reader.ReadByte();
        }
    }
}