using Base.FileData.nConfiguration;
using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;

namespace Base.FileData.nFileDataService
{
    public class cFileDataServiceContext : cCoreServiceContext
    {
        public cFileDataConfiguration Configuration { get { return App.Cfg< cFileDataConfiguration>(); } }

        public cFileDataServiceContext(cApp _App)
            :base(_App)
        {
        }
    }
}
