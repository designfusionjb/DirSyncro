using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Configuration;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirSyncro
{
    class Program
    {
        private static readonly string configurationFile = @"DirSyncro.xml";

        static void Main(string[] args)
        {
            try
            {
                ConfigWatcher wc = new ConfigWatcher(configurationFile);

                Console.ReadKey();

                wc.Shutdown();
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}", e.StackTrace);
            }
        }
    }
}
