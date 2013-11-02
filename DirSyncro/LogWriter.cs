using System;
using System.Collections.Generic;

namespace Logging
{
    /// <summary>
    /// A Logging class implementing the Singleton pattern and an internal Queue to be flushed perdiodically
    /// </summary>
    abstract class LogWriter
    {
        private int maxLogAge = 5000;
        private int queueSize = 10;
        private static Levels logLevel = Levels.ERROR;
        private Queue<LogMsg> logQueue = null;
        private DateTime LastFlushed = DateTime.Now;

        protected bool Queue
        {
            get
            {
                if (logQueue == null)
                    return false;
                else
                    return true;
            }
            set
            {
                if (value)
                    if (logQueue == null) logQueue = new Queue<LogMsg>();
                else
                    logQueue = null;
            }
        }

        public Levels LogLevel
        {
            get { return logLevel; }
            set { logLevel = value; }
        }

        /// <summary>
        /// If wanting to set different than default max log age parameter
        /// </summary>
        public int MaxLogAge
        {
            get { return maxLogAge; }
            set { maxLogAge = value; }
        }

        /// <summary>
        /// If wanting to set different than default log queue size
        /// </summary>
        public int QueueSize
        {
            get { return queueSize; }
            set { queueSize = value; }
        }

        public void Debug(string message, params object[] args)
        {
            this.Log(Levels.DEBUG, message, args);
        }

        public void Info(string message, params object[] args)
        {
            this.Log(Levels.INFO, message, args);
        }

        public void Warn(string message, params object[] args)
        {
            this.Log(Levels.WARN, message, args);
        }

        public void Error(string message, params object[] args)
        {
            this.Log(Levels.ERROR, message, args);
        }

        protected virtual void Log(Levels level, string message, params object[] args)
        {
            LogMsg log = new LogMsg(level, message, args);

            // Lock the queue while writing to prevent contention for the log file
            lock (logQueue)
            {
                // Create the entry and push to the Queue
                logQueue.Enqueue(log);

                // If we have reached the Queue Size then flush the Queue
                if (level >= Levels.WARN || logQueue.Count >= queueSize || DoPeriodicFlush())
                {
                    FlushLog();
                }
            }
        }

        protected virtual void WriteToLog(Levels level, string message, params object[] args) { }

        private bool DoPeriodicFlush()
        {
            TimeSpan logAge = DateTime.Now - LastFlushed;
            if (logAge.TotalSeconds >= maxLogAge)
            {
                LastFlushed = DateTime.Now;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Flushes the Queue to the physical log file
        /// </summary>
        private void FlushLog()
        {
            while (logQueue.Count > 0)
            {
                LogMsg log = logQueue.Dequeue();
                WriteToLog(log.Level, log.Message, log.Args);
            }
        }

        /// <summary>
        /// Flush log at application exit
        /// </summary>
        ~LogWriter()
        {
            FlushLog();
        }
    }

    public class LogMsg
    {
        public Levels Level { get; private set; }
        public string Message { get; private set; }
        public object[] Args { get; private set; }

        public LogMsg(string message, object[] args)
        {
            this.Level = Levels.INFO;
            this.Message = message;
            this.Args = args;
        }

        public LogMsg(Levels level, string message, object[] args)
        {
            this.Level = level;
            this.Message = message;
            this.Args = args;
        }
    }

    public enum Levels { DEBUG = 1, INFO = 2, WARN = 4, ERROR = 8 }
}