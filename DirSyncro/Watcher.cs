using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Threading;

namespace DirSyncro
{
    class Watcher
    {
        private FileSystemWatcher fileWatcher;
        private DirSyncroWatcher watcherConfig;
        private TimeSpan settling;
        private List<Regex> includeList = null;
        private List<Regex> excludeList = null;
        private Queue<SyncMessage> runHistory = new LimitedQueue<SyncMessage>(50);
        private Queue<SyncMessage> backLog = new LimitedQueue<SyncMessage>(1000);
        private object sync = new object();

        public Watcher(DirSyncroWatcher watcherConfig)
        {
            this.watcherConfig = watcherConfig;
            if (!string.IsNullOrEmpty(watcherConfig.Include))
            {
                includeList = new List<Regex>();
                foreach (string s in watcherConfig.Include.Split(';'))
                {
                    includeList.Add(Utility.WildcardMatch(s.Trim().ToLower(), true));
                }
            }
            else if (!string.IsNullOrEmpty(watcherConfig.Exclude))
            {
                excludeList = new List<Regex>();
                foreach (string s in watcherConfig.Exclude.Split(';'))
                {
                    excludeList.Add(Utility.WildcardMatch(s.Trim().ToLower(), true));
                }
            }
            settling = TimeSpan.FromMilliseconds((double)watcherConfig.Settling);

            fileWatcher = new FileSystemWatcher();
            fileWatcher.Path = watcherConfig.SourceDirectory;
            fileWatcher.IncludeSubdirectories = true;
            fileWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            //fileWatcher.Created += new FileSystemEventHandler(CreatedEvent);
            //fileWatcher.Renamed += new RenamedEventHandler(RenamedEvent);
            fileWatcher.Changed += new FileSystemEventHandler(ChangedEvent);
            //fileWatcher.Deleted += new FileSystemEventHandler(DeletedEvent);
            fileWatcher.EnableRaisingEvents = true;
        }

        private bool Run(SyncMessage currentMessage)
        {
            /// Don't run sync if there has been a sync run on the same sourcefile within the settling time
            lock (sync)
            {
                foreach (SyncMessage pastMessage in runHistory)
                {
                    if (currentMessage.sourceFile.Equals(pastMessage.sourceFile))
                    {
                        if (currentMessage.modifiedTime.Equals(pastMessage.modifiedTime))
                        {
                            Console.WriteLine("SKIP: \"{0}\" was already run.", currentMessage.sourceFile);
                            return false;
                        }
                        else if (currentMessage.timeStamp - pastMessage.timeStamp < settling)
                        {
                            Console.WriteLine("SKIP: \"{0}\" too many updates.", currentMessage.sourceFile);
                            return false;
                        }
                    }
                }
            }

            /// Don't run filtered jobs (Don't even add them to run history
            if (includeList != null)
            {
                bool found = false;
                foreach (Regex regex in includeList)
                {
                    if (regex.IsMatch(currentMessage.sourceFile.Name))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Console.WriteLine("SKIP: \"{0}\" is filtered.", currentMessage.sourceFile);
                    return false;
                }
            }
            else if (excludeList != null)
            {
                foreach (Regex regex in excludeList)
                {
                    if (regex.IsMatch(currentMessage.sourceFile.Name))
                    {
                        Console.WriteLine("SKIP: \"{0}\" is filtered.", currentMessage.sourceFile);
                        return false;
                    }
                }
            }

            lock (sync)
            {
                runHistory.Enqueue(currentMessage);
            }

            return true;
        }

        private void CreatedEvent(object sender, FileSystemEventArgs e)
        {

        }

        private void ChangedEvent(object sender, FileSystemEventArgs e)
        {
            SyncMessage syncMessage = new SyncMessage(e, watcherConfig);
            if (Run(syncMessage))
            {
                foreach (string targetPath in watcherConfig.TargetDirectory)
                {
                    if (Directory.Exists(targetPath))
                    {
                        syncMessage.targetPath = new FileInfo(targetPath);
                        ThreadPool.QueueUserWorkItem(new SyncModifyJob().Execute, syncMessage);
                        Console.WriteLine("CHANGED: \"{0}\".", syncMessage.sourceFile.FullName);
                    }
                    else
                    {
                        Console.WriteLine("CHANGED: \"{0}\" to backlog.", syncMessage.sourceFile.FullName);
                        backLog.Enqueue(syncMessage);
                    }
                }
            }
        }

        private void DeletedEvent(object sender, FileSystemEventArgs e)
        {
            //Console.WriteLine(String.Format("Event: {0}, Fullpath: {1}", e.ChangeType.ToString(), e.FullPath));
        }

        private void RenamedEvent(object sender, RenamedEventArgs e)
        {
            //Scheduler.AddJob(new SyncRenameJob(e.ChangeType.ToString(), fileWatcher.Path, e.OldName, e.Name, targetPaths, versions));
        }

        public void Shutdown()
        {
            fileWatcher.EnableRaisingEvents = false;
        }
    }
}
