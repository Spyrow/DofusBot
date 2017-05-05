using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusBot.ProtocolBuilder.CodeTraductor.Template
{
    public class GameDataClass
    {
        public string ClassName { get; set; }
        public string Namespace { get; set; }
        public string Parent { get; set; }
        public string Interface { get; set; }
        public List<string> Import { get; set; }
        public string Path { get; set; }
        public string BasePath { get; set; }
        public List<GameDataVariable> Variables { get; set; }

        public GameDataClass()
        {
            Variables = new List<GameDataVariable>();
            Import = new List<string>();
        }
    }
}
