using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Configuration;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace DirSyncro
{
    class Program
    {
        private static readonly string configurationFile = @"DirSyncro.xml";
        private static ServiceHost serviceHost = null;

        static void Main(string[] args)
        {
            try
            {
                if (serviceHost != null)
                    serviceHost.Close();

                serviceHost = new ServiceHost(typeof(SyncService));
                serviceHost.Open();

                ConfigWatcher.Startup(configurationFile);

                Console.ReadKey();

                serviceHost.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("{0}", e.StackTrace);
                Console.ReadKey();
            }
        }
    }
}
