using Base.Data.nDatabaseService.nDatabase;
using Data.Boundary.nData;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain.nDatabaseService.nEntities
{
    public class cPaymentEntity : cBaseEntity<cPaymentEntity>
    {

        public string Name { get; set; }

        public double Price { get; set; }

        public List<cOrderEntity> Orders { get; set; }
    }
}
