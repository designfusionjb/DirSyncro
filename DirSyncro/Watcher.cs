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
        private string watcherId;
        private DirSyncroWatcher watcherConfig;
        private List<Regex> includeList = null;
        private List<Regex> excludeList = null;

        public Watcher(DirSyncroWatcher watcherConfig)
        {
            this.watcherConfig = watcherConfig;
            this.watcherId = watcherConfig.Name;
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

            fileWatcher = new FileSystemWatcher();
            fileWatcher.Path = watcherConfig.SourceDirectory;
            fileWatcher.IncludeSubdirectories = true;
            fileWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            fileWatcher.Created += new FileSystemEventHandler(FileEvent);
            //fileWatcher.Renamed += new RenamedEventHandler(RenamedEvent);
            fileWatcher.Changed += new FileSystemEventHandler(FileEvent);
            //fileWatcher.Deleted += new FileSystemEventHandler(DeletedEvent);
            fileWatcher.EnableRaisingEvents = true;
        }

        public string WatcherId
        {
            get { return watcherId; }
        }

        private void FileEvent(object sender, FileSystemEventArgs e)
        {
            foreach (string targetPath in watcherConfig.TargetDirectory)
            {
                SyncMessage syncMessage = new SyncMessage(e, watcherConfig, targetPath, includeList, excludeList);

                if (e.ChangeType == WatcherChangeTypes.Created)
                    ThreadPool.QueueUserWorkItem(new SyncCreateJob(syncMessage).Execute);
                else if (e.ChangeType == WatcherChangeTypes.Changed)
                    ThreadPool.QueueUserWorkItem(new SyncModifyJob(syncMessage).Execute);
            }
        }

        public void StartUp()
        {
            fileWatcher.EnableRaisingEvents = true;
        }

        public void Shutdown()
        {
            fileWatcher.EnableRaisingEvents = false;
        }

        public void SyncSourceToTarget()
        {
            ThreadPool.QueueUserWorkItem(new SyncTraverseJob(new SyncMessage(watcherConfig, includeList, excludeList), watcherConfig.TargetDirectory).ExecuteSource);
        }

        public void SyncTargetToSource()
        {
            ThreadPool.QueueUserWorkItem(new SyncTraverseJob(new SyncMessage(watcherConfig, includeList, excludeList), watcherConfig.TargetDirectory).ExecuteTarget);
        }
    }
}
