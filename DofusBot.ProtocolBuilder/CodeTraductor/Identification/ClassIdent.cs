using DofusBot.ProtocolBuilder.CodeTraductor.Dictionary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DofusBot.ProtocolBuilder.CodeTraductor.Identification
{
    public class ClassIdent
    {
        public List<string> TypesOrMessages { get; private set; }
        public List<string> Enums { get; private set; }
        public List<string> GameDatas { get; private set; }

        public void SortDirectory(string[] files)
        {
            TypesOrMessages = new List<string>();
            Enums = new List<string>();
            GameDatas = new List<string>();

            foreach (string file in files)
            { 
                string str = File.ReadAllText(file);
                ClassTypeEnum t = GetClassType(str);
                switch (t)
                {
                    case ClassTypeEnum.Enum:
                        Enums.Add(file);
                        break;
                    case ClassTypeEnum.GameData:
                        GameDatas.Add(file);
                        break;
                    case ClassTypeEnum.MessageOrType:
                        TypesOrMessages.Add(file);
                        break;
                }
            }
        }

        public ClassTypeEnum GetClassType(string str)
        {
            Match m = RegularExpression.GetRegex(RegexEnum.NameSpace).Match(str);
            if (m.Success)
            {
                string name = m.Groups["name"].Value;

                if (name.Contains("com.ankamagames.dofus.datacenter"))
                    return ClassTypeEnum.GameData;

                if (name.Contains("com.ankamagames.dofus.network.messages"))
                    return ClassTypeEnum.MessageOrType;

                if (name.Contains("com.ankamagames.dofus.network.types"))
                    return ClassTypeEnum.MessageOrType;

                if (name.Contains("com.ankamagames.dofus.network.enums"))
                    return ClassTypeEnum.Enum;
            }

            return ClassTypeEnum.Undefined;
        }
    }
}
