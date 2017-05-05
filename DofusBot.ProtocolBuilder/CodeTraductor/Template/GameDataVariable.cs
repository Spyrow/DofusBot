using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;
using DofusBot.ProtocolBuilder.CodeTraductor.Dictionary;

namespace DofusBot.ProtocolBuilder.CodeTraductor.Template
{
    public class GameDataVariable
    {
        public MemberAttributes Attributes { get; set; } 
        public string VariableType
        {
            get;
            set;
        }
        private string mVariableName;
        public string VariableName
        {
            get
            {
                return mVariableName;
            }
            set
            {
                string v = value;
                switch (value.Trim().ToLower())
                {
                    case "operator":
                        v = "operator_";
                        break;
                    case "url":
                        v = "link";
                        break;
                }
                mVariableName = v;
            }
        }
        public string PrivateVariableName
        {
            get
            {
                return "m" + VariableName.Substring(0, 1).ToUpper() + VariableName.Substring(1, VariableName.Length - 1);
            }
        }


        public string PublicVariableName
        {
            get
            {
                return VariableName.Substring(0, 1).ToUpper() + VariableName.Substring(1, VariableName.Length - 1);
            }
        }

        public bool IsVector
        {
            get
            {
                if (VariableType.Contains("List<"))
                    return true;
                else
                    return false;
            }
        }

        public bool IsPrimitive
        {
            get
            {
                if (IsSubVector || IsVector)
                    return PrimitiveType.singleton.ContainsKey(VectorType);
                else
                    return PrimitiveType.singleton.ContainsKey(VariableType);
            }
        }

        public bool IsSubVector
        {
            get
            {
                if (VariableType.Contains("List<List"))
                    return true;
                else
                    return false;
            }
        }


        public string VectorType
        {
            get
            {
                if (IsSubVector)
                {
                    return VariableType.Substring(10, VariableType.Length - 12);
                }
                else
                {
                    return VariableType.Substring(5, VariableType.Length - 6);
                }
            }
        }


        public GameDataVariable(string variablename, string variabletype)
        {
            VariableName = variablename;
            VariableType = variabletype.Replace(";", "");
        }

        public void SetVariableType(string type)
        {
            VariableType = type;
        }
    }
}
