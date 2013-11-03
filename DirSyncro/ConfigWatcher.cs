using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DirSyncro
{
    class ConfigWatcher
    {
        private static ConfigWatcher instance;
        private string configurationFile;
        private bool watchersUp = false;
        //private FileSystemWatcher configWatcher = new FileSystemWatcher();
        private List<Watcher> watchers;
        private DateTime prevLastModified = DateTime.Now;

        private ConfigWatcher(string configurationFile)
        {
            this.configurationFile = configurationFile;

        }

        public static ConfigWatcher Startup(string configurationFile)
        {
            if (instance == null)
            {
                instance = new ConfigWatcher(configurationFile);
                instance.StartWatchers();
            }

            return instance;
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
            if (!watchersUp)
            {
                DirSyncro config = Utility.ReadFromXML<DirSyncro>(configurationFile);

                watchers = new List<Watcher>(config.Watcher.Length);

                foreach (DirSyncroWatcher c in config.Watcher)
                {
                    watchers.Add(new Watcher(c));
                    Debug.WriteLine("DEBUG: Startup directory watchers.");
                }

                watchersUp = true;
            }
        }

        public void StopWatchers()
        {
            if (watchersUp)
            {
                foreach (Watcher w in watchers)
                {
                    w.Shutdown();
                }
                watchers = null;

                Debug.WriteLine("DEBUG: Shutdown directory watchers.");

                watchersUp = false;
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
    }
}
