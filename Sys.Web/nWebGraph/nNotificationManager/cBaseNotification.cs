using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using Sys.Boundary.nData;
using Sys.Data.nDatabaseService.nSystemEntities;
using Sys.Data.nDataService.nDataManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sys.Web.Controllers;
using Sys.Web.nWebGraph.nNotificationManager.nNotifications;
using Sys.Web.nWebGraph.nSessionManager;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nNotificationAction;

namespace Sys.Web.nWebGraph.nNotificationManager
{
    public abstract class cBaseNotification<TNotificationParameter> : cCoreObject
        where TNotificationParameter : cBaseNotificationProps
    {
        public ENotificationType NotificationType { get; set; }
        public cBaseWebGraph WebGraph { get; set; }



        public cNotificationDataManager NotificationDataManager { get; set; }

        public cBaseNotification(ENotificationType _NotificationType, cApp _App, cBaseWebGraph _WebGraph, cNotificationDataManager _NotificationDataManager)
            : base(_App)
        {
            NotificationType = _NotificationType;
            WebGraph = _WebGraph;
            NotificationDataManager = _NotificationDataManager;

        }

        public override void Init()
        {
        }

        public abstract int LiveSecond();

        public virtual void Notify(IController _Controller, List<cUserEntity> _ReceiverActors, ENotificationChannel _NotificationChannel, TNotificationParameter _NotificationProps, bool _NoticationSendMeWithPostBack = true)
        {
            cNotificationEntity __NotificationEntity = NotificationDataManager.AddNotification(_ReceiverActors, _NotificationChannel, NotificationType, _NotificationProps.SerializeObject(), DateTime.Now.AddSeconds(LiveSecond()), true);

            List<long> __UserIDList = _ReceiverActors.Select<cUserEntity, long>(__Item => __Item.ID).ToList();

            List<cSession> __Sessions = WebGraph.SessionManager(_Controller).GetSessionByUserID(__UserIDList).Where(__Item => __Item.SignalRIDList.Count > 0).ToList();

            cNotificationProps __NotificationActionProps = new cNotificationProps(__NotificationEntity.ID, _NotificationChannel, NotificationType, _NotificationProps);

            if (__Sessions.Count > 0)
            {
                //MicroService.MicroServiceActionGraph.NotificationAction.BroadcastAction(new cMicroServiceNotificationProps(__UserIDList, __NotificationEntity.ID, _NotificationChannel, NotificationType, _NotificationProps));

                WebGraph.ActionGraph.NotificationAction.Action(_Controller, __NotificationActionProps, __Sessions, true);
            }
            //todo: bildirim gelmiyorsa eski koda dön
        }
    }
}
