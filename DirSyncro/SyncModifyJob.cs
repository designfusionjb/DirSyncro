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
    class SyncModifyJob : SyncJob
    {
        public SyncModifyJob(SyncMessage syncMessage) : base(syncMessage) { }

        protected override bool Run()
        {
            bool run = base.Run();

            // TODO fix retention on runHistory
            /// Don't run sync if there has been a sync run on the same sourcefile within the settling time
            lock (runHistory)
            {
                for (int i = 0; i < runHistory.Count; i++)
                {
                    if (syncMessage.changeType.Equals(WatcherChangeTypes.Changed) &&
                        syncMessage.sourceFile.FullName.Equals(runHistory[i].sourceFile.FullName) &&
                        (syncMessage.modifiedTime.Equals(runHistory[i].modifiedTime) ||
                            syncMessage.timeStamp - runHistory[i].timeStamp < syncMessage.settling))
                    {
                        Console.WriteLine("SKIP: \"{0}\" was already run.", syncMessage.sourceFile);
                        return false;
                    }
                }
                runHistory.Add(syncMessage);
            }

            return run;
        }

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
