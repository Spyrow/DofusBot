//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DofusBot.Protocol.Network.Messages.Game.Context.Roleplay.Lockable
{
    using System.Collections.Generic;
    using DofusBot.Protocol.Network.Messages;
    using DofusBot.Protocol.Network.Types;
    using DofusBot.Protocol;
    
    
    public class LockableStateUpdateStorageMessage : LockableStateUpdateAbstractMessage
    {
        
        public const int ProtocolId = 5669;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private int m_mapId;
        
        public virtual int MapId
        {
            get
            {
                return m_mapId;
            }
            set
            {
                m_mapId = value;
            }
        }
        
        private uint m_elementId;
        
        public virtual uint ElementId
        {
            get
            {
                return m_elementId;
            }
            set
            {
                m_elementId = value;
            }
        }
        
        public LockableStateUpdateStorageMessage(int mapId, uint elementId)
        {
            m_mapId = mapId;
            m_elementId = elementId;
        }
        
        public LockableStateUpdateStorageMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(m_mapId);
            writer.WriteVarUhInt(m_elementId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            m_mapId = reader.ReadInt();
            m_elementId = reader.ReadVarUhInt();
        }
    }
}