using DofusBot.ProtocolBuilder.CodeTraductor.Enums;
using System;

namespace DofusBot.ProtocolBuilder.CodeTraductor.Template
{
    public struct Variable
    {
        public string Name { get; set; }
        public VarType TypeOfVar { get; set; }
        public ReadMethodeType MethodeType { get; set; }
        public Type @Type { get; set; }
        public string ObjectType { get; set; }
        public string ReadMethode { get; set; }
        public string WriteMethode { get; set; }
    }
}