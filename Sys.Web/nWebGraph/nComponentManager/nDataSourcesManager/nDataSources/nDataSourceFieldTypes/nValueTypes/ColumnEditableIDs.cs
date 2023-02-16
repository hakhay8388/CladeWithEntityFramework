using Bootstrapper.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Sys.Web.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nDataSourceFieldTypes.nValueTypes
{
    public class ColumnEditableIDs : cBaseConstType<ColumnEditableIDs>
    {
        public static List<ColumnEditableIDs> TypeList { get; set; }

        public static ColumnEditableIDs Always = new ColumnEditableIDs(GetVariableName(() => Always), "always", 1);
        public static ColumnEditableIDs Never = new ColumnEditableIDs(GetVariableName(() => Never), "never", 2);
        public static ColumnEditableIDs OnUpdate = new ColumnEditableIDs(GetVariableName(() => OnUpdate), "onUpdate", 3);
        public static ColumnEditableIDs OnAdd = new ColumnEditableIDs(GetVariableName(() => OnAdd), "onAdd", 4);

        public string ClientComponentName { get; set; }

        public ColumnEditableIDs(string _Name, string _Code, int _ID)
            : base(_Name, _Code, _ID)
        {
            TypeList = TypeList ?? new List<ColumnEditableIDs>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static ColumnEditableIDs GetByID(int _ID, ColumnEditableIDs _DefaultID)
        {
            return GetByID(TypeList, _ID, _DefaultID);
        }
        public static ColumnEditableIDs GetByName(string _Name, ColumnEditableIDs _DefaultID)
        {
            return GetByName(TypeList, _Name, _DefaultID);
        }
    }
}
