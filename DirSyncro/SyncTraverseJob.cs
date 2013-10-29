using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DirSyncro
{
    class SyncTraverseJob : SyncJob
    {
        private string[] targetPaths;

        public SyncTraverseJob(SyncMessage syncMessage, string[] targetPaths)
            : base(syncMessage)
        {
            this.targetPaths = targetPaths;
        }

        public void ExecuteSource(Object context)
        {
            try
            {
                foreach (FileInfo sourceFile in new DirectoryInfo(syncMessage.sourcePath).GetFiles("*.*", SearchOption.AllDirectories))
                {
                    syncMessage.sourceFile = sourceFile;

                    foreach (string targetPath in targetPaths)
                    {
                        syncMessage.targetPath = targetPath;

                        CopyFile();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        private bool SameFile(FileInfo file, FileInfo lastFile)
        {
            if (file.Name.Length != lastFile.Name.Length)
                return false;

            Regex regex = new Regex(@"^(.+)_\d+(\.[^\.]+)?$", RegexOptions.None);

            return false;
        }

        public void ExecuteTarget(Object context)
        {
            try
            {
                foreach (string targetPath in targetPaths)
                {
                    syncMessage.targetPath = targetPath;

                    // Run one file iteration per directory to minimize memory utilization
                    DirectoryInfo[] targetDirs = new DirectoryInfo(targetPath).GetDirectories("*.*", SearchOption.AllDirectories);
                    
                    foreach (DirectoryInfo targetDir in targetDirs)
                    {
                        FileInfo[] targetFiles = targetDir.GetFiles("*.*", SearchOption.TopDirectoryOnly);
                        Array.Sort(targetFiles, (f1, f2) => f1.Name.CompareTo(f2.Name));

                        List<FileInfo> fileBuffer = new List<FileInfo>(syncMessage.versions);
                        foreach (FileInfo targetFile in targetFiles)
                        {

                        }
                        targetFiles = null;
                    }
                    targetDirs = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
