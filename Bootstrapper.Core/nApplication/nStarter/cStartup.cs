//using Bootstrapper.Core.nApplication.nCoreLoggers.nMethodCallLogger;
using Bootstrapper.Core.nAttributes;
using Bootstrapper.Core.nCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrapper.Core.nApplication.nStarter
{
    [Register]
    public class cStartup<TStarter> : cCoreObject, IStarter where TStarter : IStarter
    {
        public TStarter StarterInstance { get; set; }
        public cStartup(cApp _App)
            :base(_App)
        {
            StarterInstance = _App.Factories.ObjectFactory.ResolveInstance<TStarter>();
        }
        public void Start(cApp _App)
        {
            CultureInfo.DefaultThreadCurrentCulture = App.Configuration.UICulture;
            CultureInfo.DefaultThreadCurrentUICulture = App.Configuration.UICulture;

            //Ön yükleme yapılacak

            StarterInstance.Start(_App);

        }

     
    }
}
