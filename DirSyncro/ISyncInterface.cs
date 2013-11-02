using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading.Tasks;

namespace DirSyncro
{
    [ServiceContract(Namespace = "http:/DirSyncro")]
    interface ISyncInterface
    {
        [OperationContract]
        bool ServiceCommand(uint eventId, string objectId);
    }
}
