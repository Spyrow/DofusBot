using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DofusBot.ProtocolBuilder.CodeTraductor.Parsing;
using DofusBot.ProtocolBuilder.CodeTraductor.Generator;

namespace DofusBot.ProtocolBuilder.CodeTraductor.BulkGenerator
{
    public class NetworkulkGenerator
    {
        #region evenement

        public event EventHandler<LoadInfoEventArgs> LoadInfo;
        public event EventHandler FileTranslated;

        private void OnLoadInfo(int value)
        {
            if (LoadInfo != null)
                LoadInfo(this, new LoadInfoEventArgs(value));
        }

        private void OnFileTranslated()
        {
            if (FileTranslated != null)
                FileTranslated(this, new LoadInfoEventArgs(0));
        }

        #endregion

        #region public methode

        public void GenerateDirectory(List<string> inputDirectory, string inputPath, string outputPath)
        {
            OnLoadInfo(inputDirectory.Count);

            foreach (string str in inputDirectory)
            {
                string[] splited = (str.Replace(inputPath, outputPath)).Split('\\');
                string directory = "";

                for (int i = 0; i < splited.Length - 1; i++)
                    directory += splited[i].Substring(0, 1).ToUpper() + splited[i].Remove(0, 1) + "//";
                directory = directory.Remove(directory.Length - 1);
                directory = directory.Replace(inputPath, outputPath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                try
                {
  MessageParser parser = new MessageParser(File.ReadAllText(str, new UTF8Encoding()));
                    MessageGenerator generator = new MessageGenerator(parser.GetClass(), directory);
                }
                catch
                {
                  
                }

                OnFileTranslated();
            }
        }

        #endregion
    }
}