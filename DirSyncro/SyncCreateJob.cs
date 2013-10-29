using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DirSyncro
{
    class SyncCreateJob : SyncJob
    {
        public SyncCreateJob(SyncMessage syncMessage) : base(syncMessage) {}

        public void Execute(Object context)
        {
            try
            {
                if (Run())
                {
                    CopyFile();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
