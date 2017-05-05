using System;

namespace DofusBot.Protocol
{
    public abstract class NetworkType : MarshalByRefObject
    {
        public abstract int TypeID { get; }

        public abstract void Serialize(IDataWriter writer);
        public abstract void Deserialize(IDataReader reader);
    }
}
