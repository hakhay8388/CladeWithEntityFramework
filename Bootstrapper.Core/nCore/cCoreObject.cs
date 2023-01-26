using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nApplication.nBootstrapper;
using Bootstrapper.Core.nApplication.nConfiguration;
using Bootstrapper.Core.nApplication.nCoreLoggers;
using Bootstrapper.Core.nApplication.nFactories;
using Bootstrapper.Core.nHandlers;
using Bootstrapper.Core.nUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrapper.Core.nCore
{
    public abstract class cCoreObject 
    {
        public cApp App { get; set; }

        public cCoreObject(cApp _App)
        {
            App = _App;
        }

        public virtual void Init()
        {
        }

        public new Type GetType()
        {
            Type __Type = base.GetType();
            if (__Type.FullName.Contains("__Proxy__"))
            {
                __Type = __Type.BaseType;
            }
            return __Type;
        }
    }
}
