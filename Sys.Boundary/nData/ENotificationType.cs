using Bootstrapper.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Sys.Boundary.nData
{
    public class ENotificationType : cBaseConstType<ENotificationType>
    {
        public static ENotificationType None = new ENotificationType(GetVariableName(() => None), 0);
        public static ENotificationType Sample = new ENotificationType(GetVariableName(() => Sample), 1);



        public static List<ENotificationType> TypeList { get; set; }

        public ENotificationType(string _Code, int _ID)
            : base(_Code, _Code, _ID)
        {
            TypeList = TypeList ?? new List<ENotificationType>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static ENotificationType GetByID(int _ID, ENotificationType _DefaultData)
        {
            return GetByID(TypeList, _ID, _DefaultData);
        }
        public static ENotificationType GetByCode(string _Code, ENotificationType _DefaultData)
        {
            return GetByCode(TypeList, _Code, _DefaultData);
        }
        public static ENotificationType GetByName(string _Name, ENotificationType _DefaultData)
        {
            return GetByName(TypeList, _Name, _DefaultData);
        }
    }
}
