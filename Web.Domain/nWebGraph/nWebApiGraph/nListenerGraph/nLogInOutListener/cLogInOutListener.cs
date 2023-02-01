using Web.Domain.Controllers;
using Web.Domain.nUtils.nValueTypes;
using Web.Domain.nWebGraph.nSessionManager;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nDoCheckLoginRequestAction;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nHotSpotMessageAction;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nShowMessageAction;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nCheckLoginCommand;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nLoginCommand;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nLogoutCommand;
using Data.Domain.nDefaultValueTypes;
using System;
using System.Collections.Generic;
using Bootstrapper.Core.nApplication;
using Data.Domain.nDatabaseService;
using Data.Boundary.nData;
using Data.Domain.nDatabaseService.nSystemEntities;
using Data.Domain.nDataService.nDataManagers;

namespace Web.Domain.nWebGraph.nWebApiGraph.nListenerGraph.nLogInOutListener
{
    public class cLogInOutListener : cBaseListener
		, ILoginReceiver
		, ILogoutReceiver
		, ICheckLoginReceiver
	{
        public cUserDataManager UserDataManager { get; set; }
        public cSessionDataManager SessionDataManager { get; set; }


        public cLogInOutListener(cApp _App, cWebGraph _WebGraph, cDataService _DataService
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

                        string __Session_ID = __InnerController.ClientSession.SessionID;
                        long _UserID = __InnerController.ClientSession.User.ID;

                        cDatabaseContext __DatabaseContext = DataService.GetDatabaseContext();

                        __DatabaseContext.Perform(() =>
                        {
                            SessionDataManager.DeleteSession(__Session_ID);
                            __DatabaseContext.SaveChanges();
                        });

                        __InnerController.Logout();

                        //WebGraph.ActionGraph.SuccessResultAction.Action(_Controller);
                        WebGraph.ActionGraph.LogInOutAction.Action(__InnerController, new List<cSession>() { __InnerController.ClientSession }, true);

                        //WebGraph.ActionGraph.DoCheckLoginRequestAction.Action(__InnerController, new cDoCheckLoginRequestProps() { IsLogined = false }, new List<cSession>() { __InnerController.ClientSession }, true);
                        //WebGraph.ActionGraph.DoCheckLoginRequestAction.Action(__InnerController, new cDoCheckLoginRequestProps() { IsLogined = false });
                    }
                };

                WebGraph.ActionGraph.ShowMessageAction.Action(_Controller, __MessageProps);
            }
            else
            {
                WebGraph.ActionGraph.LogInOutAction.Action(_Controller);
            }
        }

		public void ReceiveLoginData(cListenerEvent _ListenerEvent, IController _Controller, cLoginCommandData _ReceivedData)
		{

            if (!_Controller.ClientSession.IsLogined)
            {

                if (!String.IsNullOrEmpty(_ReceivedData.UserName) && !String.IsNullOrEmpty(_ReceivedData.Password))
                {
                    cDatabaseContext __DatabaseContext = DataService.GetDatabaseContext();

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

                            WebGraph.ActionGraph.LogInOutAction.Action(_Controller);
                            WebGraph.ActionGraph.HotSpotMessageAction.Action(_Controller, new cHotSpotProps() { Header = _Controller.GetWordValue("Hi"), Message = _Controller.GetWordValue("Welcome", _Controller.ClientSession.User.Name), ColorType = EColorTypes.Success, DurationMS = 2500 });
                            WebGraph.ActionGraph.SuccessResultAction.Action(_Controller);
                        }
                        else
                        {
                            WebGraph.ActionGraph.LogInOutAction.Action(_Controller);
                            WebGraph.ActionGraph.ShowMessageAction.WarningAction(_Controller, new cMessageProps() { Header = _Controller.GetWordValue("Warning"), Message = _Controller.GetWordValue("AccountStateMessage", __UserEntity.Name, __UserState.Name) });
                        }
                    }
                    else
                    {
                        WebGraph.ActionGraph.ShowMessageAction.ErrorAction(_Controller, new cMessageProps() { Header = _Controller.GetWordValue("Error"), Message = _Controller.GetWordValue("LoginError") });
                    }
                }
                else
                {
                    WebGraph.ActionGraph.LogInOutAction.Action(_Controller);
                    WebGraph.ActionGraph.ShowMessageAction.ErrorAction(_Controller, new cMessageProps() { Header = _Controller.GetWordValue("Error"), Message = _Controller.GetWordValue("LoginError2") });
                }
            }
            else
            {
                WebGraph.ActionGraph.LogInOutAction.Action(_Controller);
            }

        }

		public void ReceiveCheckLoginData(cListenerEvent _ListenerEvent, IController _Controller, cCheckLoginCommandData _ReceivedData)
		{
            
            List<cSession> __Sessions = WebGraph.SessionManager(_Controller).GetSessionByUserID(_Controller.ClientSession.User.ID);


            cMessageProps __MessageProps = new cMessageProps();
            __MessageProps.CloseRequired = false;
            __MessageProps.Header = _Controller.GetWordValue("Error");
            __MessageProps.Message = _Controller.GetWordValue("NoPermission");


            WebGraph.ActionGraph.ShowMessageAction.Action(_Controller, __MessageProps, __Sessions, true);

            WebGraph.ActionGraph.HotSpotMessageAction.Action(_Controller, new cHotSpotProps() { ColorType = EColorTypes.None, Header = "aaa", Message = "bbb", DurationMS = 5000, WaitTime = 1000 });
        }

		

	}
}