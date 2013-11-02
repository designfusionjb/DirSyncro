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
        private static ConfigWatcher instance;
        private string configurationFile;
        private FileSystemWatcher configWatcher = new FileSystemWatcher();
        private List<Watcher> watchers;
        private DateTime prevLastModified = DateTime.Now;

        private ConfigWatcher(string configurationFile)
        {
            this.configurationFile = configurationFile;

            configWatcher.Path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            configWatcher.IncludeSubdirectories = false;
            configWatcher.Filter = configurationFile;
            configWatcher.NotifyFilter = NotifyFilters.LastWrite;
            configWatcher.Changed += new FileSystemEventHandler(ConfigChanged);
            configWatcher.EnableRaisingEvents = true;
        }

        public static ConfigWatcher Startup(string configurationFile)
        {
            if (instance == null)
            {
                instance = new ConfigWatcher(configurationFile);
            }

            return instance;
        }

        public void Shutdown()
        {
            configWatcher.EnableRaisingEvents = false;
        }

        public static ConfigWatcher Instance
        {
            get
            {
                return instance;
            }
        }

        public void StartWatchers()
        {
            DirSyncro config = Utility.ReadFromXML<DirSyncro>(configurationFile);

            watchers = new List<Watcher>(config.Watcher.Length);

            foreach (DirSyncroWatcher c in config.Watcher)
                watchers.Add(new Watcher(c));
        }

        public void StopWatchers()
        {
            foreach (Watcher w in watchers)
            {
                w.Shutdown();
            }
        }

        public void RestartWatchers()
        {
            StopWatchers();
            watchers = null;
            StartWatchers();
        }

        public void StartWatcher(string watcherId)
        {
            foreach (Watcher w in watchers.Where(w => w.WatcherId == watcherId))
            {
                w.StartUp();
            }
        }

        public void StopWatcher(string watcherId)
        {
            foreach (Watcher w in watchers.Where(w => w.WatcherId == watcherId))
            {
                w.Shutdown();
            }
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
    }
}
