using Bootstrapper.Core.nHandlers.nStringHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrapper.Core.nHandlers.nScriptHandler.nMethodExecuter
{
    public class cScriptingMethodNameParser
    {
        public string Namespace { get; set; }
        public string TypeName { get; set; }
        public string MethodName { get; set; }
        public string MethodFullName { get; set; }
        public bool IsCustomMethodType { get; set; }
        public bool Valid { get; set; }
        public cScriptingMethodNameParser(string _MethodFullName)
        {
            Valid = Parse(_MethodFullName);
        }
        private bool Parse(string _MethodFullName)
        {
            cStringList __List = new cStringList(_MethodFullName, ".");

            if (__List.Items.Count < 3)
            {
                return false;
            }

            TypeName = __List.Items[__List.Items.Count - 2];
            MethodName = __List.Items[__List.Items.Count - 1];

            __List.Items.RemoveAt(__List.Items.Count - 1);
            __List.Items.RemoveAt(__List.Items.Count - 1);

            Namespace = __List.GetValue(".");
            MethodFullName = _MethodFullName;

            IsCustomMethodType = _MethodFullName.StartsWith("Scripting.");

            return true;
        }
    }
}
