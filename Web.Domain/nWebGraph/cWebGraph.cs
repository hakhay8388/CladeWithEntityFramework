using Bootstrapper.Boundary.nCore.nObjectLifeTime;
using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nAttributes;
using Bootstrapper.Core.nCore;
using Web.Domain.Controllers;
using Web.Domain.nWebGraph.nErrorMessageManager;
using Web.Domain.nWebGraph.nSessionManager;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph;
using Web.Domain.nWebGraph.nWebApiGraph.nListenerGraph;
using Web.Domain.nWebGraph.nWebApiGraph.nValidationGraph;

namespace Web.Domain.nWebGraph
{
    [Register(typeof(cWebGraph), false, true, true, true, LifeTime.ContainerControlledLifetimeManager)]
    public class cWebGraph : cCoreObject
    {
        public cSessionManagerServices SessionManagerServices { get; set; }
        public cSessionManager SessionManager(HttpContext _HttpContext)
        { 
           return SessionManagerServices.SessionManager(_HttpContext);
        }

        public cSessionManager SessionManager(IController _Controler)
        {
            return SessionManagerServices.SessionManager(_Controler.CurrentContext);
        }
        public cActionGraph ActionGraph { get; set; }
        public cCommandGraph CommandGraph { get; set; }
        public cListenerGraph ListenerGraph { get; set; }
        public cValidationGraph ValidationGraph { get; set; }

		public cErrorMessageManager ErrorMessageManager { get; set; }


		public cWebGraph(cApp _App)
            :base(_App)
        {
        }

        public override void Init()
        {
            SessionManagerServices = App.Factories.ObjectFactory.ResolveInstance<cSessionManagerServices>();
            ActionGraph = App.Factories.ObjectFactory.ResolveInstance<cActionGraph>();
            CommandGraph = App.Factories.ObjectFactory.ResolveInstance<cCommandGraph>();
            ListenerGraph = App.Factories.ObjectFactory.ResolveInstance<cListenerGraph>();
            ValidationGraph = App.Factories.ObjectFactory.ResolveInstance<cValidationGraph>();
			ErrorMessageManager = App.Factories.ObjectFactory.ResolveInstance<cErrorMessageManager>();


			SessionManagerServices.Init();
            ActionGraph.Init();
            CommandGraph.Init();
            ListenerGraph.Init();
            ValidationGraph.Init();
		}
    }
}
