using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DofusBot.ProtocolBuilder.CodeTraductor.Dictionary;

namespace DofusBot.ProtocolBuilder.CodeTraductor.Cleaning
{
    public class MessageCleaner
    {
        #region Declarations

        private string oldClassStr;
        private string newClassStr;
        private string filePath;

        #endregion

        #region Constructeur

        public MessageCleaner(string FilePath)
        {
            filePath = FilePath;
            StreamReader reader = new StreamReader(new FileStream(filePath, FileMode.Open));
            oldClassStr = reader.ReadToEnd();
            reader.Close();
            CleanSource();
            Save();
        }

        public MessageCleaner(string file,bool save)
        {
            oldClassStr = file;
            CleanSource();
        }

        #endregion

        #region Public method

        public string GetCleanedSource()
        {
            return newClassStr;
        }

        #endregion

        #region Private method

        private void CleanSource()
        {
            string[] lines = oldClassStr.Split((char)10);

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                if (RegularExpression.GetRegex(RegexEnum.If).Match(line).Success || RegularExpression.GetRegex(RegexEnum.Else).Match(line).Success)
                {
                    lines[i] = "";

                    int openBarakCount = 0;
                    for (int subIndex = i; subIndex < lines.Length; subIndex++)
                    {
                        if (lines[subIndex].Trim() == "{")
                        {
                            lines[subIndex] = "";
                            openBarakCount++;
                        } 
                        if (lines[subIndex].Trim() == "}")
                        {
                            lines[subIndex] = "";
                            openBarakCount--;
                            if (openBarakCount <= 0)
                                break;
                        }
                        if (lines[subIndex].Trim() == "continue;")
                        {
                            lines[subIndex] = "";
                        }
                        if (lines[subIndex].Trim() == "return;")
                        {
                            lines[subIndex] = "";
                        }
                        if (RegularExpression.GetRegex(RegexEnum.ThrowError).Match(lines[subIndex]).Success)
                        {
                            lines[subIndex] = "";
                        }
                    }
                }
            }

            foreach (string line in lines)
            {
                if (line.Trim() != "")
                    newClassStr += line + ((char)10);
            }
        }

        private void Save()
        {
            StreamWriter writer = new StreamWriter(new FileStream(filePath, FileMode.Truncate));
            writer.Write(newClassStr);
            writer.Close();
        }

        #endregion
    }
}
