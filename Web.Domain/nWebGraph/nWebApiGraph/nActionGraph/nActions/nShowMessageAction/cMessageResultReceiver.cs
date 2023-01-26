using Bootstrapper.Core.nApplication;
using Web.Domain.Controllers;
using Web.Domain.nWebGraph.nSessionManager;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nMessageResultCommand;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootstrapper.Core.nCore;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nShowMessageAction
{
    public class cMessageResultReceiver<T> : cCoreObject,  IMessageResultReceiver
    {
        DateTime RequestTime { get; set; }
        IController Controller { get; set; }
        Action<cBaseCommand, IController, EMessageButton, T> InnerAction { get; set; }

        cWebGraph WebGraph { get; set; }
        public cMessageResultReceiver(cApp _App, cWebGraph _WebGraph, IController _Controller, Action<cBaseCommand, IController, EMessageButton, T> _Action)
            :base(_App)
        {
            WebGraph = _WebGraph;
            Controller = _Controller;
            InnerAction = _Action;
            RequestTime = DateTime.Now;
        }

        public void ReceiveMessageResultData(cListenerEvent _ListenerEvent, IController _Controller, cMessageResultCommandData _ReceivedData)
        {
            if (Controller.ClientSession.SessionID == _Controller.ClientSession.SessionID)
            {
                WebGraph.CommandGraph.DisconnectToCommands(this);
				try
				{
					T __RequestObject = default(T);

					if (_ReceivedData.RequestObject != null)
					{
						__RequestObject = ((JObject)_ReceivedData.RequestObject).ToObject<T>();
					}


					if (EMessageButtons.Close.ID == _ReceivedData.MessageButtonsID)
					{
						InnerAction(_ListenerEvent.SenderCommand, _Controller, EMessageButton.Close, __RequestObject);
					}
					else if (EMessageButtons.Ok.ID == _ReceivedData.MessageButtonsID || EMessageButtons.OkCancel.ID == _ReceivedData.MessageButtonsID)
					{
						if (_ReceivedData.MessageButtonNo == 1)
						{
							InnerAction(_ListenerEvent.SenderCommand, _Controller, EMessageButton.Ok, __RequestObject);
						}
						else if (_ReceivedData.MessageButtonNo == 2)
						{
							InnerAction(_ListenerEvent.SenderCommand, _Controller, EMessageButton.Cancel, __RequestObject);
						}
					}
					else if (EMessageButtons.YesNo.ID == _ReceivedData.MessageButtonsID || EMessageButtons.YesNoCancel.ID == _ReceivedData.MessageButtonsID || EMessageButtons.YesNoClose.ID == _ReceivedData.MessageButtonsID)
					{
						if (_ReceivedData.MessageButtonNo == 1)
						{
							InnerAction(_ListenerEvent.SenderCommand, _Controller, EMessageButton.Yes, __RequestObject);
						}
						else if (_ReceivedData.MessageButtonNo == 2)
						{
							InnerAction(_ListenerEvent.SenderCommand, _Controller, EMessageButton.No, __RequestObject);
						}
						if (_ReceivedData.MessageButtonNo == 3)
						{
							if (EMessageButtons.YesNoCancel.ID == _ReceivedData.MessageButtonsID)
							{
								InnerAction(_ListenerEvent.SenderCommand, _Controller, EMessageButton.Cancel, __RequestObject);
							}
							else if (EMessageButtons.YesNoClose.ID == _ReceivedData.MessageButtonsID)
							{
								InnerAction(_ListenerEvent.SenderCommand, _Controller, EMessageButton.Close, __RequestObject);
							}
						}
					}
				}
				catch(Exception _Ex)
				{
					App.Loggers.CoreLogger.LogError(_Ex);
				}
            }
            else if ((DateTime.Now - RequestTime).TotalMinutes > 30)
            {
                WebGraph.CommandGraph.DisconnectToCommands(this);
            }
        }
    }
}
