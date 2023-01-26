using Bootstrapper.Core.nApplication.nConfiguration;
using Bootstrapper.Core.nApplication.nCoreLoggers;
//using Bootstrapper.Core.nApplication.nCoreLoggers.nMethodCallLogger;
using Bootstrapper.Core.nApplication.nFactories.nFormFactory;
using Bootstrapper.Core.nApplication.nFactories.nHookedObjectFactory;
using Bootstrapper.Core.nApplication.nFactories.nObjectFactory;
using Bootstrapper.Core.nAttributes;
using Bootstrapper.Core.nCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrapper.Core.nApplication.nFactories
{
    public class cFactories : cCoreObject
    {
        public cObjectFactory ObjectFactory { get; set; }
        public cFormFactory FormFactory { get; set; }
        public cHookedObjectFactory HookedObjectFactory { get; set; }
        public cFactories(cApp _App)
            :base(_App)
        {
            ObjectFactory = new cObjectFactory(_App);
            FormFactory = new cFormFactory(_App);
            HookedObjectFactory = new cHookedObjectFactory(_App);
        }

        public override void Init()
        {
            ObjectFactory.Init();
            ObjectFactory.RegisterInstance<cFactories>(this);
            FormFactory.Init();
            HookedObjectFactory.Init();            
        }
    }
}
