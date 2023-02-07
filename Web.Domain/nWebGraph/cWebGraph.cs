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
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommandIDs;
using Web.Domain.nWebGraph.nWebApiGraph.nListenerGraph;
using Web.Domain.nWebGraph.nWebApiGraph.nValidationGraph;

namespace Web.Domain.nWebGraph
{
    [Register(typeof(cWebGraph), true, true, true, true, LifeTime.ContainerControlledLifetimeManager)]
    public class cWebGraph : cBaseWebGraph
    {
        public cActionGraph ActionGraph { get { return (cActionGraph)SysActionGraph; } }
        public new cNotificationManager NotificationManager { get { return (cNotificationManager)SysNotificationManager; } }

        public cWebGraph(cApp _App)
            :base(_App)
        {
        }

        public override void Init()
        {
            CommandIDs.Init();
            base.Init();
        }

        protected override void ResolveAll()
        {
            base.ResolveAll();

            SysActionGraph = App.Factories.ObjectFactory.ResolveInstance<cActionGraph>();
            SysNotificationManager = App.Factories.ObjectFactory.ResolveInstance<cNotificationManager>();
        }
    }
}
