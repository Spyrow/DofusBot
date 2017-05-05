
using DofusBot.ProtocolBuilder.CodeTraductor.BulkGenerator;
using DofusBot.ProtocolBuilder.CodeTraductor.Dictionary;
using DofusBot.ProtocolBuilder.CodeTraductor.Template;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DofusBot.ProtocolBuilder.CodeTraductor.Parsing
{
    public class GameDataParser
    {
        #region Declarations

        private StreamReader mReader;
        private GameDataClass mOutputClass;
        private object mDefinition;

        #endregion

        #region Public method

        public GameDataClass GetClass()
        {
            return mOutputClass;
        }

        public GameDataParser(string filePath,string directory)
        {
            mOutputClass = new GameDataClass();
            mOutputClass.BasePath = directory;
            mReader = new StreamReader(filePath);
            mOutputClass.Path = filePath;
            Parse();
        }

        #endregion

        public void Parse()
        {
            GetImport();

            GetNamespace();
            GetClassName();
         
            GetVariables();
            ParseMethod();
        }

        private void GetImport()
        {
            mOutputClass.Import.Add("System.Collections.Generic");
            mOutputClass.Import.Add("System");
        }

        private void GetNamespace()
        {
            mOutputClass.Namespace = "DofusBot.Protocol.DataCenter";
        }

        private void GetClassName()
        {
            if (mOutputClass.Path.ToLower().Contains("effectinstance"))
            { }
            mReader.BaseStream.Seek(0, SeekOrigin.Begin);
            string line = mReader.ReadLine();
            while(!line.Contains("public class")&&!mReader.EndOfStream )
            {
                if (!mReader.EndOfStream)
                    line = mReader.ReadLine();
                else
                    break;
            }
            if (mReader.EndOfStream)
                throw new Exception("Invalide data class file");
            Match m = RegularExpression.GetRegex(RegexEnum.Class).Match(line);

                if (m.Groups["parent"].Value.Trim() != "Object")
                    mOutputClass.Parent = m.Groups["parent"].Value;
                if (m.Groups["interface2"].Value != "")
                {
                    if (m.Groups["interface"].Value == "IDataCenter")
                        mOutputClass.Interface = m.Groups["interface2"].Value;
                    else
                        mOutputClass.Interface = m.Groups["interface"].Value;
                }
            mOutputClass.ClassName = m.Groups["name"].Value;
        }

        private void ParseLine(string line)
        {
            if (line.Contains("Vector"))
            {
                string[] spliter = line.Split(':');
                string variableName = spliter[0].Substring(spliter[0].LastIndexOf(" ")).Replace("_", "").Replace(" ", "");
                string variableType = spliter[1].Replace("Vector.", "List").Replace(";", "");
                mOutputClass.Variables.Add(new GameDataVariable(variableName, variableType));
            }
            else
            {
                string[] spliter = line.Split(':');
                GameDataVariable gdtv = new GameDataVariable(spliter[0].Substring(spliter[0].LastIndexOf(" ") + 1).Replace("_", ""), spliter[1]);
                if (gdtv.IsPrimitive && !gdtv.IsVector && gdtv.VariableType.ToLower() == "uint")
                {
                    
                }
                mOutputClass.Variables.Add(gdtv);
            }
        }
        private void GetVariables()
        {
            string line = mReader.ReadLine();
            while (true)
            {
                if (RegularExpression.GetRegex(RegexEnum.GameDataVar).IsMatch(line))
                {
                    ParseLine(line);
                }
                if (!mReader.EndOfStream)
                {
                    line = mReader.ReadLine();
                }
                else
                    break;
            }
        }

        private void ParseMethod()
        {
            string line = mReader.ReadLine();

            while (!mReader.EndOfStream)
            {
                if (line.Contains("public function get ") && line.Contains(": String"))
                {
                    ParseI18nMethod();
                }
                line = mReader.ReadLine();
            }
        }

        private void ParseI18nMethod()
        {
            mReader.ReadLine();
            mReader.ReadLine();
            mReader.ReadLine();
            string line = mReader.ReadLine();
            if (!line.Contains("I18n.getText("))
                return;
            string[] spliter = line.Split('=');
            string variableName = spliter[1].Replace(" I18n.getText(", "").Replace(");", "").Replace("this.", "").Replace("_", "");
            mOutputClass.Variables.Add(new GameDataVariable(variableName.Replace("Id", ""), "I18n, " + variableName));
        }
    }
}
