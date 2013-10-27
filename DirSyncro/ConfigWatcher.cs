using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirSyncro
{
    class ConfigWatcher
    {
        private string configurationFile;
        private FileSystemWatcher configWatcher = new FileSystemWatcher();
        private List<Watcher> watchers;
        private DateTime prevLastModified = DateTime.Now;

        public ConfigWatcher(string configurationFile)
        {
            this.configurationFile = configurationFile;

            StartWatchers();

            configWatcher.Path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            configWatcher.IncludeSubdirectories = false;
            configWatcher.Filter = configurationFile;
            configWatcher.NotifyFilter = NotifyFilters.LastWrite;
            configWatcher.Changed += new FileSystemEventHandler(ConfigChanged);
            configWatcher.EnableRaisingEvents = true;
        }

        private void StartWatchers()
        {
            DirSyncro config = Utility.ReadFromXML<DirSyncro>(configurationFile);

            watchers = new List<Watcher>(config.Watcher.Length);

            foreach (DirSyncroWatcher c in config.Watcher)
                watchers.Add(new Watcher(c));
        }

        private void StopWatchers()
        {
            foreach (Watcher w in watchers)
            {
                w.Shutdown();
            }
            watchers = null;
        }

        private void ConfigChanged(object sender, FileSystemEventArgs e)
        {
            DateTime lastModified = File.GetLastWriteTime(e.FullPath);
            if (lastModified.Equals(prevLastModified))
                return;

            StopWatchers();
            StartWatchers();
            
            prevLastModified = lastModified;
        }

        public void Shutdown()
        {
            configWatcher.EnableRaisingEvents = false;
        }
    }
}
