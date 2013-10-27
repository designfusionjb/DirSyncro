using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirSyncro
{
    class SyncModifyJob
    {
        public SyncModifyJob()
        {
        }

        public void Execute(Object context)
        {
            SyncMessage syncMessage = (SyncMessage)context;

            try
            {
                DirectoryInfo fullTarget = null;

                if (syncMessage.sourceFile.Exists) { 
                    syncMessage.partialPath = syncMessage.sourceFile.ToString().Replace(syncMessage.sourcePath.ToString(), syncMessage.targetPath.ToString())
                        fullTarget = new FileInfo(syncMessage.targetPath + "\\" + sourceName).Directory;
                //        CreatePath(fullTarget);

                //        string searchPattern = String.Format("{0}\\{1}_*{2}", fullTarget.FullName, Path.GetFileNameWithoutExtension(sourceFull), Path.GetExtension(sourceFull));
                //        FileInfo[] fileVersions = fullTarget.GetFiles(searchPattern, SearchOption.TopDirectoryOnly);

                //        if (fileVersions.Length > versions)
                //        {
                //            // Sort array so that the oldes file is first in list
                //            Array.Sort(fileVersions, (f1, f2) => f1.Name.CompareTo(f2.Name));

                //            for (int i = 0; i < fileVersions.Length - versions + 1; i++)
                //            {
                //                fileVersions[i].Delete();
                //            }
                //        }

                //        // Construct filename with timestamp
                //        String newFullTarget = String.Format("{0}\\{1}_{2}{3}", fullTarget.FullName, Path.GetFileNameWithoutExtension(sourceFull), CreateTimestamp(), Path.GetExtension(sourceFull));
                //        File.Copy(sourceFull, newFullTarget, false);
                //    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            //Console.WriteLine(String.Format("Thread: {2}, Event: {0}, Fullpath: {1}", job.eventType, job.sourceFull, Thread.CurrentThread.ManagedThreadId.ToString()));

            //job.finished = true;
        }
    }
}
