using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DirSyncro
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISyncService" in both code and config file together.
    [ServiceContract]
    public interface ISyncService
    {
        [OperationContract]
        bool ServiceCommand(uint eventId, string objectId);
    }
}
