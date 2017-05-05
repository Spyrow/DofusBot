using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DofusBot.ProtocolBuilder.CodeTraductor.Cleaning;

namespace DofusBot.ProtocolBuilder.CodeTraductor.BulkGenerator
{
    public class BulkCleaner
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

        #region Public method

        public void CleanDirectory(List<string> directory)
        {
            OnLoadInfo(directory.Count);

            foreach (string file in directory)
            {
                new MessageCleaner(file);
                OnFileTranslated();
            }
        }

        #endregion
    }
}
