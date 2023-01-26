using Base.Data.nDatabaseService.nDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain.nDatabaseService.nSystemEntities
{
    public class cUserSessionEntity : cBaseEntity<cUserSessionEntity>
    {
        public string SessionHash { get; set; }
        public string IpAddress { get; set; }

        public cUserEntity User { get; set; }
    }
}
