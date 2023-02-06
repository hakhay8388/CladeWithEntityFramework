using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sys.Boundary.nData;
using Newtonsoft.Json.Linq;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nNotificationAction
{
    public class cNotificationProps : cBaseProps
    {
        public virtual long NotificationID { get; set; }

        public virtual int ChannelID { get; set; }

        public virtual int Type { get; set; }

        public virtual  object ParameterObjects { get; set; }
        public cNotificationProps(long _NotificationID, ENotificationChannel _NotificationChannel, ENotificationType _NotificationType, object _ParameterObjects)
            :base()
        {
            NotificationID = _NotificationID;
            ChannelID = _NotificationChannel.ID;
            Type = _NotificationType.ID;
            ParameterObjects = _ParameterObjects;
        }

    }
}
