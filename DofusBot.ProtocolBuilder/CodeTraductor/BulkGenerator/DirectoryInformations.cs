using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Specialized;

namespace DofusBot.ProtocolBuilder.CodeTraductor.BulkGenerator
{
    class DirectoryInformations
    {
        public List<string> Files;

        public DirectoryInformations(string directory,string ext = "*.as")
        {
            Files = listAllFiles(new List<string>(), directory, ext, true);
        }

        public DirectoryInformations()
        {

        }

        private List<string> listAllFiles(List<string> allFiles, string path, string ext, bool scanDirOk)
        {
            string[] listFilesCurrDir = Directory.GetFiles(path, ext);
            foreach (string rowFile in listFilesCurrDir)
                if (allFiles.Contains(rowFile) == false)
                    allFiles.Add(rowFile);
            listFilesCurrDir = null;

            if (scanDirOk)
            {
                string[] listDirCurrDir = Directory.GetDirectories(path);
                if (listDirCurrDir.Length != 0)
                    foreach (string rowDir in listDirCurrDir) 
                        listAllFiles(allFiles, rowDir, ext, scanDirOk);
                listDirCurrDir = null;

            }
            return allFiles;
        }

    }
}
