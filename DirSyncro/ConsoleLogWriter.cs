using System;
using System.Collections.Generic;

namespace Logging
{
    /// <summary>
    /// A Logging class implementing the Singleton pattern and an internal Queue to be flushed perdiodically
    /// </summary>
    class ConsoleLogWriter : LogWriter
    {
        protected static ConsoleLogWriter instance;

        public static ConsoleLogWriter Instance
        {
            get
            {
                if (instance == null) instance = new ConsoleLogWriter();
                return instance;
            }
        }

        protected override void Log(Levels level, string message, params object[] args)
        {
            WriteToLog(level, message, args);
        }

        protected override void WriteToLog(Levels level, string message, params object[] args)
        {
            switch (level)
            {
                case Levels.DEBUG:
                    Console.Out.WriteLine(message, args);
                    break;
                case Levels.INFO:
                    Console.Out.WriteLine(message, args);
                    break;
                case Levels.WARN:
                    Console.Out.WriteLine(message, args);
                    break;
                case Levels.ERROR:
                    Console.Error.WriteLine(message, args);
                    break;
            }
        }
    }
}