using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DofusBot.ProtocolBuilder.CodeTraductor.Template;
using System.Text.RegularExpressions;
using DofusBot.ProtocolBuilder.CodeTraductor.Dictionary;
using DofusBot.ProtocolBuilder.CodeTraductor.Enums;

namespace DofusBot.ProtocolBuilder.CodeTraductor.Parsing
{
    public class ParserUtility
    {
        public static string GetIParent(string parent, string @interface)
        {
            if (@interface == "INetworkType" && parent == "Object")
                return "NetworkType";
            else
                return parent;
        }

        public static string GetNameSpace(string @namespace, int removeCount = 0)
        {
            @namespace = @namespace.Replace("com.ankamagames.dofus.network.", "DofusBot.Protocol.Network.");
            string[] nameSpaceSplited = @namespace.Split('.');
            @namespace = "";
            for (int i = 0; i < nameSpaceSplited.Length - removeCount; i++)
            {
                string str = nameSpaceSplited[i];
                @namespace += str.Substring(0, 1).ToUpper() + str.Remove(0, 1) + ".";

            }
            return @namespace.Remove(@namespace.Length - 1, 1);
        }

        public static string GetName(string name)
        {
            switch (name)
            {
                case "id":
                    return "ObjectId";
                default:
                    return name;
            }
        }

        public static string[] GetImports(string[] imports)
        {
            List<string> retVal = new List<string>();
            foreach (string import in imports)
            {
                if(import.ToLower().Contains("com.ankamagames.jerakine"))
                {
                    continue;
                }
                switch (import.ToLower())
                {
                    case "com.ankamagames.jerakine.network":
                        break;
                    case "com.ankamagames.jerakine.network.networkmessage":
                        break;
                    case "com.ankamagames.jerakine.network.inetworkmessage":
                        break;
                    case "com.ankamagames.jerakine.network.inetworktype":
                        break;
                    case "com.ankamagames.jerakine.network.networkType":
                        break;
                    case "__AS3__.vec.vector":
                        break;
                    case "flash.utils.idataoutput":
                        break;
                    case "flash.utils.bytearray":
                        break;
                    case "flash.utils.idatainput":
                        break;
                    case "com.ankamagames.dofus.network.protocoltypeManager":
                        break;
                    case "com.ankamagames.jerakine.network.utils.booleanbyteWrapper":
                        break;
                    default:
                        retVal.Add(GetNameSpace(import, 1));
                        break;
                }
            }


            retVal.Add("System.Collections.Generic");
            retVal.Add("DofusBot.Protocol.Network.Messages");
            retVal.Add("DofusBot.Protocol.Network.Types");
            retVal.Add("DofusBot.Protocol");
            return retVal.ToArray();
        }

        public static Variable[] SortVars(Variable[] vars, string fileStr)
        {

            Dictionary<string, Variable> varsDictionary = new Dictionary<string, Variable>();
            foreach (var v in vars)
                varsDictionary.Add(v.Name, v);
            List<Variable> retVal = new List<Variable>();
            string[] lines = (fileStr + (char)10 + (char)10 + (char)10 + (char)10).Split(((char)10));
            int boolCount = 0;

            for (int index = 0; index < lines.Length - 4; index++)
            {
                string linesM1 = "";
                if (index > 1)
                    linesM1 = lines[index - 1];
                string line = lines[index];
                string line2 = lines[index + 1];
                string line3 = lines[index + 2];
                string line4 = lines[index + 4];

                Match m = RegularExpression.GetRegex(RegexEnum.ReadFlagMetode).Match(line);
                if (m.Success)
                {
                    Variable currentVar = varsDictionary[m.Groups["name"].Value];
                    currentVar.Type = typeof(bool);
                    currentVar.MethodeType = Enums.ReadMethodeType.BooleanByteWraper;
                    currentVar.ReadMethode = m.Groups["flag"].Value;
                    currentVar.WriteMethode = m.Groups["flag"].Value;
                    retVal.Insert(boolCount, currentVar);
                    boolCount += 1;
                    continue;
                }
                m = RegularExpression.GetRegex(RegexEnum.ReadMetodePrimitive).Match(line);
                if (m.Success)
                    retVal.Add(varsDictionary[m.Groups["name"].Value]);
                m = RegularExpression.GetRegex(RegexEnum.ReadMethodeObject).Match(line);
                if (m.Success)
                {
                    Match m2 = RegularExpression.GetRegex(RegexEnum.ReadMethodObjectProtocolManager).Match(linesM1);
                    if (m2.Success)
                    {
                        Variable var = varsDictionary[m.Groups["name"].Value];
                        var.MethodeType = ReadMethodeType.ProtocolTypeManager;
                        retVal.Add(var);
                    }
                    else
                        retVal.Add(varsDictionary[m.Groups["name"].Value]);

                }
                m = RegularExpression.GetRegex(RegexEnum.ReadVectorMetodeObject).Match(line + (char)10 + line2 + (char)10 + line3);
                if (m.Success)
                    retVal.Add(varsDictionary[m.Groups["name"].Value]);
                m = RegularExpression.GetRegex(RegexEnum.ReadVectorMetodePotocolManager).Match(line + (char)10 + line2 + (char)10 + line3 + (char)10 + line4);
                if (m.Success)
                {
                    Variable var = varsDictionary[m.Groups["name"].Value];
                    var.ObjectType = m.Groups["type"].Value;
                    var.MethodeType = Enums.ReadMethodeType.ProtocolTypeManager;
                    retVal.Add(var);
                }
                m = RegularExpression.GetRegex(RegexEnum.ReadVectorMetodePrimitive).Match(line + (char)10 + line2);
                if (m.Success)
                    retVal.Add(varsDictionary[m.Groups["name"].Value]);
            }

            List<Variable> rv = new List<Variable>();
            foreach (Variable var in retVal)
            {
                Variable v = var;
                v.Name = GetName(v.Name);
                rv.Add(v);
            }
            return rv.ToArray();
        }
    }
}