using Bootstrapper.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Sys.Boundary.nData
{
    public enum EnumSortDirectionTypes
    {
        Unspecified = -1,
        Ascending = 0,
        Descending = 1
    }

    public class ESortDirectionTypes : cBaseConstType<ESortDirectionTypes>
    {

        public static List<ESortDirectionTypes> TypeList { get; set; }

        public static ESortDirectionTypes Unspecified = new ESortDirectionTypes(GetVariableName(() => Unspecified), EnumSortDirectionTypes.Unspecified);
        public static ESortDirectionTypes Ascending = new ESortDirectionTypes(GetVariableName(() => Ascending), EnumSortDirectionTypes.Ascending);
        public static ESortDirectionTypes Descending = new ESortDirectionTypes(GetVariableName(() => Descending), EnumSortDirectionTypes.Descending);

        public ESortDirectionTypes(string _Name, EnumSortDirectionTypes _ColorTypes)
            : base(_Name, _Name, (int)_ColorTypes)
        {
            TypeList = TypeList ?? new List<ESortDirectionTypes>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static ESortDirectionTypes GetByID(EnumSortDirectionTypes _ID, ESortDirectionTypes _DefaultCommandID)
        {
            return GetByID(TypeList, (int)_ID, _DefaultCommandID);
        }
        public static ESortDirectionTypes GetByName(string _Name, ESortDirectionTypes _DefaultCommandID)
        {
            return GetByName(TypeList, _Name, _DefaultCommandID);
        }
    }
}
