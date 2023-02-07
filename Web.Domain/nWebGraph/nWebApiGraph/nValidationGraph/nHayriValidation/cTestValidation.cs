using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph;
using System;
using Sys.Data.nDatabaseService;
using Bootstrapper.Core.nApplication;
using Domain.Data.nDatabaseService;
using Base.Data.nDatabaseService;
using Sys.Web.nWebGraph.nWebApiGraph.nValidationGraph;
using Sys.Web.Controllers;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nValidationResultAction;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nTestCommand;

namespace Web.Domain.nWebGraph.nWebApiGraph.nValidationGraph.nHayriValidation
{
    public class cTestValidation : cBaseValidation, ITestReceiver
    {

		public cTestValidation(cApp _App, cWebGraph _WebGraph, IDataService _DataService)
			: base(_App, _WebGraph, _DataService)
		{
			WebGraph = _WebGraph;
		}

        public void ReceiveTestData(cListenerEvent _ListenerEvent, IController _Controller, cTestCommandData _ReceivedData)
        {
            cValidationResultProps __ValidationResultProps = new cValidationResultProps();

            if (_ReceivedData.Price < 10)
            {
                __ValidationResultProps.ValidationItems.Add(new cValidationItem()
                {
                    FieldName = App.Handlers.LambdaHandler.GetObjectPropName(() => _ReceivedData.Price),
                    Success = false,
                    Message = "Uzunluk yanlış"
                });
            }

            if (__ValidationResultProps.ValidationItems.Count > 0)
            {
                _ListenerEvent.StopPropogation();
            }

            WebGraph.ActionGraph.ValidationResultAction.Action(_Controller, __ValidationResultProps);
        }

        
	}
}