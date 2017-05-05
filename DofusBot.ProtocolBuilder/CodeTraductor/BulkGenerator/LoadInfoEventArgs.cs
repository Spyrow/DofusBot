using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusBot.ProtocolBuilder.CodeTraductor.BulkGenerator
{
    public class LoadInfoEventArgs : EventArgs
    {
        public int FilesCount { get; private set; }

        public LoadInfoEventArgs(int filesCount)
        {
            FilesCount = filesCount;
        }
    }
}
