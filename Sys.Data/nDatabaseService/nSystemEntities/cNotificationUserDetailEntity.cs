using Base.Data.nDatabaseService.nDatabase;
using Sys.Boundary.nData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sys.Data.nDatabaseService.nSystemEntities
{
    public class cNotificationUserDetailEntity : cBaseEntity<cNotificationUserDetailEntity>
    {
        public bool Readed { get; set; }

        public List<cUserEntity> User { get; set; }
        public cNotificationEntity Notification { get; set; }

    }
}
