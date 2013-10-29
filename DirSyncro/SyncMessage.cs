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
        public string sourcePath { private set; get; }
        public FileInfo _sourceFile { set; get; }
        public string targetPath { set; get; }
        public string partialPath { private set; get; }
        public int versions { private set; get; }
        public List<Regex> includeList { private set; get; }
        public List<Regex> excludeList { private set; get; }
        public TimeSpan settling { private set; get; }

        public FileInfo sourceFile
        {
            get { return _sourceFile; }
            set
            {
                _sourceFile = value;
                partialPath = sourceFile.DirectoryName.Replace(sourcePath, "");
            }
        }

        public SyncMessage(DirSyncroWatcher watcherConfig, List<Regex> includeList, List<Regex> excludeList)
        {
            this.timeStamp = DateTime.Now;
            this.sourcePath = watcherConfig.SourceDirectory;
            this.modifiedTime = sourceFile.LastWriteTime;
            this.includeList = includeList;
            this.excludeList = excludeList;
            versions = watcherConfig.Versions;
            settling = TimeSpan.FromMilliseconds(watcherConfig.Settling);
        }

        public SyncMessage(FileSystemEventArgs eventType, DirSyncroWatcher watcherConfig, List<Regex> includeList, List<Regex> excludeList)
        {
            this.timeStamp = DateTime.Now;
            this.changeType = eventType.ChangeType;
            this.sourceFile = new FileInfo(eventType.FullPath);
            this.sourcePath = watcherConfig.SourceDirectory;
            this.modifiedTime = sourceFile.LastWriteTime;
            this.includeList = includeList;
            this.excludeList = excludeList;
            versions = watcherConfig.Versions;
            settling = TimeSpan.FromMilliseconds(watcherConfig.Settling);
        }

        public SyncMessage(FileSystemEventArgs eventType, DirSyncroWatcher watcherConfig, string targetPath, List<Regex> includeList, List<Regex> excludeList)
        {
            this.timeStamp = DateTime.Now;
            this.changeType = eventType.ChangeType;
            this.sourceFile = this.sourceFile = new FileInfo(eventType.FullPath);
            this.sourcePath = watcherConfig.SourceDirectory;
            this.modifiedTime = sourceFile.LastWriteTime;
            this.targetPath = targetPath;
            this.includeList = includeList;
            this.excludeList = excludeList;
            versions = watcherConfig.Versions;
            settling = TimeSpan.FromMilliseconds(watcherConfig.Settling);
        }
    }
}
