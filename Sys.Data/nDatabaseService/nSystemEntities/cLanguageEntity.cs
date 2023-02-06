using Base.Data.nDatabaseService.nDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Data.nDatabaseService.nSystemEntities
{
    public class cLanguageEntity : cBaseEntity<cLanguageEntity>
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string IconCode { get; set; }

        public List<cLanguageWordEntity> Words { get; set; }
        
    }
}
