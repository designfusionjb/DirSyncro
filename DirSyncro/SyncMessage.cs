using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirSyncro
{
    class SyncMessage
    {
        public DateTime timeStamp { private set; get; }
        public DateTime modifiedTime { private set; get; }
        public WatcherChangeTypes changeType { private set; get; }
        public string sourcePath { private set; get; }
        public FileInfo sourceFile { private set; get; }
        public string targetPath { set; get; }
        public string partialPath { private set; get; }
        public int versions { private set; get; }

        public SyncMessage(FileSystemEventArgs eventType, DirSyncroWatcher watcherConfig)
        {
            this.timeStamp = DateTime.Now;
            this.changeType = eventType.ChangeType;
            this.sourceFile = new FileInfo(eventType.FullPath);
            this.sourcePath = watcherConfig.SourceDirectory;
            this.modifiedTime = sourceFile.LastWriteTime;
            partialPath = sourceFile.DirectoryName.Replace(sourcePath, "");
            versions = watcherConfig.Versions;
        }

        public SyncMessage(FileSystemEventArgs eventType, DirSyncroWatcher watcherConfig, string targetPath)
        {
            this.timeStamp = DateTime.Now;
            this.changeType = eventType.ChangeType;
            this.sourceFile = this.sourceFile = new FileInfo(eventType.FullPath);
            this.sourcePath = watcherConfig.SourceDirectory;
            this.modifiedTime = sourceFile.LastWriteTime;
            this.targetPath = targetPath;
            partialPath = sourceFile.DirectoryName.Replace(sourcePath, "");
            versions = watcherConfig.Versions;
        }
    }
}
