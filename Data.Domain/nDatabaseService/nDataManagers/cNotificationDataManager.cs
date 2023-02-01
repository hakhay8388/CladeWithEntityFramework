using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Data.Boundary.nData;
using Base.Data.nDatabaseService;
using Data.Domain.nDatabaseService;
using Data.Domain.nDatabaseService.nSystemEntities;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.Style;

namespace Data.Domain.nDataService.nDataManagers
{
    public class cNotificationDataManager : cBaseDataManager
    {
        public cNotificationDataManager(cDataServiceContext _CoreServiceContext, cDataService _DataService, IFileDateService _FileDataService)
          : base(_CoreServiceContext, _DataService, _FileDataService)
        {
        }

        public cNotificationEntity AddNotification(List<cUserEntity> _ReceiverUser, ENotificationChannel _NotificationChannel, ENotificationType _ENotificationType, string _ParamObject,  DateTime _ValidUntilDate, bool _Broadcasted)
        {
            //cNotificationEntity

            cNotificationEntity __NotificationEntity = cNotificationEntity.Add(new cNotificationEntity()
            {
                ChannelID = _NotificationChannel.ID,
                Type = _ENotificationType.ID,
                ParameterObjects = _ParamObject,
                ValidUntilDate = _ValidUntilDate,
                NotificationBroadcasted = _Broadcasted,
            });
            

            _ReceiverUser.ForEach(__Item =>
            {
                __NotificationEntity.UserDetails.Add(new cNotificationUserDetailEntity()
                {
                    Readed = false,
                    User = new List<cUserEntity>() { __Item }
                });
            });

            __NotificationEntity.Save();

            return __NotificationEntity;
        }


		public cNotificationUserDetailEntity GetNotificationByIDAndActorID(long _UserID, long _NotificationID)
		{
            return cNotificationUserDetailEntity.Get(__Item => __Item.User.Any(__Item => __Item.ID == _UserID) && __Item.Notification.ID == _NotificationID).FirstOrDefault();
		}

		public List<cNotificationEntity> GetMyNotification(cUserEntity _User, ENotificationChannel _NotificationChannel)
        {
            List< cNotificationEntity> __Result = cNotificationEntity.Get(__Item => __Item.ChannelID == _NotificationChannel.ID && __Item.UserDetails.Any(__Item => __Item.User.Any(__Item => __Item.ID == _User.ID)))
                               .OrderByDescending(__Item => __Item.CreateDate)
                               .ToList();
            return __Result;
        }


        public List<dynamic> GetTopNotificationsForAllChannel(cUserEntity _User, int _TopNotificationCount, Action<dynamic> _Action)
        {
            cDatabaseContext __DatabaseContext = DataService.GetDatabaseContext();

            DbSet<cNotificationEntity> __Notifications = __DatabaseContext.Notifications;
            DbSet<cNotificationUserDetailEntity> __NotificationUserDetails = __DatabaseContext.NotificationUserDetails;

            List<dynamic> __Result = __Notifications.Join(
            __NotificationUserDetails,
            __NotificationUserDetails => __NotificationUserDetails.ID,
            __Notifications => __Notifications.Notification.ID,
            (__Notifications, __NotificationUserDetails) => new
            {
                ParameterObjects = __Notifications.ParameterObjects,
                Type = __Notifications.Type,
                ChannelID = __Notifications.ChannelID,
                NotificationBroadcasted = __Notifications.NotificationBroadcasted,
                ValidUntilDate = __Notifications.ValidUntilDate,
                Readed = __NotificationUserDetails.Readed
            }).ToDynamicObjectList();

            return __Result;
        }

        public List<cNotificationEntity> GetNotBroadcasstedNotification()
        {
            return cNotificationEntity.Get(__Item => !__Item.NotificationBroadcasted).ToList();
        }

        public int DeleteOldNotificationDate(DateTime _Date)
        {
            return cNotificationEntity.RemoveRange(__Item => __Item.ValidUntilDate < _Date);
        }
    }
}
