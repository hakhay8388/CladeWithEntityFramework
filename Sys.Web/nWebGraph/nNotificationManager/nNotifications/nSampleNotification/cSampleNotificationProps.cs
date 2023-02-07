using System;
using System.Collections.Generic;
using System.Text;

namespace Sys.Web.nWebGraph.nNotificationManager.nNotifications.nSampleNotification
{
    public class cSampleNotificationProps : cBaseNotificationProps
    {
        public virtual string TestName { get; set; }
        public virtual string TestProfileImage { get; set; }
        public virtual DateTime TestBookedDate { get; set; }
        public virtual DateTime TestStartDate { get; set; }

    }
}
