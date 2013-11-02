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

        public void CopyFromSourceToTarget(DirectoryInfo sourceDirectory)
        {
            // Copy source files in this directory to target directory
            List<FileInfo> targetFiles = null;
            foreach (FileInfo sourceFile in sourceDirectory.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly))
            {
                syncMessage.sourceFile = sourceFile;

                if (IncludeFile())
                {
                    foreach (string targetPath in targetPaths)
                    {
                        syncMessage.targetPath = new DirectoryInfo(targetPath);

                        targetFiles = CopyFile(targetFiles);
                    }
                }
            }

            // Recurse down into sub-directories
            foreach (DirectoryInfo subDirectory in sourceDirectory.EnumerateDirectories("*.*", SearchOption.TopDirectoryOnly))
            {
                CopyFromSourceToTarget(subDirectory);
            }
        }

        public void ExecuteSource(Object context)
        {
            try
            {
                CopyFromSourceToTarget(syncMessage.sourcePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        public void DeleteFromTarget(DirectoryInfo targetDirectory)
        {
            // Recurse down into sub-directories
            foreach (DirectoryInfo subDirectory in targetDirectory.EnumerateDirectories("*.*", SearchOption.TopDirectoryOnly))
            {
                DeleteFromTarget(subDirectory);
            }

            Dictionary<string, List<FileInfo>> sourceAndTargets = new Dictionary<string, List<FileInfo>>();
            
            // Get all files in directory and sort them by name which gives time order for each backup file as well
            FileInfo[] targetFiles = targetDirectory.GetFiles("*.*", SearchOption.TopDirectoryOnly);
            Array.Sort(targetFiles, (f1, f2) => f1.Name.CompareTo(f2.Name));

            // Gather files ordered by sourceFile
            foreach (FileInfo targetFile in targetFiles)
            {
                string sourceFile = syncMessage.sourcePath.FullName + targetDirectory.FullName.Replace(syncMessage.targetPath.FullName, "") + Path.DirectorySeparatorChar +
                    GetSourceFileFromTargetFile(targetFile.Name);

                if (!sourceAndTargets.ContainsKey(sourceFile))
                {
                    sourceAndTargets.Add(sourceFile, new List<FileInfo>());
                }
                sourceAndTargets[sourceFile].Add(targetFile);
            }
            // Loop over sourceFile and remove targetFiles if they are old
            foreach (KeyValuePair<string, List<FileInfo>> kvp in sourceAndTargets)
            {
                foreach (FileInfo targetFile in kvp.Value)
                {
                    if (DateTime.UtcNow - GetBackupTime(targetFile.Name) > syncMessage.retention)
                    {
                        if (targetFile == kvp.Value.Last())
                        {
                            if (!File.Exists(kvp.Key))
                            {
                                targetFile.Delete();
                            }
                        }
                        else
                        {
                            targetFile.Delete();
                        }
                    }
                }
            }
            if (IsDirectoryEmpty(targetDirectory.FullName))
                targetDirectory.Delete();
        }

        public void ExecuteTarget(Object context)
        {
            try
            {
                foreach (string targetPath in targetPaths)
                {
                    syncMessage.targetPath = new DirectoryInfo(targetPath);

                    DeleteFromTarget(syncMessage.targetPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
