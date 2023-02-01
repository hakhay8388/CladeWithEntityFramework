using Base.Data.nDatabaseService.nDatabase;
using Data.Boundary.nData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Domain.nDatabaseService.nSystemEntities
{
    public class cNotificationUserDetailEntity : cBaseEntity<cNotificationUserDetailEntity>
    {
        public bool Readed { get; set; }

        public List<cUserEntity> User { get; set; }
        public cNotificationEntity Notification { get; set; }

    }
}
