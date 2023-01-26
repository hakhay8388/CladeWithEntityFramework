using Base.Data.nDatabaseService.nDatabase;

using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Domain.nDatabaseService.nSystemEntities
{
    public class cDefaultDataChecksumEntity : cBaseEntity<cDefaultDataChecksumEntity>
    {
        public string Code { get; set; }

        public string CheckSum { get; set; }
    }
}
