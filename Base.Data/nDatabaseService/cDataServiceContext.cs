using Base.Data.nConfiguration;
using Base.FileData.nConfiguration;
using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.nDatabaseService
{
    public class cDataServiceContext : cCoreServiceContext
    {
        public cDataConfiguration Configuration { get { return App.Cfg<cDataConfiguration>(); } }

        public cDataServiceContext(cApp _App)
            : base(_App)
        {
        }
    }
}
