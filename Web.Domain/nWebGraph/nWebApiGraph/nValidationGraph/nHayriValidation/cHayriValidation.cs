using Web.Domain.Controllers;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nValidationResultAction;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph;
using System;
using Data.Domain.nDatabaseService;
using Bootstrapper.Core.nApplication;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nLoginCommand;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nHayriCommand;

namespace Web.Domain.nWebGraph.nWebApiGraph.nValidationGraph.nHayriValidation
{
    public class cHayriValidation : cBaseValidation, IHayriReceiver
    {

		public cHayriValidation(cApp _App, cWebGraph _WebGraph, cDataService _DataService)
			: base(_App, _WebGraph, _DataService)
		{
			WebGraph = _WebGraph;
		}

        public void ReceiveHayriData(cListenerEvent _ListenerEvent, IController _Controller, cHayriCommandData _ReceivedData)
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