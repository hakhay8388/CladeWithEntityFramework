using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nApplication.nCoreLoggers;
//using Bootstrapper.Core.nApplication.nCoreLoggers.nMethodCallLogger;
using Bootstrapper.Core.nCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrapper.Core.nApplication.nFactories.nHookedObjectFactory.nPropertyHookedObjectFactory
{
    public abstract class cPropertyHookController : cCoreObject
    {
        public cPropertyHookController()
            : base(null)
        {
        }
        public abstract void HookedFuction(object _Instance, string _PropertyName, object _PropertyInner);
    }
}
