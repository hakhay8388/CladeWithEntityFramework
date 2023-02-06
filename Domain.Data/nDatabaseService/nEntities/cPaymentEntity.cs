using Base.Data.nDatabaseService.nDatabase;
using Sys.Boundary.nData;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Data.nDatabaseService.nEntities
{
    public class cPaymentEntity : cBaseEntity<cPaymentEntity>
    {

        public string Name { get; set; }

        public double Price { get; set; }

    }
}
