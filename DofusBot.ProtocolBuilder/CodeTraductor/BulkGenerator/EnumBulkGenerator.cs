
using DofusBot.ProtocolBuilder.CodeTraductor.Generator;
using DofusBot.ProtocolBuilder.CodeTraductor.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusBot.ProtocolBuilder.CodeTraductor.BulkGenerator
{
    public class EnumBulkGenerator
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
            if (!(Directory.Exists(inputPath)))
                Directory.CreateDirectory(inputPath);
            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);
            OnLoadInfo(inputDirectory.Count);

            foreach (string str in inputDirectory)
            {
                string[] splited = str.Split('\\');
                string directory = "";

                for (int i = 0; i < splited.Length - 1; i++)
                    directory += splited[i] + "\\";
                directory = directory.Remove(directory.Length - 1);
                directory = directory.Replace(inputPath, outputPath+"\\Enums");
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
             //   try
             //   {

                    EnumParser parser = new EnumParser(File.ReadAllText(str, new UTF8Encoding()));
                    EnumGenerator generator = new EnumGenerator(parser.GetEnum(), outputPath);
               // }
               // catch (Exception e)
               // {
                 //   Console.WriteLine("Error to translate : " + str);
                //}
                OnFileTranslated();
            }
        }

        #endregion

    }
}
