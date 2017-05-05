using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusBot.ProtocolBuilder.CodeTraductor.Enums
{
    public enum ReadMethodeType
    {
        Primitive,
        SerializeOrDeserialize,
        ProtocolTypeManager,
        BooleanByteWraper
    }
}
