using Bootstrapper.Core.nApplication.nCoreLoggers;
//using Bootstrapper.Core.nApplication.nCoreLoggers.nMethodCallLogger;
using Bootstrapper.Core.nApplication.nFactories.nHookedObjectFactory.nPropertyHookedObjectFactory;
using Bootstrapper.Core.nAttributes;
using Bootstrapper.Core.nCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrapper.Core.nApplication.nFactories.nHookedObjectFactory
{
    public class cHookedObjectFactory : cCoreObject
    {
        public cPropertyHookedObjectFactory PropertyHookedObjectFactory { get; set; }
        public cHookedObjectFactory(cApp _App)
            :base(_App)
        {
            PropertyHookedObjectFactory = new cPropertyHookedObjectFactory(_App);
        }

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cHookedObjectFactory>(this);
        }
    }
}
