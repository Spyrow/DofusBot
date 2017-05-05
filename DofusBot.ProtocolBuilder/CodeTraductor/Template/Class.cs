using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusBot.ProtocolBuilder.CodeTraductor.Template
{
    public struct Class
    {
        public string Name { get; set; }
        public string NameSpace { get; set; }
        public string Parent { get; set; }
        public string Interface { get; set; }
        public int ProtocolId { get; set; }
        public string[] Imports { get; set; }
        public Variable[] Variables { get;  set; }
        public StaticVar[] Constantes { get;  set; }
    }
}
