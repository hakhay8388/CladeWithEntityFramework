using Web.Domain.Controllers;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nValidationResultAction;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph;
using System;
using Data.Domain.nDatabaseService;
using Bootstrapper.Core.nApplication;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nLoginCommand;

namespace Web.Domain.nWebGraph.nWebApiGraph.nValidationGraph.nSellerRegisterValidation
{
    public class cLoginValidation : cBaseValidation, ILoginReceiver
    {

		public cLoginValidation(cApp _App, cWebGraph _WebGraph, cDataService _DataService)
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

            WebGraph.ActionGraph.ValidationResultAction.Action(_Controller, __ValidationResultProps);
        }
	}
}