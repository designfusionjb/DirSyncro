using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DirSyncro
{
    abstract class SyncJob
    {
        public SyncJob()
        {
        }

        protected void CreatePath(DirectoryInfo fullTarget)
        {
            if (!fullTarget.Exists)
            {
                if (!fullTarget.Parent.Exists)
                    CreatePath(fullTarget.Parent);

                fullTarget.Create();
            }
        }

        protected string CreateEpoch()
        {
            return Math.Round((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString();
        }
    }
}
