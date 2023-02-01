using Bootstrapper.Core.nApplication;
using Data.Boundary.nData;
using Data.Domain.nDataService.nDataManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.nWebGraph.nNotificationManager.nNotifications.nTestNotification
{
    public class cTestNotification : cBaseNotification<cTestNotificationProps>
    {
        public cTestNotification(cApp _App, cWebGraph _WebGraph, cNotificationDataManager _NotificationDataManager)
          : base(ENotificationType.Test, _App, _WebGraph, _NotificationDataManager)
        {
        }

        public override int LiveSecond()
        {
            return 3600 * 24 * 7;
        }
    }
}
