using Base.Data.nDatabaseService.nDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain.nDatabaseService.nSystemEntities
{
    public class cRoleMenuMapEntity : cBaseEntity<cRoleMenuMapEntity>
    {
        public long cMenuEntityID { get; set; }
        public cMenuEntity Menu { get; set; }


        public long cRoleEntityID { get; set; }
        public cRoleEntity Role { get; set; }

        public int SortValue { get; set; }
}

}
