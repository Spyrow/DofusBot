using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DofusBot.ProtocolBuilder.CodeTraductor.Identification;

namespace DofusBot.ProtocolBuilder.CodeTraductor.BulkGenerator
{
    public class BulkGenerator
    {
        public static event EventHandler<LoadInfoEventArgs> LoadInfo;
        public static event EventHandler FileTranslated;
        public static event Action<string> StatsChang;

        private static void OnStatsChang(string value)
        {
            if (StatsChang != null)
                StatsChang(value);
        }

        private static void OnLoadInfo(int value)
        {
            if (LoadInfo != null)
                LoadInfo(new object(), new LoadInfoEventArgs(value));
        }

        private static void OnFileTranslated()
        {
            if (FileTranslated != null)
                FileTranslated(new object(), new LoadInfoEventArgs(0));
        }

        public static void GenerateDirectory(string inputPath, string outputPath)
        {
            ClassIdent identificator = new ClassIdent();
            DirectoryInformations directoryInfos = new DirectoryInformations(inputPath);
            identificator.SortDirectory(directoryInfos.Files.ToArray());

            OnStatsChang("Traduction des gameData...");
            GameDataBulkGenerator gbg = new GameDataBulkGenerator();
            gbg.FileTranslated += eFileTranslated;
            gbg.LoadInfo += eLoadInfo;
            gbg.GenerateDirectory(identificator.GameDatas, inputPath, outputPath);

            OnStatsChang("Nettoyage des messages et types...");
            BulkCleaner bc = new BulkCleaner();
            bc.FileTranslated += eFileTranslated;
            bc.LoadInfo += eLoadInfo;
            bc.CleanDirectory(identificator.TypesOrMessages);

            OnStatsChang("Traduction des messages et types...");
            NetworkulkGenerator mbg = new NetworkulkGenerator();
            mbg.FileTranslated += eFileTranslated;
            mbg.LoadInfo += eLoadInfo;
            mbg.GenerateDirectory(identificator.TypesOrMessages, inputPath, outputPath);


            OnStatsChang("Traduction des enums...");
            EnumBulkGenerator ebg = new EnumBulkGenerator();
            ebg.LoadInfo += eLoadInfo;
            ebg.FileTranslated += eFileTranslated;
            ebg.GenerateDirectory(identificator.Enums, inputPath, outputPath);

            OnStatsChang("Traduction terminer");
        }

        static void eLoadInfo(object sender, LoadInfoEventArgs e)
        {
            OnLoadInfo(e.FilesCount);
        }

        static void eFileTranslated(object sender, EventArgs e)
        {
            OnFileTranslated();
        }
    }
}
