//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DofusBot.Protocol.DataCenter
{
    using System.Collections.Generic;
    using DofusBot.Protocol;
    using System;
    
    
    [Serializable()]
    public class ChatChannel : IData
    {
        
        public virtual uint Id
        {
            get
            {
                return mId;
            }
            set
            {
                mId = value;
            }
        }
        
        private uint mId;
        
        public virtual uint NameId
        {
            get
            {
                return mNameId;
            }
            set
            {
                mNameId = value;
            }
        }
        
        private uint mNameId;
        
        public virtual uint DescriptionId
        {
            get
            {
                return mDescriptionId;
            }
            set
            {
                mDescriptionId = value;
            }
        }
        
        private uint mDescriptionId;
        
        public virtual string Shortcut
        {
            get
            {
                return mShortcut;
            }
            set
            {
                mShortcut = value;
            }
        }
        
        private string mShortcut;
        
        public virtual string ShortcutKey
        {
            get
            {
                return mShortcutKey;
            }
            set
            {
                mShortcutKey = value;
            }
        }
        
        private string mShortcutKey;
        
        public virtual bool IsPrivate
        {
            get
            {
                return mIsPrivate;
            }
            set
            {
                mIsPrivate = value;
            }
        }
        
        private bool mIsPrivate;
        
        public virtual bool AllowObjects
        {
            get
            {
                return mAllowObjects;
            }
            set
            {
                mAllowObjects = value;
            }
        }
        
        private bool mAllowObjects;
        
        public ChatChannel()
        {
        }
    }
}
