using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using Bootstrapper.Core.nExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrapper.Core.nHandlers.nScriptHandler.nMethodExecuter
{
    public class cExecuterBase :  cCoreObject
    {
        public cExecuterBase(cApp _App)
            :base(_App)
        {
        }

        protected object CallInstanceMethod(object _Instance, MethodInfo _MethodInfo, bool _FailIfNotExist, params object[] _Args)
        {
            if (_MethodInfo != null)
            {
                string __FullName = _Instance.GetType().Namespace + "." + _Instance.GetType().Name + "." + _MethodInfo.Name;
                App.Loggers.CoreLogger.LogInfo("Start-ScriptingMethod: " + __FullName);
                object __Value = _MethodInfo.Invoke(_Instance, _Args);
                App.Loggers.CoreLogger.LogInfo("End-ScriptingMethod: " + __FullName);
                return __Value;
            }
            else if (_FailIfNotExist)
            {
                throw new cCoreException(App, "Scripting Error");
            }

            return null;
        }

        protected Assembly LoadAssembly(string _Source, List<string> _DllList)
        {
            return App.Handlers.ScriptHandler.LoadAssembly(_Source,  _DllList);
        }

        protected object GetInstance(Type _Type)
        {
            return App.Factories.ObjectFactory.ResolveInstance(_Type);
        }
    }
}
