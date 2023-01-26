using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using Bootstrapper.Core.nExceptions;
using Bootstrapper.Core.nScripting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrapper.Core.nHandlers.nScriptHandler.nMethodExecuter
{
    public class cCodeSnippetExecuter : cExecuterBase
    {
        public cCodeSnippetExecuter(cApp _App)
            :base(_App)
        {
        }

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cCodeSnippetExecuter>(this);
        }
        public object EvalCode<TResult>(string _CodeSnippet, string _ClassUsingResourse, string _ClassReplaceWithString, List<string> _DllList, params object[] _Parameters)
        {
            return LoadTempAssembly(_CodeSnippet, typeof(TResult), _ClassUsingResourse, _ClassReplaceWithString, _DllList, _Parameters);
        }

        public void EvalCode(string _CodeSnippet, string _ClassUsingResourse, string _ClassReplaceWithString, List<string> _DllList, params object[] _Parameters)
        {
            LoadTempAssembly(_CodeSnippet,null, _ClassUsingResourse, _ClassReplaceWithString, _DllList, _Parameters);
        }

        private object LoadTempAssembly(string _CodeSnippet, Type _ResultType, string _ClassUsingResourse, string _ClassReplaceWithString, List<string> _DllList, params object[] _Parameters)
        {
            string __Source = GetSnippetString(_CodeSnippet, _ResultType, GetParametersTypeList(_Parameters));
            string __ClassSource = _ClassUsingResourse.Replace(_ClassReplaceWithString, __Source);

            Assembly __Assembly = LoadAssembly(__ClassSource, _DllList);
            Type __Type = __Assembly.GetTypes().First(x => x.Name.StartsWith("CodeSnippet"));

            object __Instance = GetInstance(__Type);
            MethodInfo __MethodInfo = __Type.GetMethod("CodeSnippetMain");
            return CallInstanceMethod(__Instance, __MethodInfo, true, _Parameters);
        }


        public List<Type> GetParametersTypeList(params object[] _ParameterTypes)
        {
            List<Type> __Result = new List<Type>();
            foreach (object __Item in _ParameterTypes)
            {
                __Result.Add(__Item.GetType());
            }
            return __Result;
        }

        public string GetSnippetString(string _CodeSnippet, Type _ResultType, List<Type> _ParameterTypes)
        {
            string __TypeName = "CodeSnippet" + App.Utils.HashUtils.GetChecksum(_CodeSnippet);
            string __Source = "namespace Scripting { \r\n" +
                            "public class " + __TypeName + " : " + typeof(cCoreObject).Name + " { \r\n" +
                            "public " + __TypeName + "(" + typeof(cApp).Name + " _App) : base(_App) { }\r\n";
            if (_ResultType != null) __Source+= "public object CodeSnippetMain(";
            else __Source += "public void CodeSnippetMain(";

            for (int i = 0; i < _ParameterTypes.Count; i++)
            {
                if (i != 0) __Source += ", ";
                __Source += _ParameterTypes[i].Name + " _Param" + i.ToString();
            }

            __Source += ") { \r\n";

            if (_ResultType != null)
            {
                __Source += _ResultType.Name + " Result = default(" + _ResultType.Name + ");";
                __Source += _CodeSnippet + "\r\n";
                __Source += "return (" + _ResultType.Name + ") Result;" + "\r\n";
            }
            else
            {
                __Source +=  _CodeSnippet + "\r\n";
            }
            __Source += "}}}";

            return __Source;

        }

    }
}
