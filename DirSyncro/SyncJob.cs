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

        protected virtual bool Run()
        {
            /// Don't run filtered jobs (Don't even add them to run history
            if (syncMessage.includeList != null)
            {
                bool found = false;
                foreach (Regex regex in syncMessage.includeList)
                {
                    if (regex.IsMatch(syncMessage.sourceFile.Name))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Console.WriteLine("SKIP: \"{0}\" is filtered.", syncMessage.sourceFile);
                    return false;
                }
            }
            else if (syncMessage.excludeList != null)
            {
                foreach (Regex regex in syncMessage.excludeList)
                {
                    if (regex.IsMatch(syncMessage.sourceFile.Name))
                    {
                        Console.WriteLine("SKIP: \"{0}\" is filtered.", syncMessage.sourceFile);
                        return false;
                    }
                }
            }

            if (!Directory.Exists(syncMessage.targetPath))
            {
                lock (backLog)
                {
                    backLog.Enqueue(syncMessage);
                }
                Console.WriteLine("CREATED: \"{0}\" to backlog.", syncMessage.sourceFile.FullName);

                return false;
            }

            return true;
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
            // Construct the partial target path directory
            DirectoryInfo fullTarget = null;
            if (string.IsNullOrEmpty(syncMessage.partialPath))
            {
                fullTarget = new DirectoryInfo(syncMessage.targetPath);
            }
            else
            {
                fullTarget = new DirectoryInfo(syncMessage.targetPath + "\\" + syncMessage.partialPath);
            }

            // Verify target path and create if necessary
            CreatePath(fullTarget);

            // Remove excess backup versions
            List<FileInfo> fileVersions = fullTarget.GetFiles("*.*", SearchOption.TopDirectoryOnly)
                .Where(path => Regex.IsMatch(path.Name, @"^" + Regex.Escape(Path.GetFileNameWithoutExtension(syncMessage.sourceFile.Name)) + @"_\d+" + Regex.Escape(Path.GetExtension(syncMessage.sourceFile.Name)) + @"$")).ToList();

            // Construct filename with timestamp and copy source file to target directory
            FileInfo newFullTarget = new FileInfo(String.Format("{0}\\{1}_{2}{3}", fullTarget.FullName, Path.GetFileNameWithoutExtension(syncMessage.sourceFile.Name), CreateEpoch(), Path.GetExtension(syncMessage.sourceFile.Name)));
            // Sort array so that the oldes file is first in list
            fileVersions.Sort((f1, f2) => f1.LastWriteTime.CompareTo(f2.LastWriteTime));

            // Only copy file to target if last modified time is newer or the file sizes differ (could mean interrupted file copy)
            if (syncMessage.sourceFile.LastWriteTime > fileVersions.Last().LastWriteTime ||
                syncMessage.sourceFile.Length != fileVersions.Last().Length)
            {
                syncMessage.sourceFile.CopyTo(newFullTarget.FullName, true);
                fileVersions.Add(newFullTarget);

                if (fileVersions.Count > syncMessage.versions)
                {
                    for (int i = 0; i < fileVersions.Count - syncMessage.versions; i++)
                    {
                        fileVersions[i].Delete();
                    }
                }
            }
        }

        protected string CreateEpoch()
        {
            return Math.Round((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString();
        }
    }
}
