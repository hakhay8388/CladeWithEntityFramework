using Base.Data.nDatabaseService.nDatabase;
using Data.Boundary.nData;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain.nDatabaseService.nEntities
{
    public class cOrderEntity : cBaseEntity<cOrderEntity>
    {

        public string Name { get; set; }

        public cPaymentEntity Payment { get; set; }

    }
}
