using Base.Data.nDatabaseService.nDatabase;
using Data.Domain.nDefaultValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain.nDatabaseService.nSystemEntities
{
    public class cDataSourcePermissionEntity : cBaseEntity<cDataSourcePermissionEntity>
    {
        public bool CanRead { get; set; }

        public bool CanCreate { get; set; }

        public bool CanUpdate { get; set; }

        public bool CanDelete { get; set; }

        public int DataSourceID { get; set; }

        public string DataSourceCode { get; set; }

        public List<cRoleEntity> Roles { get; set; }

        public DataSourceIDs DataSource
        {
            get
            {
                return DataSourceIDs.GetByID(DataSourceID, null);
            }
        }

    }
}
