using Bootstrapper.Boundary.nCore.nObjectLifeTime;
using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nAttributes;
using Bootstrapper.Core.nCore;
using Sys.Web.nWebGraph;
using Sys.Web.nWebGraph.nNotificationManager;
using Sys.Web.nWebGraph.nSessionManager;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph;
using Web.Domain.nWebGraph.nNotificationManager;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph;
using Web.Domain.nWebGraph.nWebApiGraph.nListenerGraph;
using Web.Domain.nWebGraph.nWebApiGraph.nValidationGraph;

namespace Web.Domain.nWebGraph
{
    public class cWebGraph : cBaseWebGraph
    {
        public new cActionGraph ActionGraph { get; set; }
        public new cNotificationManager NotificationManager { get; set; }

        public cWebGraph(cApp _App)
            :base(_App)
        {
        }

        protected override void ResolveAll()
        {
            base.ResolveAll();

            ActionGraph = App.Factories.ObjectFactory.ResolveInstance<cActionGraph>();
            NotificationManager = App.Factories.ObjectFactory.ResolveInstance<cNotificationManager>();

        }
    }
}
