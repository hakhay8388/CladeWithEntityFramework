using Bootstrapper.Core.nApplication;
using Data.Domain.nDatabaseService;
using Web.Domain.Controllers;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetGlobalParamListCommand;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSetGlobalParamListAction;

namespace Web.Domain.nWebGraph.nWebApiGraph.nListenerGraph.nParamListener
{
     public class cParamListener : cBaseListener
        , IGetGlobalParamListReceiver
    {
        public cParamListener(cApp _App, cWebGraph _WebGraph, cDataService _DataService)
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
            List<object> __ClonedGlobalParams = DataService.GlobalParams.PublicParamList.CloneOnlyList();
            cSetGlobalParamListProps __Result = new cSetGlobalParamListProps() {ParamList = __ClonedGlobalParams };
            return __Result;
        }
    }
}
