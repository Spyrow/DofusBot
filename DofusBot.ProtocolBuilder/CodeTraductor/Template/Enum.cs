using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusBot.ProtocolBuilder.CodeTraductor.Template
{
    public struct Enum
    {
        public string Name { get; set; }
        public EnumItem[] Items { get; set; }
    }
}
