using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using System.Collections.Generic;
using System.IO;

using Bootstrapper.Core.nExceptions;
using Bootstrapper.Core.nHandlers.nScriptHandler.nMethodExecuter;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrapper.Core.nScripting
{
    public abstract class cScriptingBase : cCoreObject, IScripting
    {
        public cScriptingBase(cApp _App)
            : base(_App)
        {
            App = _App;
        }

        /// <summary>
        /// istenilen kodların tamamı bir metodun içine yazılıyormuş gibi yazılmalı, sonra sonucu "Result" içine atılması gerekiyor.
        /// /// istenildiği kadar parametre gönderilebilir. Parametreler sırası ile, _Param0, _Param1 _Param2,....
        /// diye devam eder.
        /// Örnek : 
        /// string __Temp = DenemeServiceContext.Scripting.EvalCode<string>("Result = Console.ReadLine();", new object[] {});
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_CodeSnippet"></param>
        /// <param name="_Args"></param>
        /// <returns></returns>
        public T EvalCode<T>(string _CodeSnippet, params object[] _Args)
        {
            T __Value = default(T);
            __Value = (T)App.Handlers.ScriptHandler.SnippetExecuter.EvalCode<T>(_CodeSnippet, GetClassSourceTemplate(), GetClassReplaceWithStringInTemplate(), GetReferencedAssemblies(), _Args);
            return __Value;

        }

        /// <summary>
        /// istenilen kodların tamamı bir metodun içine yazılıyormuş gibi yazılmalı.
        /// istenildiği kadar parametre gönderilebilir. Parametreler sırası ile, _Param0, _Param1 _Param2,....
        /// diye devam eder.
        /// Örnek :
        /// DenemeServiceContext.Scripting.EvalCode("Console.WriteLine(_Param0);", __Temp);
        /// </summary>
        /// <param name="_CodeSnippet"></param>
        /// <param name="_Args"></param>
        public void EvalCode(string _CodeSnippet, params object[] _Args)
        {
            App.Handlers.ScriptHandler.SnippetExecuter.EvalCode(_CodeSnippet, GetClassSourceTemplate(), GetClassReplaceWithStringInTemplate(), GetReferencedAssemblies(), _Args);
        }

        /// <summary>
        /// Script yazıldıysa ve script içinden bir metot çağrılacaksa veya instance oluşturulu bir method çağıma işmi gerekiyorsa
        /// bu metot kullanılır.
        /// Örnek :
        /// DenemeServiceContext.Scripting.ExecMethod<int>("Scripting.MyDeneme.Mesaj", DenemeServiceContext);
        /// Eğer bu kod varsa çalıştır yoksa hata vermesin isteniliyorsa başına "?" konulur.
        /// Örnek :
        /// DenemeServiceContext.Scripting.ExecMethod<int>("? Scripting.MyDeneme.Mesaj", DenemeServiceContext);
        /// Eğer c# içinde olan bir classtan instance oluşturulup onun içindeki birmethod çağrılacaksa yanı şekilde çağrılabilir
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="_MethodFullName"></param>
        /// <param name="_Args"></param>
        /// <returns></returns>
        public TResult ExecMethod<TResult>(string _MethodFullName, params object[] _Args)
        {
            TResult __Value = default(TResult);
            __Value = App.Handlers.ScriptHandler.MethodExecuter.ExecMethod<TResult>(_MethodFullName, GetClassSourceTemplate(), GetClassReplaceWithStringInTemplate(), GetReferencedAssemblies(), _Args);
            return __Value;
        }
      
        protected abstract string GetClassSourceTemplate(); //for now, only "using" lines are prepended to source, user must write his code starting with "namespace Scripting..."
        protected abstract string GetClassReplaceWithStringInTemplate(); //for now, only "using" lines are prepended to source, user must write his code starting with "namespace Scripting..."
        protected abstract string GetCodeSnippetBaseClassName(); // used for codeSnippets, other script may include their own class names and injections
        protected abstract string GetCodeSnippetInjectionClassName(); // used for codeSnippets, other script may include their own class names and injections

        private string GetClassSource(string _Source)
        {
            string __Value = GetClassSourceTemplate();
            __Value = __Value.Replace("SOURCE", _Source ?? string.Empty);
            return __Value;
        }

        public virtual List<string> GetReferencedAssemblies()
        {
            List<string> __Result = new List<string>();
            __Result.Add(Path.Combine(Path.GetDirectoryName(typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly.Location), "System.Runtime.dll"));
            __Result.Add(Path.Combine(Path.GetDirectoryName(typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly.Location), "System.dll"));
            __Result.Add(Path.Combine(Path.GetDirectoryName(typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly.Location), "System.Linq.dll"));
            __Result.Add(Path.Combine(Path.GetDirectoryName(typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly.Location), "System.Data.dll"));
            __Result.Add(Path.Combine(Path.GetDirectoryName(typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly.Location), "System.Core.dll"));
            __Result.Add(Path.Combine(Path.GetDirectoryName(typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly.Location), "System.Linq.Expressions.dll"));
            __Result.Add(Path.Combine(Path.GetDirectoryName(typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly.Location), "System.Xml.dll"));
            __Result.Add(Path.Combine(Path.GetDirectoryName(typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly.Location), "System.Xml.Linq.dll"));
            __Result.Add(Path.Combine(Path.GetDirectoryName(typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly.Location), "Microsoft.CSharp.dll"));
            __Result.Add(Path.Combine(App.Configuration.BinPath, "Bootstrapper.Core.dll"));
            return __Result;
        }

    }
}
