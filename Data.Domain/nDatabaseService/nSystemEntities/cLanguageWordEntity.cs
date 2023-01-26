using Base.Data.nDatabaseService.nDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain.nDatabaseService.nSystemEntities
{
    public class cLanguageWordEntity : cBaseEntity<cLanguageWordEntity>
    {
        public string Code { get; set; }

        public string Word { get; set; }

        public string Description { get; set; }

        public int ParamCount { get; set; }

        public string CheckSum { get; set; }

        public cLanguageEntity Language { get; set; }

    }
}
