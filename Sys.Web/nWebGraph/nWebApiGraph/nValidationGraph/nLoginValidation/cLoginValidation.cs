using Sys.Web.Controllers;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nValidationResultAction;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph;
using System;
using Sys.Data.nDatabaseService;
using Bootstrapper.Core.nApplication;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nLoginCommand;

using Base.Data.nDatabaseService;

namespace Sys.Web.nWebGraph.nWebApiGraph.nValidationGraph.nSellerRegisterValidation
{
    public class cLoginValidation : cBaseValidation, ILoginReceiver
    {

		public cLoginValidation(cApp _App, cBaseWebGraph _WebGraph, IDataService _DataService)
			: base(_App, _WebGraph, _DataService)
		{
			WebGraph = _WebGraph;
		}

        public void ReceiveLoginData(cListenerEvent _ListenerEvent, IController _Controller, cLoginCommandData _ReceivedData)
        {
            cValidationResultProps __ValidationResultProps = new cValidationResultProps();

            if (string.IsNullOrEmpty(_ReceivedData.UserName))
            {
                __ValidationResultProps.ValidationItems.Add(new cValidationItem()
                {
                    FieldName = App.Handlers.LambdaHandler.GetObjectPropName(() => _ReceivedData.UserName),
                    Success = false,
                    Message = _Controller.GetWordValue("Error")
                });
            }


            if (__ValidationResultProps.ValidationItems.Count > 0)
            {
                _ListenerEvent.StopPropogation();
            }

            WebGraph.SysActionGraph.ValidationResultAction.Action(_Controller, __ValidationResultProps);
        }
	}
}