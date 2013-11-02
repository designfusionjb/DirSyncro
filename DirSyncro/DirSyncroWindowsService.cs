using System;
using System.ServiceProcess;
using System.ServiceModel;
using System.ComponentModel;
using System.Configuration.Install;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirSyncro
{
    class DirSyncroWindowsService : ServiceBase
    {
        public ServiceHost serviceHost = null;

        public DirSyncroWindowsService()
        {
            // Name the Windows Service
            ServiceName = "DirSyncro Service";
        }

        public static void Main()
        {
            ServiceBase.Run(new DirSyncroWindowsService());
        }

        protected override void OnStart(string[] args)
        {
            if (serviceHost != null)
                serviceHost.Close();

            serviceHost = new ServiceHost(typeof(SyncInterface));
            serviceHost.Open();
        }

        protected override void OnStop()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }
        }

        // Provide the ProjectInstaller class which allows 
        // the service to be installed by the Installutil.exe tool
        [RunInstaller(true)]
        public class ProjectInstaller : Installer
        {
            private ServiceProcessInstaller process;
            private ServiceInstaller service;

            public ProjectInstaller()
            {
                process = new ServiceProcessInstaller();
                process.Account = ServiceAccount.LocalSystem;
                service = new ServiceInstaller();
                service.ServiceName = "DirSyncro Service";
                Installers.Add(process);
                Installers.Add(service);
            }
        }
    }
}
