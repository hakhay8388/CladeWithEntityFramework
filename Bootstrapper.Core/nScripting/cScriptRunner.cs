using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nAttributes;
using Bootstrapper.Core.nCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrapper.Core.nScripting
{
    [Register(typeof(IScripting))]
    public class cScriptRunner : cScriptingBase
    {
        public cScriptRunner(cApp _App)
            : base(_App)
        {
            App.Factories.ObjectFactory.RegisterInstance<IScripting>(this);
        }
        public override void Init()
        {
            base.Init();

        }
        protected override string GetClassSourceTemplate()
        {
            return Bootstrapper.Core.Properties.Resources.SCRIPTING_CLASS_SOURCE;
        }
        protected override string GetCodeSnippetBaseClassName()
        {
            return typeof(cCoreService<>).Name;
        }
        protected override string GetCodeSnippetInjectionClassName()
        {
            return typeof(cCoreServiceContext).Name;
        }

        protected override string GetClassReplaceWithStringInTemplate()
        {
            return "SOURCE";
        }
    }
}
