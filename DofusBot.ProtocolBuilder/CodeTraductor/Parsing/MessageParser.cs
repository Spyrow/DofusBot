using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DofusBot.ProtocolBuilder.CodeTraductor.Template;
using DofusBot.ProtocolBuilder.CodeTraductor.Dictionary;
using System.Text.RegularExpressions;
using DofusBot.ProtocolBuilder.CodeTraductor.Enums;

namespace DofusBot.ProtocolBuilder.CodeTraductor.Parsing
{
    public class MessageParser
    {
        private string fileToTranslat;
        private Class classTranslated;

        public MessageParser(string fileStr)
        {
            fileToTranslat = fileStr;
            Parse();
        }

        public Class GetClass()
        {
            return classTranslated;
        }

        private void Parse()
        {
            ParseImports();
            ParseClassInformations();
            ParseClassConsts();
            ParseClassVars();
            ParseClassReadOrWriteMethods();
            classTranslated.Variables = ParserUtility.SortVars(classTranslated.Variables, fileToTranslat);
        }

        #region Parse methode

        private void ParseImports()
        {
            List<string> imports = new List<string>();
            Match m = RegularExpression.GetRegex(RegexEnum.Import).Match(fileToTranslat);
            while (m.Success)
            {
                imports.Add(m.Groups["name"].Value);
                m = m.NextMatch();
            }
            classTranslated.Imports = ParserUtility.GetImports(imports.ToArray());
        }

        private void ParseClassInformations()
        {
            Match m = RegularExpression.GetRegex(RegexEnum.Class).Match(fileToTranslat);
            if (!m.Success)
                throw new Exception("The class is not a valide as3 class");
            classTranslated.Name = m.Groups["name"].Value;
            classTranslated.Parent = ParserUtility.GetIParent(m.Groups["parent"].Value, m.Groups["interface"].Value);
            classTranslated.Interface = "";
            m = RegularExpression.GetRegex(RegexEnum.ConstPrimitive).Match(fileToTranslat);
            if (m.Success)
                classTranslated.ProtocolId = Convert.ToInt32(m.Groups["value"].Value);
            m = RegularExpression.GetRegex(RegexEnum.NameSpace).Match(fileToTranslat);
            if (m.Success)
                classTranslated.NameSpace = ParserUtility.GetNameSpace(m.Groups["name"].Value);
            else
                throw new Exception("The class is not a valide as3 class");
        }

        private void ParseClassVars()
        {
            List<Variable> Variables = new List<Variable>();
            Match m;
            //var object
             m = RegularExpression.GetRegex(RegexEnum.VarObject).Match(fileToTranslat);
            while (m.Success)
            {
                Variable newVar = new Variable();
                newVar.TypeOfVar = VarType.Object;
                newVar.MethodeType = ReadMethodeType.SerializeOrDeserialize;
                newVar.Name = m.Groups["name"].Value;
                newVar.ObjectType = m.Groups["type"].Value;
                newVar.ReadMethode = "Deserialize";
                newVar.WriteMethode = "Serialize";
                Variables.Add(newVar);
                m = m.NextMatch();
            }
            //var primitive
            m = RegularExpression.GetRegex(RegexEnum.VarPrimitive).Match(fileToTranslat);
            while (m.Success)
            {
                Variable newVar = new Variable();
                newVar.TypeOfVar = VarType.Primitive;
                newVar.Name = m.Groups["name"].Value;
                Variables.Add(newVar);
                m = m.NextMatch();
            }
            //var vector
            m = RegularExpression.GetRegex(RegexEnum.VarVector).Match(fileToTranslat);
            while (m.Success)
            {
                Variable newVar = new Variable();
                newVar.TypeOfVar = VarType.Vector;
                newVar.Name = m.Groups["name"].Value;
                Variables.Add(newVar);
                m = m.NextMatch();
            }

            classTranslated.Variables = Variables.ToArray();
        }

        private void ParseClassConsts()
        {
            
        }

        #region Read methode

        private void ParseClassReadOrWriteMethods()
        {
            ReadPrimitiveVectorMethode();
            ReadObjectVectorMethode();
            ReadVectorProtocoelTypeManagerMethode();
            ReadObjectMethode();
            ReadPrimitiveMethode();
        }

