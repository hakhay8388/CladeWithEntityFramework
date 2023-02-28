using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Base.Data.nDatabaseService.nDatabase
{
    public class cBaseEntityType
    {

        [Key]
        public long ID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public static IDataService DataService { get; set; }
    }
}
