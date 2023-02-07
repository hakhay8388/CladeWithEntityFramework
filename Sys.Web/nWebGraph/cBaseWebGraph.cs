using Bootstrapper.Boundary.nCore.nObjectLifeTime;
using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nAttributes;
using Bootstrapper.Core.nCore;
using Microsoft.AspNetCore.Http;
using Sys.Web.Controllers;
using Sys.Web.nWebGraph.nErrorMessageManager;
using Sys.Web.nWebGraph.nNotificationManager;
using Sys.Web.nWebGraph.nSessionManager;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph;
using Sys.Web.nWebGraph.nWebApiGraph.nListenerGraph;
using Sys.Web.nWebGraph.nWebApiGraph.nValidationGraph;

namespace Sys.Web.nWebGraph
{
    public abstract class cBaseWebGraph : cCoreObject
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
        public cBaseActionGraph SysActionGraph { get; set; }
        public cCommandGraph CommandGraph { get; set; }
        public cListenerGraph ListenerGraph { get; set; }
        public cValidationGraph ValidationGraph { get; set; }

		public cErrorMessageManager ErrorMessageManager { get; set; }

        public cBaseNotificationManager SysNotificationManager { get; set; }

        public cBaseWebGraph(cApp _App)
            :base(_App)
        {
        }

        public TActionGraph ActionGraph<TActionGraph>() where TActionGraph : cBaseActionGraph
        {
            return (TActionGraph)SysActionGraph;
        }

        protected virtual void ResolveAll()
        {
            SessionManagerServices = App.Factories.ObjectFactory.ResolveInstance<cSessionManagerServices>();
            CommandGraph = App.Factories.ObjectFactory.ResolveInstance<cCommandGraph>();
            ListenerGraph = App.Factories.ObjectFactory.ResolveInstance<cListenerGraph>();
            ValidationGraph = App.Factories.ObjectFactory.ResolveInstance<cValidationGraph>();
            ErrorMessageManager = App.Factories.ObjectFactory.ResolveInstance<cErrorMessageManager>();
        }


        public override void Init()
        {
            ResolveAll();

            SessionManagerServices.Init();
            SysActionGraph.Init();
            CommandGraph.Init();
            ListenerGraph.Init();
            ValidationGraph.Init();
            SysNotificationManager.Init();

        }
    }
}
