
using Base.Data.nDatabaseService.nDatabase;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Data.nDatabaseService.nSystemEntities
{
    public class cPageEntity : cBaseEntity<cPageEntity>
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string Url { get; set; }

        public string ComponentName { get; set; }

        public List<cRoleEntity> Roles { get; set; }

        public List<cMenuEntity> Menus { get; set; }

    }
}
