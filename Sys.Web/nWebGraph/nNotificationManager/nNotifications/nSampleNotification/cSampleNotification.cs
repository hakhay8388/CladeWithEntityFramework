using Bootstrapper.Core.nApplication;
using Sys.Boundary.nData;
using Sys.Data.nDataService.nDataManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sys.Web.nWebGraph.nNotificationManager.nNotifications.nSampleNotification
{
    public class cSampleNotification : cBaseNotification<cSampleNotificationProps>
    {
        public cSampleNotification(cApp _App, cBaseWebGraph _WebGraph, cNotificationDataManager _NotificationDataManager)
          : base(ENotificationType.Sample, _App, _WebGraph, _NotificationDataManager)
        {
        }

        public override int LiveSecond()
        {
            return 3600 * 24 * 7;
        }
    }
}
