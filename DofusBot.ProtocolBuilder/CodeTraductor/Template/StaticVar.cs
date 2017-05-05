using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusBot.ProtocolBuilder.CodeTraductor.Template
{
    public struct StaticVar
    {
        public string Name { get; set; }
        public Type @Type { get; set; }
        public string Value { get; set; }

        public StaticVar Int(string name, Type @type, string value)
        {
            Name = name;
            @Type = type;
            Value = value;
            return this;
        }
    }
}