        private void ReadObjectMethode()
        {
            Match m = RegularExpression.GetRegex(RegexEnum.ReadMethodeObject).Match(fileToTranslat);
            while (m.Success)
            {
                for (int i = 0; i < classTranslated.Variables.Length; i++)
                {
                    if (classTranslated.Variables[i].Name == m.Groups["name"].Value)
                    {
                        classTranslated.Variables[i].MethodeType = ReadMethodeType.SerializeOrDeserialize;
                        classTranslated.Variables[i].ReadMethode = "Deserialize";
                        classTranslated.Variables[i].WriteMethode = "Serialize";
                    }
                }
                m = m.NextMatch();
            }
        }

        private void ReadPrimitiveMethode()
        {
            Match m = RegularExpression.GetRegex(RegexEnum.ReadMetodePrimitive).Match(fileToTranslat);
            while (m.Success)
            {
                for (int i = 0; i < classTranslated.Variables.Length; i++)
                {
                    if (classTranslated.Variables[i].Name == m.Groups["name"].Value)
                    {
                        classTranslated.Variables[i].MethodeType = ReadMethodeType.Primitive;
                        classTranslated.Variables[i].ReadMethode = ReadOrWriteMethode.GetReadMethode(m.Groups["methode"].Value);
                        classTranslated.Variables[i].WriteMethode = ReadOrWriteMethode.GetWriteMethode(m.Groups["methode"].Value);
                        classTranslated.Variables[i].Type = PrimitiveType.GetTypeByReadMethode(classTranslated.Variables[i].ReadMethode);
                    }
                }
                m = m.NextMatch();
            }
        }

        private void ReadVectorProtocoelTypeManagerMethode()
        {
            Match m = RegularExpression.GetRegex(RegexEnum.ReadVectorMetodePotocolManager).Match(fileToTranslat);
            while (m.Success)
            {
                for (int i = 0; i < classTranslated.Variables.Length; i++)
                {
                    if (classTranslated.Variables[i].Name == m.Groups["name"].Value)
                    {
                        classTranslated.Variables[i].ObjectType = m.Groups["type"].Value;
                        classTranslated.Variables[i].MethodeType = ReadMethodeType.ProtocolTypeManager;
                    }
                }
                m = m.NextMatch();
            }
        }

        private void ReadObjectVectorMethode()
        {
            Match m = RegularExpression.GetRegex(RegexEnum.ReadVectorMetodeObject).Match(fileToTranslat);
            while (m.Success)
            {
                for (int i = 0; i < classTranslated.Variables.Length; i++)
                {

                    if (classTranslated.Variables[i].Name == m.Groups["name"].Value)
                    {
                        classTranslated.Variables[i].ReadMethode = "Deserialize";
                        classTranslated.Variables[i].WriteMethode = "Serialize";
                        classTranslated.Variables[i].ObjectType = m.Groups["type"].Value;
                        classTranslated.Variables[i].MethodeType = ReadMethodeType.SerializeOrDeserialize;
                    }
                }
                m = m.NextMatch();
            }
        }

        private void ReadPrimitiveVectorMethode()
        {
            Match m = RegularExpression.GetRegex(RegexEnum.ReadVectorMetodePrimitive).Match(fileToTranslat);
            while (m.Success)
            {
                for (int i = 0; i< classTranslated.Variables.Length;i++)
                {
                    if (classTranslated.Variables[i].Name == m.Groups["name"].Value)
                    {
                        classTranslated.Variables[i].ReadMethode = ReadOrWriteMethode.GetReadMethode(m.Groups["methode"].Value);
                        classTranslated.Variables[i].WriteMethode = ReadOrWriteMethode.GetWriteMethode(m.Groups["methode"].Value);
                        classTranslated.Variables[i].Type = PrimitiveType.GetTypeByReadMethode(classTranslated.Variables[i].ReadMethode);
                        classTranslated.Variables[i].MethodeType = ReadMethodeType.Primitive;
                    }
                }
                m = m.NextMatch();
            }
        }

        #endregion

        #endregion
    }
}
