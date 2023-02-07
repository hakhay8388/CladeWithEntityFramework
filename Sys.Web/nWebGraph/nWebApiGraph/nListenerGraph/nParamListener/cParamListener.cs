using Bootstrapper.Core.nApplication;
using Sys.Data.nDatabaseService;
using Sys.Web.Controllers;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetGlobalParamListCommand;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSetGlobalParamListAction;

using Base.Data.nDatabaseService;

namespace Sys.Web.nWebGraph.nWebApiGraph.nListenerGraph.nParamListener
{
     public class cParamListener : cBaseListener
        , IGetGlobalParamListReceiver
    {
        public cParamListener(cApp _App, cBaseWebGraph _WebGraph, IDataService _DataService)
               : base(_App, _WebGraph, _DataService)
        {
        }

        public void ReceiveGetGlobalParamListData(cListenerEvent _ListenerEvent, IController _Controller, cGetGlobalParamListCommandData _ReceivedData)
        {
            cSetGlobalParamListProps __SetGlobalParamListProps = PrepareGetGlobalParamListProps(_Controller, _ReceivedData);
            WebGraph.ActionGraph.SetGlobalParamListAction.Action(_Controller, __SetGlobalParamListProps);
        }

        public cSetGlobalParamListProps PrepareGetGlobalParamListProps(IController _Controller, cGetGlobalParamListCommandData _ReceivedData)
        {
            List<object> __ClonedGlobalParams = Params.GlobalParams.PublicParamList.CloneOnlyList();
            cSetGlobalParamListProps __Result = new cSetGlobalParamListProps() {ParamList = __ClonedGlobalParams };
            return __Result;
        }
    }
}
