using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using Bootstrapper.Core.nExceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrapper.Core.nHandlers.nScriptHandler.nMethodExecuter
{
    public class cMethodExecuter : cExecuterBase
    {
        public cMethodExecuter(cApp _App)
            :base(_App)
        {
        }

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cMethodExecuter>(this);
        }

        public TResult ExecMethod<TResult>(string _MethodFullName, string _ClassUsingResourse, string _ClassReplaceWithString, List<string> _DllList,  params object[] _Arguments)
        {
            bool __FailIfNotExist = !_MethodFullName.Trim().StartsWith("?");
            string __InvokeMethodFullName = __FailIfNotExist ? _MethodFullName.Trim() : _MethodFullName.Trim().Substring(1).Trim();
            cScriptingMethodNameParser _ScriptingMethod = new cScriptingMethodNameParser(__InvokeMethodFullName);
            object __Value = default(TResult);
            if (_ScriptingMethod.Valid)
            {
                if (_ScriptingMethod.IsCustomMethodType)
                {
                    __Value = ExecCustomMethod(_ScriptingMethod, __FailIfNotExist, _ClassUsingResourse, _ClassReplaceWithString, _DllList, _Arguments);
                }
                else
                {
                    __Value = ExecBuiltinMethod(_ScriptingMethod, __FailIfNotExist, _ClassUsingResourse, _ClassReplaceWithString, _Arguments);
                }
            }
            return (TResult)__Value;
        }

        private object ExecBuiltinMethod(cScriptingMethodNameParser _ScriptingMethod, bool _FailIfNotExist, string _ClassUsingResourse, string _ClassReplaceWithString, params object[] _Args)
        {
            Type __Type = App.Handlers.AssemblyHandler.FindFirstType(_ScriptingMethod.Namespace + "." + _ScriptingMethod.TypeName);

            if (__Type != null)
            {
                object __Instance = App.Factories.ObjectFactory.ResolveInstance(__Type);
                MethodInfo __MethodInfo = __Instance.GetType().GetMethod(_ScriptingMethod.MethodName);

                if (__MethodInfo != null)
                {
                    return CallInstanceMethod(__Instance, __MethodInfo,_FailIfNotExist, _Args);
                }
                else if (_FailIfNotExist)
                {
                    //throw new cCoreException(RS.Message.E1032_BuiltinMethodNotFound, methodFullName);
                    throw new cCoreException(App, "Scripting Error");
                }
            }
            else if (_FailIfNotExist)
            {
                //throw new cCoreException(RS.Message.E1031_BuiltinTypeNotFound, parser.TypeName);
                throw new cCoreException(App, "Scripting Error");
            }
            return null;
        }

        private object ExecCustomMethod(cScriptingMethodNameParser _ScriptingMethod, bool _FailIfNotExist, string _ClassUsingResourse, string _ClassReplaceWithString, List<string> _DllList, params object[] _Args)
        {
            string __ClassFileName = _ScriptingMethod.TypeName + ".cs";
            string __PhysicalPath = App.Handlers.FileHandler.FindFile(App.Configuration.ScriptPath, __ClassFileName); 

            if (File.Exists(__PhysicalPath))
            {
                string __Source = App.Handlers.FileHandler.ReadString(__PhysicalPath);
                string __ClassSource = _ClassUsingResourse.Replace(_ClassReplaceWithString, __Source);

                Assembly __Assembly = LoadAssembly(__ClassSource, _DllList);
                Type[] __Types = __Assembly.GetTypes();
                Type __Type = __Types.FirstOrDefault(x => x.Name.Equals(_ScriptingMethod.TypeName, StringComparison.InvariantCultureIgnoreCase)); // sometimes case of classname is unknown, eg: Report template

                if (__Type != null)
                {
                    object __Instance = GetInstance(__Type);
                    MethodInfo __MethodInfo = __Instance.GetType().GetMethod(_ScriptingMethod.MethodName);
                    return CallInstanceMethod(__Instance, __MethodInfo, _FailIfNotExist, _Args);
                }
                else if (_FailIfNotExist)
                    //throw new cCoreException(RS.Message.E1026_ScriptingTypeNotFound, parser.TypeName);
                    throw new cCoreException(App, "Scripting Error");
            }
            else if (_FailIfNotExist)
                //throw new cCoreException(RS.Message.E1022_FileNotFound, classFileName);
                throw new cCoreException(App, "Scripting Error");
            return null;
        }

     
    }
}
