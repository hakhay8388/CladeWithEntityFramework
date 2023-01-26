using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;
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
    public class cCompilerResult
    {
        public EmitResult CompilerResult { get; private set; }
        public string ErrorString { get; private set; }
        public bool IsSuccess { get; private set; }
        public Assembly DynamicAssembly { get; private set; }
        
        public cCompilerResult(EmitResult _CompilerResults, MemoryStream _MemoryStream)
        {
            if (_CompilerResults.Success)
            {
                _MemoryStream.Seek(0, SeekOrigin.Begin);

                DynamicAssembly = AssemblyLoadContext.Default.LoadFromStream(_MemoryStream);
            }
            else
            {
                ErrorString = GetCompilerError(_CompilerResults);
            }

            IsSuccess = _CompilerResults.Success;
            CompilerResult = _CompilerResults;

        }

        private string GetCompilerError(EmitResult _CompilerResults)
        {
            IEnumerable<Diagnostic> __Failures = _CompilerResults.Diagnostics.Where(diagnostic =>
                diagnostic.IsWarningAsError ||
                diagnostic.Severity == DiagnosticSeverity.Error);

            string __Error = "";
            foreach (Diagnostic __Item in __Failures)
            {
                __Error += " ID : " + __Item.Id + " -> " + __Item.GetMessage() + "\r\n";
            }
            return __Error.IsNullOrEmpty() ? __Error : "ScriptInvoker compile errors : \r\n" + __Error;
        }
    }
}
