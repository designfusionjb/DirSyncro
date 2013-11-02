using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirSyncro
{
    class SyncInterface : ISyncInterface
    {
        public const uint STARTUP = 0;
        public const uint SHUTDOWN = 1;
        public const uint RESTART = 2;

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
            }
            return true;
        }
    }
}
