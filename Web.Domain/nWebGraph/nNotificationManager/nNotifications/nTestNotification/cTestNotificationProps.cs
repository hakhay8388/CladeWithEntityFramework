using Sys.Web.nWebGraph.nNotificationManager.nNotifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Domain.nWebGraph.nNotificationManager.nNotifications.nTestNotification
{
    public class cTestNotificationProps : cBaseNotificationProps
    {
        public virtual string TestName { get; set; }
        public virtual string TestProfileImage { get; set; }
        public virtual DateTime TestBookedDate { get; set; }
        public virtual DateTime TestStartDate { get; set; }

    }
}
