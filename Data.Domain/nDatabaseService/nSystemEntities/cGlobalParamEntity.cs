using Base.Data.nDatabaseService.nDatabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Domain.nDatabaseService.nSystemEntities
{
    public class cGlobalParamEntity : cBaseEntity<cGlobalParamEntity>
    {
        public int SortOrder { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Value { get; set; }

        public string TypeFullName { get; set; }

    }
}
