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
using Web.Domain.Controllers;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nNotificationAction;
using Web.Domain.nWebGraph.nSessionManager;
using Domain.Data.nDatabaseService;
using Base.Data.nDatabaseService;

namespace Web.Domain.nWebGraph.nNotificationManager
{
    public class cNotificationManager : cCoreObject
    {
        DateTime LastExecutionTime { get; set; }
        cWebGraph WebGraph { get; set; }
        IDataService DataService { get; set; }
        cNotificationDataManager NotificationDataManager { get; set; }

        public cTestNotification TestNotification { get; set; }

        public cNotificationManager(cApp _App, cWebGraph _WebGraph, IDataService _DataService, cNotificationDataManager _NotificationDataManager)
            : base(_App)
        {
            WebGraph = _WebGraph;
            DataService = _DataService;
            NotificationDataManager = _NotificationDataManager;
            LastExecutionTime = DateTime.Now;
        }

        public override void Init()
        {
            TestNotification = App.Factories.ObjectFactory.ResolveInstance<cTestNotification>();
        }


        public List<dynamic> GetLastNotifications(cUserEntity _Actor)
        {
            List<dynamic> __Notifications = NotificationDataManager.GetTopNotificationsForAllChannel(_Actor, 5, __Item =>
            {

                __Item.NotificationID = __Item.ID;
                __Item.ParameterObjects = JObject.Parse(__Item.ParameterObjects.Replace("{{", "{").Replace("}}", "}"));
            });
            return __Notifications;
        }


        public void StartNotificationBroadCaster(IController _Controller)
        {
            try
            {
                if ((DateTime.Now - LastExecutionTime).TotalSeconds > 15)
                {
                    lock (this)
                    {
                        if ((DateTime.Now - LastExecutionTime).TotalSeconds > 15)
                        {
                            lock (this)
                            {
                                LastExecutionTime = DateTime.Now;


                                List<cNotificationEntity> __Notifications = NotificationDataManager.GetNotBroadcasstedNotification();
                                if (__Notifications.Count > 0)
                                {
                                    cSysDatabaseContext __DatabaseContext = DataService.GetDatabaseContext<cSysDatabaseContext>();
                                    __DatabaseContext.Perform(() =>
                                    {
                                        for (int i = 0; i < __Notifications.Count; i++)
                                        {
                                            string __ParameterObjectsString = __Notifications[i].ParameterObjects.Replace("{{", "{").Replace("}}", "}");
                                            object __ParameterObjects = JsonConvert.DeserializeObject(__ParameterObjectsString);

                                            cNotificationProps __NotificationProps = new cNotificationProps(__Notifications[i].ID,
                                                ENotificationChannel.GetByID(__Notifications[i].ChannelID, ENotificationChannel.GlobalChannel),
                                                ENotificationType.GetByID(__Notifications[i].Type, ENotificationType.None),
                                                __ParameterObjects);

                                            List<long> __ActorIDList = new List<long>();

                                            __Notifications[i].Load(__Item => __Item.UserDetails);
                                            foreach (var __ActorDetail in __Notifications[i].UserDetails.ToList())
                                            {
                                                __ActorDetail.Load(__Item => __Item.User);
                                                __ActorIDList.Add(__ActorDetail.User.FirstOrDefault().ID);
                                            }

                                            List<cSession> __Sessions = WebGraph.SessionManager(_Controller).GetSessionByUserID(__ActorIDList).Where(__Item => __Item.SignalRIDList.Count > 0).ToList();

                                            if (__Sessions.Count > 0)
                                            {
                                                WebGraph.ActionGraph.NotificationAction.Action(_Controller, __NotificationProps, __Sessions, true);
                                            }

                                            __Notifications[i].NotificationBroadcasted = true;
                                            __Notifications[i].Save();
                                        }
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception _Ex)
            {
                List<string> __List = new List<string>();
                __List.Add("####################################################");
                __List.Add("###### NotificationBroadCaster Error   #############");
                __List.Add("####################################################");
                App.Loggers.BatchJobLogger.LogError(__List, _Ex, null);
            }
        }
    }
}
