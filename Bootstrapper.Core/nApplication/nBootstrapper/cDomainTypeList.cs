using Bootstrapper.Core.nAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrapper.Core.nApplication.nBootstrapper
{
    public class cDomainTypeList
    {
        public string DomainName { get; private set; }
        public List<cTypeInheritLevel> SortedTypeList { get; private set; }

        public cBootstrapper Bootstrapper { get; set; }

        public cDomainTypeList(cApp _App, cBootstrapper _Bootstrapper, string _DomainName)
        {
            Bootstrapper = _Bootstrapper;
            DomainName = _DomainName;

            SortedTypeList = Bootstrapper.GetTypeInheritLevel(_App.Handlers.AssemblyHandler.GetLoadedApplicationTypesByCustomAttribute<Register>(_DomainName));
        }
    }
}
