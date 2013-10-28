using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DirSyncro
{
    class SyncModifyJob : SyncJob
    {
        public SyncModifyJob()
        {
        }

        public void Execute(Object context)
        {
            SyncMessage syncMessage = (SyncMessage)context;

            try
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

                // Construct filename with timestamp and copy source file to target directory
                String newFullTarget = String.Format("{0}\\{1}_{2}{3}", fullTarget.FullName, Path.GetFileNameWithoutExtension(syncMessage.sourceFile.Name), CreateEpoch(), Path.GetExtension(syncMessage.sourceFile.Name));
                File.Copy(syncMessage.sourceFile.FullName, newFullTarget, false);

                // Remove excess backup versions
                FileInfo[] fileVersions = fullTarget.GetFiles("*.*", SearchOption.TopDirectoryOnly)
                    .Where(path => Regex.IsMatch(path.Name, @"^" + Regex.Escape(Path.GetFileNameWithoutExtension(syncMessage.sourceFile.Name)) + @"_\d+" + Regex.Escape(Path.GetExtension(syncMessage.sourceFile.Name)) + @"$")).ToArray();

                if (fileVersions.Length > syncMessage.versions)
                {
                    // Sort array so that the oldes file is first in list
                    Array.Sort(fileVersions, (f1, f2) => f1.Name.CompareTo(f2.Name));

                    for (int i = 0; i < fileVersions.Length - syncMessage.versions; i++)
                    {
                        fileVersions[i].Delete();
                    }
                }

            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
