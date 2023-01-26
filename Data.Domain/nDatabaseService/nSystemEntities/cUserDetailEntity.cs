using Base.Data.nDatabaseService.nDatabase;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain.nDatabaseService.nSystemEntities
{
    public class cUserDetailEntity : cBaseEntity<cUserDetailEntity>
    {
        public string Telephone { get; set; }

        public int Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

    }
}
