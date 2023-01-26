using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.nDatabaseService
{
    public interface IDataService
    {
        DbContext GetCoreEFDatabaseContext();
    }
}
