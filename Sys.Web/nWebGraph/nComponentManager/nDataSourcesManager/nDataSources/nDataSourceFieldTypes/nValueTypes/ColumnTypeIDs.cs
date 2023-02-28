using Bootstrapper.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Sys.Web.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nDataSourceFieldTypes.nValueTypes
{
    public class ColumnTypeIDs : cBaseConstType<ColumnTypeIDs>
    {
        public static List<ColumnTypeIDs> TypeList { get; set; }

        public static ColumnTypeIDs Boolean = new ColumnTypeIDs(GetVariableName(() => Boolean), 1);
        public static ColumnTypeIDs Numeric = new ColumnTypeIDs(GetVariableName(() => Numeric), 2);
        public static ColumnTypeIDs Date = new ColumnTypeIDs(GetVariableName(() => Date), 3);
        public static ColumnTypeIDs Datetime = new ColumnTypeIDs(GetVariableName(() => Datetime), 4);
        public static ColumnTypeIDs Time = new ColumnTypeIDs(GetVariableName(() => Time), 5);
        public static ColumnTypeIDs Currency = new ColumnTypeIDs(GetVariableName(() => Currency), 6);
        public static ColumnTypeIDs String = new ColumnTypeIDs(GetVariableName(() => String), 7);
        public static ColumnTypeIDs Avatar = new ColumnTypeIDs(GetVariableName(() => Avatar), 8);

        public ColumnTypeIDs(string _Code, int _ID)
            : base(_Code, _Code, _ID)
        {
            TypeList = TypeList ?? new List<ColumnTypeIDs>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static ColumnTypeIDs GetByID(int _ID, ColumnTypeIDs _DefaultID)
        {
            return GetByID(TypeList, _ID, _DefaultID);
        }
        public static ColumnTypeIDs GetByName(string _Name, ColumnTypeIDs _DefaultID)
        {
            return GetByName(TypeList, _Name, _DefaultID);
        }

        public static ColumnTypeIDs GetByCode(string _Code, ColumnTypeIDs _DefaultID)
        {
            return GetByCode(TypeList, _Code.ToLower(), _DefaultID);
        }
    }
}
