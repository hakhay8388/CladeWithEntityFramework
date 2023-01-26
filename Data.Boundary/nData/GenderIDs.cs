using Bootstrapper.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Boundary.nData
{
    public class GenderIDs : cBaseConstType<GenderIDs>
    {

        public static List<GenderIDs> TypeList { get; set; }

        public static GenderIDs Man = new GenderIDs(GetVariableName(() => Man), 1);
        public static GenderIDs Women = new GenderIDs(GetVariableName(() => Women), 2);

        public GenderIDs(string _Code, int _ID)
            : base(_Code, _Code, _ID)
        {
            TypeList = TypeList ?? new List<GenderIDs>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static GenderIDs GetByID(int _ID, GenderIDs _DefaultID)
        {
            return GetByID(TypeList, _ID, _DefaultID);
        }
        public static GenderIDs GetByName(string _Name, GenderIDs _DefaultID)
        {
            return GetByName(TypeList, _Name, _DefaultID);
        }

        public static GenderIDs GetByCode(string _Code, GenderIDs _DefaultID)
        {
            return GetByCode(TypeList, _Code, _DefaultID);
        }
    }
}
