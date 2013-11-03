using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DirSyncro
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SyncService" in both code and config file together.
    public class SyncService : ISyncService
    {
        public const uint STARTUP = 0;
        public const uint SHUTDOWN = 1;
        public const uint RESTART = 2;
        public const uint STATS = 3;

        public bool ServiceCommand(uint eventId, string objectId)
        {
            ConfigWatcher instance = ConfigWatcher.Instance;

            switch (eventId)
            {
                case STARTUP:
                    if (string.IsNullOrEmpty(objectId))
                    {
                        instance.StartWatchers();
                    }
                    else
                    {
                        instance.StartWatcher(objectId);
                    }
                    break;
                case SHUTDOWN:
                    if (string.IsNullOrEmpty(objectId))
                    {
                        instance.StopWatchers();
                    }
                    else
                    {
                        instance.StopWatcher(objectId);
                    }
                    break;
                case RESTART:
                    instance.RestartWatchers();
                    break;
                case STATS:
                    instance.RestartWatchers();
                    break;
            }
            return true;
        }
    }
}
