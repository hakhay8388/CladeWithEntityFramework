using Base.Data.nDatabaseService.nDatabase;
using Sys.Boundary.nData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sys.Data.nDatabaseService.nSystemEntities
{
    public class cNotificationEntity : cBaseEntity<cNotificationEntity>
    {
        public string ParameterObjects { get; set; }

        public int Type { get; set; }

        public int ChannelID { get; set; }

        public bool NotificationBroadcasted { get; set; }

        public DateTime ValidUntilDate { get; set; }

        public List<cNotificationUserDetailEntity> UserDetails { get; set; }

    }
}
