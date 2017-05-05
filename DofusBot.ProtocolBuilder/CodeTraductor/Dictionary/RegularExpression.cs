using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DofusBot.ProtocolBuilder.CodeTraductor.Dictionary
{
    public class RegularExpression:Dictionary<RegexEnum,Regex>
    {
        private static RegularExpression singleton = new RegularExpression();

        public RegularExpression()
        {
            Add(RegexEnum.Class, new Regex(@"public\s*[final]*\s+class\s+(?<name>[\w]+)\s*[extends]*\s*(?<parent>[\w]*)\s+[implements\s+]*(?<interface>[\w]*)\s*,*\s*(?<interface2>[\w]*)", RegexOptions.Multiline));
            Add(RegexEnum.Var, new Regex(@"^\s*(?<porte>public|private)\s+var\s+(?<name>[\w]+)\s*:\s*(?<type>[\w|.|<|>]+)", RegexOptions.Multiline));
            Add(RegexEnum.VarObject, new Regex(@"^\s*public\s*var\s*(?<name>[\w]+)\s*:\s*(?<type>[\w]+)\s*;\s*$", RegexOptions.Multiline));
            Add(RegexEnum.VarPrimitive, new Regex(@"^\s*(?<porte>public|private)\s+var\s+(?<name>[\w]+)\s*:\s*(?<type>String|Boolean|int|Number|uint|byte)\s*=\s*", RegexOptions.Multiline));
            Add(RegexEnum.VarVector, new Regex(@"^\s*(?<porte>public|private)\s+var\s+(?<name>[\w]+)\s*:\s*Vector.\s*<\s*(?<type>[\w]+)\s*>\s*;\s*$", RegexOptions.Multiline));
            Add(RegexEnum.ConstPrimitive, new Regex(@"^\s*public\s*static\s*const\s*(?<name>[\w]+)\s*:\s*(?<type>[\w]+)\s*=\s*(?<value>[\d]+)\s*;\s*$", RegexOptions.Multiline));
            Add(RegexEnum.ReadMetodePrimitive, new Regex(@"^\s*this.(?<name>[\w]+)\s*=\s*param1.(?<methode>[\w]+)\(\);", RegexOptions.Multiline));
            Add(RegexEnum.ReadMethodeObject, new Regex(@"^\s*this.(?<name>[\w]+).deserialize\([\w]+\);\s*$"));
            Add(RegexEnum.ReadVectorMetodePrimitive, new Regex(@"\s*_\w+_\s*=\s*param1.(?<methode>[\w]+)\(\)\s*;\s*\n\s*this.(?<name>[\w]+).push\(_\w+_\)\s*;\s*", RegexOptions.Multiline));
            Add(RegexEnum.ReadVectorMetodeObject, new Regex(@"^\s*_\w+_\s*=\s*new\s+(?<type>[\w]+)\(\)\s*;\s*\n\s*_\w+_.deserialize\(\s*param1\s*\)\s*;\s*\n\s*this.(?<name>[\w]+).push\(_\w+\)\s*;\s*$", RegexOptions.Multiline));
            Add(RegexEnum.ReadVectorMetodePotocolManager, new Regex(@"\s*_[\w]+_\s*=\s*param1.readUnsignedShort\(\);\s*\n\s*_\w+_\s*=\s*ProtocolTypeManager.getInstance\((?<type>[\w]+)\s*,\s*_\w+_\s*\)\s*;\s*\n\s*this.(?<name>[\w]+).push\(_\w+_\)\s*;\s*", RegexOptions.Multiline));
            Add(RegexEnum.ReadFlagMetode, new Regex(@"^\s*this.(?<name>[\w]+)\s*=\s*BooleanByteWrapper.getFlag\(_\w+_\s*,\s*(?<flag>[\d]+)\s*\)\s*;\s*$"));
            Add(RegexEnum.NameSpace, new Regex(@"^\s*package\s+(?<name>[\w|\.]+)\s*$",RegexOptions.Multiline));
            Add(RegexEnum.Import, new Regex(@"^\s*import\s+(?<name>[\w|\.]+)\s*;\s*$", RegexOptions.Multiline));
            Add(RegexEnum.EnumItem, new Regex(@"^\s*public\s+static\s+const\s+(?<name>[\w|_]+)\s*:\s*(?<type>[\w]+)\s*=\s*(?<value>[\d|\w]+)\s*;\s*$", RegexOptions.Multiline));
            Add(RegexEnum.Enum, new Regex(@"^\s*public\s+class\s+(?<name>[\w]+)\s+extends\s+(?<parent>[\w]+)\s*$", RegexOptions.Multiline));
            Add(RegexEnum.If, new Regex(@"^\s*if\s*\([\w|\[|\]|\d|\<|\>|\.|\s|\=]+\s*\)\s*$"));
            Add(RegexEnum.Else, new Regex(@"^\s*else\s*$"));
            Add(RegexEnum.ThrowError, new Regex(@"^\s*throw\s+new\s+Error\s*\("));
            Add(RegexEnum.GameDataVar, new Regex(@"^\s*public\s+var\s+(?<name>[\w|_|\d]+)\s*:\s*(?<type>[\w|\d|.|\<|\>]+)\s*;\s*$"));
            Add(RegexEnum.ReadMethodObjectProtocolManager, new Regex(@"^\s*this.(?<name>[\w]+)\s*=\s*ProtocolTypeManager.getInstance\(\s*(?<type>[\w]+)\s*,\s*[_|\w]+\)\s*;\s*$"));
        }

        public static Regex GetRegex(RegexEnum key)
        {
            return singleton[key];
        }
    }

    public enum RegexEnum
    {
        Class,
        NameSpace,
        Import,
        GameDataVar,
        Var,
        VarVector,
        VarObject,
        VarPrimitive,
        ConstPrimitive,
        ReadMetodePrimitive,
        ReadMethodeObject,
        ReadMethodObjectProtocolManager,
        ReadVectorMetodePrimitive,
        ReadVectorMetodeObject,
        ReadVectorMetodePotocolManager,
        ReadFlagMetode,
        Enum,
        EnumItem,
        If,
        Else,
        ThrowError,
    }
}
