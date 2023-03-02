using Base.Data.nDatabaseService.nDatabase;
using Sys.Boundary.nData;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Data.nDatabaseService.nEntities
{
    public class cLawsuitEntity : cBaseEntity<cLawsuitEntity>
    {

        public string Name { get; set; }

        public string Key { get; set; }

    }
}
