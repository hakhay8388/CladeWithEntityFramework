using Base.Data.nDatabaseService.nDatabase;
using Data.Domain.nDatabaseService.nSystemEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain.nDatabaseService.nEntities
{
    public class cPaymentEntity : cBaseEntity<cPaymentEntity>
    {
        public double Price { get; set; }

        public string Name { get; set; }
    }
}
