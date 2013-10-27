//using System;
//using System.IO;
//using System.Threading;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DirSyncro
//{
//    class SyncCreateJob : SyncJob
//    {
//        public SyncCreateJob(string eventType, string sourceBase, string sourceName, string[] targetBases, int versions)
//        {
//            this.eventType = eventType;
//            this.sourceBase = sourceBase;
//            this.sourceName = sourceName;
//            this.sourceFull = sourceBase + "\\" + sourceName;
//            this.targetBases = targetBases;
//            this.versions = versions;
//            this.started = false;
//            this.finished = false;
//            this.jobAfter = null;
//            this.jobBefore = null;
//        }

//        public override void Execute(Object context)
//        {
//            SyncCreateJob job = (SyncCreateJob)context;

//            job.started = true;

//            try
//            {
//                DirectoryInfo fullTarget = null;

//                if (File.Exists(sourceFull))
//                {
//                    foreach (string target in targetBases)
//                    {
//                        if (!Directory.Exists(target)) continue;

//                        fullTarget = new FileInfo(target + "\\" + sourceName).Directory;
//                        CreatePath(fullTarget);

//                        // Construct filename with timestamp
//                        String newFullTarget = String.Format("{0}\\{1}_{2}{3}", fullTarget.FullName, Path.GetFileNameWithoutExtension(sourceFull), CreateTimestamp(), Path.GetExtension(sourceFull));
//                        File.Copy(sourceFull, newFullTarget, false);
//                    }
//                }
//                else if (Directory.Exists(sourceFull))
//                {
//                    foreach (string target in targetBases)
//                    {
//                        if (!Directory.Exists(target)) continue;

//                        fullTarget = new DirectoryInfo(target + "\\" + sourceName);
//                        CreatePath(fullTarget);
//                    }
//                }
//            }
//            catch (IOException ex)
//            {
//                Console.WriteLine(ex.StackTrace);
//            }

//            Console.WriteLine(String.Format("Thread: {2}, Event: {0}, Fullpath: {1}", job.eventType, job.sourceFull, Thread.CurrentThread.ManagedThreadId.ToString()));

//            job.finished = true;
//        }
//    }
//}
