using System;
using System.Diagnostics;
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
        protected Regex multipleBackslash = new Regex(@"\\", RegexOptions.Compiled);
        protected SyncMessage syncMessage;
        protected static List<SyncMessage> runHistory = new List<SyncMessage>();
        private static Queue<SyncMessage> backLog = new LimitedQueue<SyncMessage>(1000);

        public SyncJob(SyncMessage syncMessage)
        {
            this.syncMessage = syncMessage;
        }

        public bool IsDirectoryEmpty(string path)
        {
            IEnumerable<string> items = Directory.EnumerateFileSystemEntries(path);
            using (IEnumerator<string> en = items.GetEnumerator())
            {
                return !en.MoveNext();
            }
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
            int backupTimeIndex = Path.GetFileNameWithoutExtension(backupFile).LastIndexOf('_') + 1;

            return EpochToDateTime(long.Parse(Path.GetFileNameWithoutExtension(backupFile).Substring(backupTimeIndex, Path.GetFileNameWithoutExtension(backupFile).Length - backupTimeIndex)));
        }

        protected bool IncludeFile()
        {
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
            return true;
        }

        protected bool Run()
        {
            bool run = true;

            run = IncludeFile();

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
                            run = false;
                        }
                        if (DateTime.UtcNow - runHistory[i].timeStamp > syncMessage.settling)
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
                Debug.WriteLine("CREATED: \"{0}\" to backlog.", syncMessage.sourceFile);

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

        protected List<FileInfo> CopyFile(List<FileInfo> fileVersions)
        {
            string partialPath = syncMessage.sourceFile.DirectoryName.Replace(syncMessage.sourcePath.FullName, "");
            
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

            // todo - Make solution for having to list the whole directory each iteration of the synctraverse target to source
            if (fileVersions == null)
            {
                fileVersions = fullTarget.EnumerateFiles("*", SearchOption.TopDirectoryOnly).ToList();
            }
            List<FileInfo> sourceFileVersions = fileVersions.Select(path => path).Where(path => syncMessage.sourceFile.Name == GetSourceFileFromTargetFile(path.Name)).ToList();

            // Only copy file to target if last modified time is newer or the file sizes differ (could mean interrupted file copy)
            if (sourceFileVersions.Count == 0)
            {
                // Construct filename with timestamp and copy source file to target directory
                string newFullTarget = String.Format("{0}\\{1}_{2}{3}", fullTarget.FullName, Path.GetFileNameWithoutExtension(syncMessage.sourceFile.Name), CreateEpoch(), syncMessage.sourceFile.Extension);
                
                syncMessage.sourceFile.CopyTo(newFullTarget, true);

                Debug.WriteLine("Copying file \"{0}\" to \"{1}\"", syncMessage.sourceFile.FullName, newFullTarget);
            }
            else if (syncMessage.sourceFile.LastWriteTime > sourceFileVersions.Last().LastWriteTime ||
                syncMessage.sourceFile.Length != sourceFileVersions.Last().Length)
            {
                // Construct filename with timestamp and copy source file to target directory
                string newFullTarget = String.Format("{0}\\{1}_{2}{3}", fullTarget.FullName, Path.GetFileNameWithoutExtension(syncMessage.sourceFile.Name), CreateEpoch(), syncMessage.sourceFile.Extension);

                // Sort array so that the oldes file is first in list
                sourceFileVersions.Sort((f1, f2) => f1.Name.CompareTo(f2.Name));

                syncMessage.sourceFile.CopyTo(newFullTarget, true);
                Debug.WriteLine("Copying file \"{0}\" to \"{1}\"", syncMessage.sourceFile.FullName, newFullTarget);

                sourceFileVersions.Add(new FileInfo(newFullTarget));

                // Remove excess backup versions
                if (sourceFileVersions.Count > syncMessage.versions)
                {
                    for (int i = 0; i < sourceFileVersions.Count - syncMessage.versions; i++)
                    {
                        Debug.WriteLine("Deleting file \"{0}\"", sourceFileVersions[i].FullName);
                        sourceFileVersions[i].Delete();
                    }
                }
            }

            return fileVersions;
        }
    }
}
