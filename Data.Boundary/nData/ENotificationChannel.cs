using Bootstrapper.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Data.Boundary.nData
{
    public enum ENotificationChannelEnums
    {
        GlobalChannel = 0,
        UpcomingLessonChannel = 1
    }

    public class ENotificationChannel : cBaseConstType<ENotificationChannel>
    {
        public static ENotificationChannel GlobalChannel = new ENotificationChannel(GetVariableName(() => GlobalChannel), (int)ENotificationChannelEnums.GlobalChannel, "GlobalChannel");
        
        public static List<ENotificationChannel> TypeList { get; set; }

        public ENotificationChannel(string _Code, int _ID, string _Name)
            : base(_Name, _Code, _ID)
        {
            TypeList = TypeList ?? new List<ENotificationChannel>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static ENotificationChannel GetByID(int _ID, ENotificationChannel _DefaultData)
        {
            return GetByID(TypeList, _ID, _DefaultData);
        }
        public static ENotificationChannel GetByCode(string _Code, ENotificationChannel _DefaultData)
        {
            return GetByCode(TypeList, _Code, _DefaultData);
        }
        public static ENotificationChannel GetByName(string _Name, ENotificationChannel _DefaultData)
        {
            return GetByName(TypeList, _Name, _DefaultData);
        }
    }
}
