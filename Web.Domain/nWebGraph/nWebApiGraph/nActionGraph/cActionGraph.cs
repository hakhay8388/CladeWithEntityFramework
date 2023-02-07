using Bootstrapper.Core.nApplication;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nTestAction;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph
{
    public class cActionGraph : cBaseActionGraph
    {

        public cTestAction TestAction { get; set; }

        public cActionGraph(cApp _App, cWebGraph _WebGraph)
            : base(_App, _WebGraph)
        {
        }
    }
}