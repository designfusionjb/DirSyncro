using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DirSyncro
{
    class SyncMessage
    {
        public DateTime timeStamp { private set; get; }
        public DateTime modifiedTime { private set; get; }
        public WatcherChangeTypes changeType { private set; get; }
        public DirectoryInfo sourcePath { private set; get; }
        public FileInfo sourceFile { set; get; }
        public DirectoryInfo targetPath { set; get; }
        public int versions { private set; get; }
        public TimeSpan settling { private set; get; }
        public TimeSpan retention { private set; get; }
        public List<Regex> includeList { private set; get; }
        public List<Regex> excludeList { private set; get; }

        public SyncMessage(DirSyncroWatcher watcherConfig, List<Regex> includeList, List<Regex> excludeList)
        {
            this.timeStamp = DateTime.Now;
            this.sourcePath = new DirectoryInfo(watcherConfig.SourceDirectory);
            this.includeList = includeList;
            this.excludeList = excludeList;
            this.versions = watcherConfig.Versions;
            this.settling = TimeSpan.FromSeconds(watcherConfig.Settling);
            this.retention = TimeSpan.FromDays(watcherConfig.Retention);
        }

        public SyncMessage(FileSystemEventArgs eventType, DirSyncroWatcher watcherConfig, List<Regex> includeList, List<Regex> excludeList)
        {
            this.timeStamp = DateTime.Now;
            this.changeType = eventType.ChangeType;
            this.sourceFile = new FileInfo(eventType.FullPath);
            this.sourcePath = new DirectoryInfo(watcherConfig.SourceDirectory);
            this.includeList = includeList;
            this.excludeList = excludeList;
            this.versions = watcherConfig.Versions;
            this.settling = TimeSpan.FromMilliseconds(watcherConfig.Settling);
            this.retention = TimeSpan.FromMilliseconds(watcherConfig.Retention);
        }

        public SyncMessage(FileSystemEventArgs eventType, DirSyncroWatcher watcherConfig, string targetPath, List<Regex> includeList, List<Regex> excludeList)
        {
            this.timeStamp = DateTime.Now;
            this.changeType = eventType.ChangeType;
            this.sourceFile = new FileInfo(eventType.FullPath);
            this.sourcePath = new DirectoryInfo(watcherConfig.SourceDirectory);
            this.targetPath = new DirectoryInfo(targetPath);
            this.includeList = includeList;
            this.excludeList = excludeList;
            this.versions = watcherConfig.Versions;
            this.settling = TimeSpan.FromMilliseconds(watcherConfig.Settling);
            this.retention = TimeSpan.FromMilliseconds(watcherConfig.Retention);
        }
    }
}
