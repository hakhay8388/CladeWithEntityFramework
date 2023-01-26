using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Web.nCustomDI
{
    public class cUnityServiceProviderFactory : cCoreObject, IServiceProviderFactory<cUnityContainerBuilder>
    {
        private IServiceCollection Services { get; set; }

        public cUnityServiceProviderFactory(cApp _App)
            : base(_App)
        {
        }

        public cUnityContainerBuilder CreateBuilder(IServiceCollection _Services)
        {
            Services = _Services;
            return new cUnityContainerBuilder(App, _Services);
        }

        public IServiceProvider CreateServiceProvider(cUnityContainerBuilder _ContainerBuilder)
        {

            return _ContainerBuilder.BuildServiceProvider(); 
        }
    }
}
