using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DirSyncro
{
    abstract class SyncJob
    {
        protected SyncMessage syncMessage;
        protected static List<SyncMessage> runHistory = new List<SyncMessage>();
        private static Queue<SyncMessage> backLog = new LimitedQueue<SyncMessage>(1000);

        public SyncJob(SyncMessage syncMessage)
        {
            this.syncMessage = syncMessage;
        }

        protected string CreateEpoch()
        {
            return Math.Round((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString();
        }

        protected DateTime EpochToDateTime(long seconds)
        {
            return new DateTime(1970, 1, 1).AddSeconds(seconds);
        }

        protected string GetSourceFileFromTargetFile(string targetFile)
        {
            return targetFile.Substring(0, Path.GetFileNameWithoutExtension(targetFile).LastIndexOf('_')) + Path.GetExtension(targetFile);
        }

        protected DateTime GetBackupTime(string backupFile)
        {
            int backupTimeIndex = Path.GetFileNameWithoutExtension(backupFile).LastIndexOf('_');

            return EpochToDateTime(long.Parse(Path.GetFileNameWithoutExtension(backupFile).Substring(backupTimeIndex, Path.GetFileNameWithoutExtension(backupFile).Length - backupTimeIndex)));
        }

        protected bool Run()
        {
            bool run = true;

            /// Don't run filtered jobs (Don't even add them to run history
            if (syncMessage.includeList != null)
            {
                bool isFound = false;
                foreach (Regex regex in syncMessage.includeList)
                {
                    if (regex.IsMatch(syncMessage.sourceFile.Name))
                    {
                        isFound = true;
                        break;
                    }
                }
                if (!isFound)
                {
                    Console.WriteLine("SKIP: \"{0}\" is filtered.", syncMessage.sourceFile);
                    run = false;
                }
            }
            else if (syncMessage.excludeList != null)
            {
                foreach (Regex regex in syncMessage.excludeList)
                {
                    if (regex.IsMatch(syncMessage.sourceFile.Name))
                    {
                        Console.WriteLine("SKIP: \"{0}\" is filtered.", syncMessage.sourceFile);
                        run = false;
                        break;
                    }
                }
            }

            if (run && syncMessage.changeType.Equals(WatcherChangeTypes.Changed))
            {
                lock (runHistory)
                {
                    for (int i = 0; i < runHistory.Count; i++)
                    {
                        if (run &&
                            syncMessage.sourceFile.FullName.Equals(runHistory[i].sourceFile.FullName) &&
                            (syncMessage.sourceFile.LastWriteTimeUtc.Equals(runHistory[i].sourceFile.LastWriteTimeUtc) ||
                                syncMessage.timeStamp - runHistory[i].timeStamp < syncMessage.settling))
                        {
                            Console.WriteLine("SKIP: \"{0}\" was already run.", syncMessage.sourceFile);
                            run = false;
                        }
                        if (DateTime.Now - runHistory[i].timeStamp > syncMessage.settling)
                        {
                            runHistory.RemoveAt(i);
                            i--;
                        }
                    }
                    if (run) runHistory.Add(syncMessage);
                }
            }

            if (run && !syncMessage.targetPath.Exists)
            {
                lock (backLog)
                {
                    backLog.Enqueue(syncMessage);
                }
                Console.WriteLine("CREATED: \"{0}\" to backlog.", syncMessage.sourceFile);

                return false;
            }

            return run;
        }

        protected void CreatePath(DirectoryInfo fullTarget)
        {
            if (!fullTarget.Exists)
            {
                if (!fullTarget.Parent.Exists)
                    CreatePath(fullTarget.Parent);

                fullTarget.Create();
            }
        }

        protected void CopyFile()
        {
            string partialPath = syncMessage.sourceFile.FullName.Replace(syncMessage.sourcePath.FullName, "");
            
            // Construct the partial target path directory
            DirectoryInfo fullTarget = null;
            if (string.IsNullOrEmpty(partialPath))
            {
                fullTarget = syncMessage.targetPath;
            }
            else
            {
                fullTarget = new DirectoryInfo(syncMessage.targetPath.FullName + Path.DirectorySeparatorChar + partialPath);
            }

            // Verify target path and create if necessary
            CreatePath(fullTarget);

            List<FileInfo> fileVersions = fullTarget.GetFiles("*.*", SearchOption.TopDirectoryOnly)
                .Where(path => syncMessage.sourceFile.Name == GetSourceFileFromTargetFile(path.Name)).ToList();

            // Construct filename with timestamp and copy source file to target directory
            string newFullTarget = String.Format("{0}\\{1}_{2}{3}", fullTarget.FullName, Path.GetFileNameWithoutExtension(syncMessage.sourceFile.Name), CreateEpoch(), syncMessage.sourceFile.Extension);

            // Only copy file to target if last modified time is newer or the file sizes differ (could mean interrupted file copy)
            if (fileVersions.Count == 0)
            {
                syncMessage.sourceFile.CopyTo(newFullTarget, true);
            }
            else if (syncMessage.sourceFile.LastWriteTime > fileVersions.Last().LastWriteTime ||
                syncMessage.sourceFile.Length != fileVersions.Last().Length)
            {
                // Sort array so that the oldes file is first in list
                fileVersions.Sort((f1, f2) => f1.Name.CompareTo(f2.Name));

                syncMessage.sourceFile.CopyTo(newFullTarget, true);
                fileVersions.Add(new FileInfo(newFullTarget));

                // Remove excess backup versions
                if (fileVersions.Count > syncMessage.versions)
                {
                    for (int i = 0; i < fileVersions.Count - syncMessage.versions; i++)
                    {
                        fileVersions[i].Delete();
                    }
                }
            }
        }
    }
}
