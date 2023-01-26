using Base.Data.nDatabaseService.nDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain.nDatabaseService.nSystemEntities
{
    public class cRoleEntity : cBaseEntity<cRoleEntity>
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public virtual List<cRoleMenuMapEntity> Menus { get; set; }
        
        public virtual List<cPageEntity> Pages { get; set; }

        public List<cUserEntity> Users { get; set; }

        public List<cDataSourcePermissionEntity> DataSourcePermissions { get; set; }

        public List<cDataSourceColumnEntity> DataSourceColumns { get; set; }
    }

}
