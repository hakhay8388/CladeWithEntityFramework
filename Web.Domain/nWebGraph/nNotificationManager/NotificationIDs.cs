using Bootstrapper.Boundary.nValueTypes.nConstType;
using Sys.Boundary.nData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Web.Domain.nWebGraph.nNotificationManager
{
    public class NotificationIDs
    {
        public static ENotificationType Test = new ENotificationType(nameof(Test), 100);
    }
}
