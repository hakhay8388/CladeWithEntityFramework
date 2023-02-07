using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Sys.Boundary.nData;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Bootstrapper.Core.nCore;
using Sys.Data.nDatabaseService;
using Bootstrapper.Core.nApplication;
using Sys.Data.nDataService.nDataManagers;
using Web.Domain.nWebGraph.nNotificationManager.nNotifications.nTestNotification;
using Sys.Data.nDatabaseService.nSystemEntities;
using Domain.Data.nDatabaseService;
using Base.Data.nDatabaseService;
using Sys.Web.nWebGraph.nNotificationManager;

namespace Web.Domain.nWebGraph.nNotificationManager
{
    public class cNotificationManager : cBaseNotificationManager
    {
        public cTestNotification TestNotification { get; set; }

        public cNotificationManager(cApp _App, cWebGraph _WebGraph, IDataService _DataService, cNotificationDataManager _NotificationDataManager)
            : base(_App, _WebGraph, _DataService, _NotificationDataManager)
        {
        }

        public override void Init()
        {
            TestNotification = App.Factories.ObjectFactory.ResolveInstance<cTestNotification>();
        }


    }
}
