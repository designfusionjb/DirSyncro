using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Logging
{
    /// <summary>
    /// A Logging class implementing the Singleton pattern and an internal Queue to be flushed perdiodically
    /// </summary>
    class EventLogWriter : LogWriter
    {
        protected static EventLogWriter instance = null;
        private string source = null;

        public static EventLogWriter Instance
        {
            get
            {
                if (instance == null) instance = new EventLogWriter();
                instance.Queue = true;
                return instance;
            }
        }

        public string Source
        {
            get { return source; }
            set { source = value; }
        }

        protected override void Log(Levels level, string message, params object[] args)
        {
            base.Log(level, message, args);
        }

        protected override void WriteToLog(Levels level, string message, params object[] args)
        {
            if (!string.IsNullOrEmpty(source))
            {
                switch (level)
                {
                    case Levels.DEBUG:
                        EventLog.WriteEntry(source, String.Format(message, args), EventLogEntryType.Information);
                        break;
                    case Levels.INFO:
                        EventLog.WriteEntry(source, String.Format(message, args), EventLogEntryType.Information);
                        break;
                    case Levels.WARN:
                        EventLog.WriteEntry(source, String.Format(message, args), EventLogEntryType.Warning);
                        break;
                    case Levels.ERROR:
                        EventLog.WriteEntry(source, String.Format(message, args), EventLogEntryType.Error);
                        break;
                }
            }
            else
            {
                Console.Error.WriteLine("Cannot generate log output since eventlog source has not been initialized.");
            }
        }
    }
}