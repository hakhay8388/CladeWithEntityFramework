using Bootstrapper.Boundary.nValueTypes.nConstType;
using Sys.Boundary.nDefaultValueTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Boundary.nDefaultValueTypes
{
    public class DomainDataSourceIDs : DataSourceIDs
    {
        public static DataSourceIDs TestDataSource { get; set; }

        public static void Init()
        {
            TestDataSource = new DataSourceIDs(GetVariableName(() => TestDataSource), "TTestDataSource", "TestDataSource", 10, new List<RoleIDs>() { RoleIDs.Admin });
        }

        public DomainDataSourceIDs(string _Code, string _ClientComponentName, string _Name, int _ID, List<RoleIDs> _MainRoles)
            : base(_Code, _ClientComponentName, _Name, _ID, _MainRoles)
        {
        }
    }
}
