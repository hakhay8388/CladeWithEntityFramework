using Sys.Web.Controllers;
using Sys.Web.nUtils.nValueTypes;
using Sys.Web.nWebGraph.nSessionManager;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nHotSpotMessageAction;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nShowMessageAction;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nCheckLoginCommand;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nLoginCommand;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nLogoutCommand;
using Sys.Boundary.nDefaultValueTypes;
using System;
using System.Collections.Generic;
using Bootstrapper.Core.nApplication;
using Sys.Data.nDatabaseService;
using Sys.Boundary.nData;
using Sys.Data.nDatabaseService.nSystemEntities;
using Sys.Data.nDataService.nDataManagers;

using Base.Data.nDatabaseService;

namespace Sys.Web.nWebGraph.nWebApiGraph.nListenerGraph.nLogInOutListener
{
    public class cLogInOutListener : cBaseListener
		, ILoginReceiver
		, ILogoutReceiver
		, ICheckLoginReceiver
	{
        public cUserDataManager UserDataManager { get; set; }
        public cSessionDataManager SessionDataManager { get; set; }


        public cLogInOutListener(cApp _App, cBaseWebGraph _WebGraph, IDataService _DataService
            , cUserDataManager _UserDataManager
            , cSessionDataManager _SessionDataManager
            )
		   : base(_App, _WebGraph, _DataService)
		{
            UserDataManager = _UserDataManager;
            SessionDataManager = _SessionDataManager;
        }

		public void ReceiveLogoutData(cListenerEvent _ListenerEvent, IController _Controller, cLogoutCommandData _ReceivedData)
		{
            if (_Controller.ClientSession.IsLogined)
            {
                cMessageProps __MessageProps = new cMessageProps();
                __MessageProps.Header = _Controller.GetWordValue("Exit");
                __MessageProps.Message = _Controller.GetWordValue("DoYouWantToExit");
                __MessageProps.ColorType = EColorTypes.None;
                __MessageProps.MessageButtons = EMessageButtons.YesNo;
                __MessageProps.DefaultMessageButton = EMessageButton.No;

                __MessageProps.FirstButtonColorType = EColorTypes.Error;
                __MessageProps.FirstButtonEnabled = true;

                __MessageProps.SecondButtonColorType = EColorTypes.Success;
                __MessageProps.SecondButtonEnabled = true;
                __MessageProps.Action = delegate (cBaseCommand __BaseCommand, IController __InnerController, EMessageButton __MessageButton, object __RequestObject)
                {
                    if (__MessageButton.ID == EMessageButton.Yes.ID)
                    {
                        if (_Controller.ClientSession.IsLogined)
                        {
                            string __Session_ID = __InnerController.ClientSession.SessionID;
                            long _UserID = __InnerController.ClientSession.User.ID;

                            cSysDatabaseContext __DatabaseContext = DataService.GetDatabaseContext<cSysDatabaseContext>();

                            __DatabaseContext.Perform(() =>
                            {
                                SessionDataManager.DeleteSession(__Session_ID);
                                __DatabaseContext.SaveChanges();
                            });

                            __InnerController.Logout();

                            //WebGraph.ActionGraph.SuccessResultAction.Action(_Controller);
                            WebGraph.SysActionGraph.ReinitAction.Action(__InnerController, new List<cSession>() { __InnerController.ClientSession }, true);
                        }
                        //WebGraph.ActionGraph.DoCheckLoginRequestAction.Action(__InnerController, new cDoCheckLoginRequestProps() { IsLogined = false }, new List<cSession>() { __InnerController.ClientSession }, true);
                        //WebGraph.ActionGraph.DoCheckLoginRequestAction.Action(__InnerController, new cDoCheckLoginRequestProps() { IsLogined = false });
                    }
                };

                WebGraph.SysActionGraph.ShowMessageAction.Action(_Controller, __MessageProps);
            }
            else
            {
                WebGraph.SysActionGraph.ReinitAction.Action(_Controller);
            }
        }

		public void ReceiveLoginData(cListenerEvent _ListenerEvent, IController _Controller, cLoginCommandData _ReceivedData)
		{

            if (!_Controller.ClientSession.IsLogined)
            {

                if (!String.IsNullOrEmpty(_ReceivedData.UserName) && !String.IsNullOrEmpty(_ReceivedData.Password))
                {
                    cSysDatabaseContext __DatabaseContext = DataService.GetDatabaseContext<cSysDatabaseContext>();

                    cUserEntity __UserEntity = UserDataManager.GetUserByEmailAndPassword(_ReceivedData.UserName, _ReceivedData.Password);

                    if (__UserEntity != null)
                    {
                        EUserState __UserState = EUserState.GetByID(__UserEntity.State, EUserState.Canceled);
                        if (EUserState.GetByID(__UserEntity.State, EUserState.Canceled).ID == EUserState.Confirmed.ID)
                        {

                            if (_ReceivedData.StaySession)
                            {
                                __DatabaseContext.Perform(() =>
                                {
                                    SessionDataManager.AddUserSession(__UserEntity, _Controller.ClientSession.SessionID, _Controller.ClientSession.IpAddress);

                                });
                            }

                            _Controller.ClientSession.SetUser(__UserEntity);

                            WebGraph.SysActionGraph.ReinitAction.Action(_Controller);
                            WebGraph.SysActionGraph.HotSpotMessageAction.Action(_Controller, new cHotSpotProps() { Header = _Controller.GetWordValue("Hi"), Message = _Controller.GetWordValue("Welcome", _Controller.ClientSession.User.Name), ColorType = EColorTypes.Success, DurationMS = 2500 });
                            WebGraph.SysActionGraph.SuccessResultAction.Action(_Controller);
                        }
                        else
                        {
                            WebGraph.SysActionGraph.ReinitAction.Action(_Controller);
                            WebGraph.SysActionGraph.ShowMessageAction.WarningAction(_Controller, new cMessageProps() { Header = _Controller.GetWordValue("Warning"), Message = _Controller.GetWordValue("AccountStateMessage", __UserEntity.Name, __UserState.Name) });
                        }
                    }
                    else
                    {
                        WebGraph.SysActionGraph.ShowMessageAction.ErrorAction(_Controller, new cMessageProps() { Header = _Controller.GetWordValue("Error"), Message = _Controller.GetWordValue("LoginError") });
                    }
                }
                else
                {
                    WebGraph.SysActionGraph.ReinitAction.Action(_Controller);
                    WebGraph.SysActionGraph.ShowMessageAction.ErrorAction(_Controller, new cMessageProps() { Header = _Controller.GetWordValue("Error"), Message = _Controller.GetWordValue("LoginError2") });
                }
            }
            else
            {
                WebGraph.SysActionGraph.ReinitAction.Action(_Controller);
            }

        }

		public void ReceiveCheckLoginData(cListenerEvent _ListenerEvent, IController _Controller, cCheckLoginCommandData _ReceivedData)
		{

            WebGraph.SysActionGraph.ResultItemAction.Action(_Controller, new nActionGraph.nActions.nResultItemAction.cResultItemProps()
            {
                Item = new { 
                    Name = "Obaaaaaaa"
                }
            });
        }

		

	}
}