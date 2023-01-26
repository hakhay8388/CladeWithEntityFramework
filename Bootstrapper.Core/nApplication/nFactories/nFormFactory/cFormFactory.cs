using Bootstrapper.Core.nApplication.nCoreLoggers;
////using Bootstrapper.Core.nApplication.nCoreLoggers.nMethodCallLogger;
using Bootstrapper.Core.nCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrapper.Core.nApplication.nFactories.nFormFactory
{
    public class cFormFactory : cCoreObject
    {
        public cFormFactory(cApp _App)
            :base(_App)
        {
        }

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cFormFactory>(this);
        }

        private TForm CreateFrom<TForm>()
        {
            TForm __Result = typeof(TForm).ResolveInstance<TForm>(App);
            //App.Factories.ObjectFactory.ResolveInnerObject(__Result);
            /*MethodInfo __MethodInfo = typeof(TForm).SearchMethod("Init");
            if (__MethodInfo != null)
            {
                __MethodInfo.Invoke(__Result, new object[] { });
            }*/
            return __Result;
        }

    }
}
