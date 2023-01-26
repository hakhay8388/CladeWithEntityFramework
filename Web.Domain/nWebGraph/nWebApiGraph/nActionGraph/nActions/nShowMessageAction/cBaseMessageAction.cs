using Newtonsoft.Json.Linq;
using Web.Domain.nUtils.nValueTypes;
using Web.Domain.nWebGraph.nSessionManager;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nMessageResultCommand;
using System;
using Bootstrapper.Core.nApplication;
using Web.Domain.Controllers;
using System.Collections.Generic;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nShowMessageAction
{
    public abstract class cBaseMessageAction : cBaseActionWithProps<cMessageProps>
    {
        public cBaseMessageAction(cApp _App, cWebGraph _WebGraph, ActionIDs _ActionID)
           : base(_App, _WebGraph, _ActionID)
        {
        }


        protected JObject DesignAction<T>(IController _Controller, cBaseMessageProps<T> _MessageProps, List<cSession> _SignalSessions = null)
        {
            if (_MessageProps.MessageButtons.ID == EMessageButtons.Ok.ID)
            {
                _MessageProps.FirstButtonEnabled = true;
                _MessageProps.FirstButtonText = _Controller.GetWordValue("Ok");
                return InnerAction(_Controller, _MessageProps);
            }
            else if (_MessageProps.MessageButtons.ID == EMessageButtons.YesNo.ID)
            {
                _MessageProps.FirstButtonEnabled = true;
                _MessageProps.FirstButtonText = _Controller.GetWordValue("Yes");
                _MessageProps.SecondButtonEnabled = true;
                _MessageProps.SecondButtonText = _Controller.GetWordValue("No");
                return InnerAction(_Controller, _MessageProps);
            }
            else if (_MessageProps.MessageButtons.ID == EMessageButtons.OkCancel.ID)
            {
                _MessageProps.FirstButtonEnabled = true;
                _MessageProps.FirstButtonText = _Controller.GetWordValue("Ok");
                _MessageProps.SecondButtonEnabled = true;
                _MessageProps.SecondButtonText = _Controller.GetWordValue("Cancel");
                return InnerAction(_Controller, _MessageProps);
            }
            else if (_MessageProps.MessageButtons.ID == EMessageButtons.YesNoCancel.ID)
            {
                _MessageProps.FirstButtonEnabled = true;
                _MessageProps.FirstButtonText = _Controller.GetWordValue("Yes");
                _MessageProps.SecondButtonEnabled = true;
                _MessageProps.SecondButtonText = _Controller.GetWordValue("No");

                _MessageProps.ThirdButtonEnabled= true;
                _MessageProps.ThirdButtonText = _Controller.GetWordValue("Cancel");
                return InnerAction(_Controller, _MessageProps);
            }
            else if (_MessageProps.MessageButtons.ID == EMessageButtons.YesNoClose.ID)
            {
                _MessageProps.FirstButtonEnabled = true;
                _MessageProps.FirstButtonText = _Controller.GetWordValue("Yes");
                _MessageProps.SecondButtonEnabled = true;
                _MessageProps.SecondButtonText = _Controller.GetWordValue("No");
                _MessageProps.ThirdButtonEnabled = true;
                _MessageProps.ThirdButtonText = _Controller.GetWordValue("Close");
                return InnerAction(_Controller, _MessageProps);
            }
            if (_MessageProps.MessageButtons.ID == EMessageButtons.None.ID)
            {
                _MessageProps.FirstButtonEnabled = false;
                _MessageProps.CloseRequired = false;
                return InnerAction(_Controller, _MessageProps);
            }
            else
            {
                _MessageProps.FirstButtonEnabled = true;
                _MessageProps.FirstButtonText = _Controller.GetWordValue("Close");
                return InnerAction(_Controller, _MessageProps);
            }
        }


        protected JObject InnerAction<T>(IController _Controller, cBaseMessageProps<T> _MessageProps, List<cSession> _SignalSessions = null)
        {
            if (_MessageProps.Action != null)
            {
                WebGraph.CommandGraph.ConnectToCommands(new cMessageResultReceiver<T>(App, WebGraph, _Controller, _MessageProps.Action));
            }

            JObject __JsonObject = new JObject();
            __JsonObject["MessageButtonsID"] = _MessageProps.MessageButtons.ID;
            if (_MessageProps.RequestObject != null)
            {
                __JsonObject["RequestObject"] = JObject.FromObject(_MessageProps.RequestObject);
            }
            else
            {
                __JsonObject["RequestObject"] = null;
            }

            if (_MessageProps.MessageButtons.ID == EMessageButtons.Close.ID || _MessageProps.MessageButtons.ID == EMessageButtons.Ok.ID)
            {
                __JsonObject["DefaultButtonNo"] = 1;
            }
            else if (_MessageProps.MessageButtons.ID == EMessageButtons.OkCancel.ID)
            {
                if (EMessageButton.Ok.ID == _MessageProps.DefaultMessageButton.ID)
                {
                    __JsonObject["DefaultButtonNo"] = 1;
                }
                else
                {
                    __JsonObject["DefaultButtonNo"] = 2;
                }
            }
            else if (_MessageProps.MessageButtons.ID == EMessageButtons.YesNo.ID)
            {
                if (EMessageButton.Yes.ID == _MessageProps.DefaultMessageButton.ID)
                {
                    __JsonObject["DefaultButtonNo"] = 1;
                }
                else
                {
                    __JsonObject["DefaultButtonNo"] = 2;
                }
            }
            else if (_MessageProps.MessageButtons.ID == EMessageButtons.YesNoCancel.ID)
            {
                if (EMessageButton.Yes.ID == _MessageProps.DefaultMessageButton.ID)
                {
                    __JsonObject["DefaultButtonNo"] = 1;
                }
                if (EMessageButton.No.ID == _MessageProps.DefaultMessageButton.ID)
                {
                    __JsonObject["DefaultButtonNo"] = 2;
                }
                else
                {
                    __JsonObject["DefaultButtonNo"] = 3;
                }
            }
            else if (_MessageProps.MessageButtons.ID == EMessageButtons.YesNoClose.ID)
            {
                if (EMessageButton.Yes.ID == _MessageProps.DefaultMessageButton.ID)
                {
                    __JsonObject["DefaultButtonNo"] = 1;
                }
                if (EMessageButton.No.ID == _MessageProps.DefaultMessageButton.ID)
                {
                    __JsonObject["DefaultButtonNo"] = 2;
                }
                else
                {
                    __JsonObject["DefaultButtonNo"] = 3;
                }
            }
            __JsonObject["Message"] = JObject.FromObject(new { Type = _MessageProps.ColorType.Name, Header = _MessageProps.Header, Message = _MessageProps.Message });
            __JsonObject["Buttons"] = JObject.FromObject(new
            {
                First = new
                {
                    Enabled = _MessageProps.FirstButtonEnabled,
                    Type = _MessageProps.FirstButtonColorType.Name,
                    Text = _MessageProps.FirstButtonText
                }
                ,
                Second = new
                {
                    Enabled = _MessageProps.SecondButtonEnabled,
                    Type = _MessageProps.SecondButtonColorType.Name,
                    Text = _MessageProps.SecondButtonText
                }
                ,
                Third = new
                {
                    Enabled = _MessageProps.ThirdButtonEnabled,
                    Type = _MessageProps.ThirdButtonColorType.Name,
                    Text = _MessageProps.ThirdButtonText
                }
            });
            __JsonObject["CloseRequired"] = _MessageProps.CloseRequired;
            return __JsonObject;
        }
    }
}
