using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using Bootstrapper.Core.nExceptions;
using Bootstrapper.Core.nHandlers.nScriptHandler.nMethodExecuter;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrapper.Core.nHandlers.nScriptHandler
{
    public class cScriptHandler : cCoreObject
    {
        public cMethodExecuter MethodExecuter { get; set; }
        public cCodeSnippetExecuter SnippetExecuter { get; set; }
        public cScriptHandler(cApp _App)
            : base(_App)
        {
            MethodExecuter = new cMethodExecuter(_App);
            SnippetExecuter = new cCodeSnippetExecuter(_App);
        }

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cScriptHandler>(this);
            MethodExecuter.Init();
            SnippetExecuter.Init();
        }

        public Assembly LoadAssembly(string _CodeSource, List<string> _DllReference)
        {
            lock (App)
            {
                cCompilerResult __CompilerResults = Compile(_CodeSource, _DllReference);

                if (!__CompilerResults.IsSuccess)
                {
                    throw new cCoreException(App, __CompilerResults.ErrorString);
                }
                Assembly __Assembly = __CompilerResults.DynamicAssembly;
                return __Assembly;
            }
        }

        public cCompilerResult Compile(string _Source, List<string> _DllReference)
        {
           
            SyntaxTree __SyntaxTree = CSharpSyntaxTree.ParseText(_Source);

            string __AssemblyName = Path.GetRandomFileName();

            _DllReference.Insert(0, typeof(System.Object).GetTypeInfo().Assembly.Location);
            _DllReference.Insert(0, typeof(Console).GetTypeInfo().Assembly.Location);

            MetadataReference[] __References = _DllReference.ToArray().Select(__Item => MetadataReference.CreateFromFile(__Item)).ToArray();

           


            CSharpCompilation __Compilation = CSharpCompilation.Create(
                __AssemblyName,
                syntaxTrees: new[] { __SyntaxTree },
                references: __References,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            MemoryStream __MemoryStream = new MemoryStream();

            EmitResult __Result = __Compilation.Emit(__MemoryStream);

            return new cCompilerResult(__Result, __MemoryStream);

        }



    }
}
