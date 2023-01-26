using Bootstrapper.Boundary.nCore.nBootType;
using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Base.Web.nCustomDI
{
    public class cUnityContainerBuilder : cCoreObject
    {
        public IServiceCollection Services { get; set; }
        public cUnityContainerBuilder(cApp _App, IServiceCollection _Services) 
            :base(_App)
        {
            Services = _Services;
        }

        public cUnityServiceProvider BuildServiceProvider()
        {
            //App.Factories.ObjectFactory.RegisterInstance<IConfiguration>(Configuration);

            cUnityServiceProvider __UnityServiceProvider = new cUnityServiceProvider(App.Factories.ObjectFactory.DependencyContainer);


            Services.AddSingleton<IControllerActivator>(new cUnityControllerActivator(App.Factories.ObjectFactory.DependencyContainer));

            IServiceProvider __DefaultProvider = Services.BuildServiceProvider();

            App.Factories.ObjectFactory.DependencyContainer.AddExtension(new cUnityFallbackProviderExtension(__DefaultProvider));

            return __UnityServiceProvider;
        }
    }
}
