using Bootstrapper.Core.nApplication;
using Sys.Data.nDatabaseService;
using Sys.Web.Controllers;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nCheckLoginCommand;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nLoginCommand;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nLogoutCommand;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nFirstInitCommand;
using Bootstrapper.Core.nHandlers.nLanguageHandler;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nLanguageAction;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nShowMessageAction;
using Base.Data.nDatabaseService;
using Sys.Web.nWebGraph.nWebApiGraph.nListenerGraph.nLogInOutListener;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nPageResultAction;
using Sys.Web.nWebGraph.nWebApiGraph.nListenerGraph.nPermissionListener;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetMenuListCommand;
using Sys.Boundary.nDefaultValueTypes;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nMenuResultAction;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetPageListCommand;
using Sys.Web.nUtils.nValueTypes;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nHotSpotMessageAction;
using Sys.Web.nWebGraph.nSessionManager;
using Sys.Web.nWebGraph.nWebApiGraph.nListenerGraph.nParamListener;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSetGlobalParamListAction;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetGlobalParamListCommand;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nLogInOutAction;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nReinitCommand;

namespace Sys.Web.nWebGraph.nWebApiGraph.nListenerGraph.nFirstInitListener
{
     public class cFirstInitListener : cBaseListener
        , IFirstInitReceiver
        , IReinitReceiver
    {
        public cFirstInitListener(cApp _App, cBaseWebGraph _WebGraph, IDataService _DataService)
               : base(_App, _WebGraph, _DataService)
        {
        }

        public void ReceiveFirstInitData(cListenerEvent _ListenerEvent, IController _Controller, cFirstInitCommandData _ReceivedData)
        {
            WebGraph.SysActionGraph.CommandListAction.Action(_Controller);
            WebGraph.SysActionGraph.ActionListAction.Action(_Controller);

            if (string.IsNullOrEmpty(_ReceivedData.LanguageCode))
            {
                if (_Controller.ClientSession.IsLogined)
                {
                    _ReceivedData.LanguageCode = _Controller.ClientSession.Language;
                }
                else
                {
                    _ReceivedData.LanguageCode = App.Handlers.LanguageHandler.LanguageNameList[0].Code;
                }
            }
            cLanguageItem __LanguageItem = App.Handlers.LanguageHandler.GetLanguageByCode(_ReceivedData.LanguageCode);
            List<string> __DefinedLanguages = new List<string>();
            foreach (KeyValuePair<string, cLanguageItem> __LanguageItemDictionary in App.Handlers.LanguageHandler.LanguageList)
            {
                __DefinedLanguages.Add(__LanguageItemDictionary.Key);
            }

            if (_Controller.ClientSession.IsLogined)
            {
                cSysDatabaseContext __DatabaseContext = DataService.GetDatabaseContext<cSysDatabaseContext>();
                __DatabaseContext.Perform(() =>
                {
                    _Controller.ClientSession.User.Language = _ReceivedData.LanguageCode;
                    __DatabaseContext.SaveChanges();
                });
            }

            WebGraph.SysActionGraph.LanguageAction.Action(_Controller, new cLanguageProps() { Language = __LanguageItem.LanguageObject, LanguageCode = _ReceivedData.LanguageCode, DefinedLanguages = __DefinedLanguages });

            cMenuResultProps __MenuResultProps = WebGraph.ListenerGraph.GetListenerByType<cPermissionListener>().PrepareMenuResultProps(_Controller, new cGetMenuListCommandData() { MenuTypeCode = MenuTypes.LeftMenu.Code, RootMenuCode = null});
            WebGraph.SysActionGraph.MenuResultAction.Action(_Controller, __MenuResultProps);

            cPageResultProps __PageResultProps = WebGraph.ListenerGraph.GetListenerByType<cPermissionListener>().PreparePageResultProps(_Controller, new cGetPageListCommandData());
            WebGraph.SysActionGraph.PageResultAction.Action(_Controller, __PageResultProps);

            cSetGlobalParamListProps __GlobalParamListResultProps = WebGraph.ListenerGraph.GetListenerByType<cParamListener>().PrepareGetGlobalParamListProps(_Controller, new cGetGlobalParamListCommandData() );
            WebGraph.SysActionGraph.SetGlobalParamListAction.Action(_Controller, __GlobalParamListResultProps);

            WebGraph.SysActionGraph.SetUserOnClientAction.Action(_Controller);

        }

        public void ReceiveReinitData(cListenerEvent _ListenerEvent, IController _Controller, cReinitCommandData _ReceivedData)
        {
            if (string.IsNullOrEmpty(_ReceivedData.LanguageCode))
            {
                if (_Controller.ClientSession.IsLogined)
                {
                    _ReceivedData.LanguageCode = _Controller.ClientSession.Language;
                }
                else
                {
                    _ReceivedData.LanguageCode = App.Handlers.LanguageHandler.LanguageNameList[0].Code;
                }
            }
            cLanguageItem __LanguageItem = App.Handlers.LanguageHandler.GetLanguageByCode(_ReceivedData.LanguageCode);
            List<string> __DefinedLanguages = new List<string>();
            foreach (KeyValuePair<string, cLanguageItem> __LanguageItemDictionary in App.Handlers.LanguageHandler.LanguageList)
            {
                __DefinedLanguages.Add(__LanguageItemDictionary.Key);
            }

            if (_Controller.ClientSession.IsLogined)
            {
                cSysDatabaseContext __DatabaseContext = DataService.GetDatabaseContext<cSysDatabaseContext>();
                __DatabaseContext.Perform(() =>
                {
                    _Controller.ClientSession.User.Language = _ReceivedData.LanguageCode;
                    __DatabaseContext.SaveChanges();
                });
            }

            WebGraph.SysActionGraph.LanguageAction.Action(_Controller, new cLanguageProps() { Language = __LanguageItem.LanguageObject, LanguageCode = _ReceivedData.LanguageCode, DefinedLanguages = __DefinedLanguages });

            cMenuResultProps __MenuResultProps = WebGraph.ListenerGraph.GetListenerByType<cPermissionListener>().PrepareMenuResultProps(_Controller, new cGetMenuListCommandData() { MenuTypeCode = MenuTypes.LeftMenu.Code, RootMenuCode = null });
            WebGraph.SysActionGraph.MenuResultAction.Action(_Controller, __MenuResultProps);

            cPageResultProps __PageResultProps = WebGraph.ListenerGraph.GetListenerByType<cPermissionListener>().PreparePageResultProps(_Controller, new cGetPageListCommandData());
            WebGraph.SysActionGraph.PageResultAction.Action(_Controller, __PageResultProps);

            cSetGlobalParamListProps __GlobalParamListResultProps = WebGraph.ListenerGraph.GetListenerByType<cParamListener>().PrepareGetGlobalParamListProps(_Controller, new cGetGlobalParamListCommandData());
            WebGraph.SysActionGraph.SetGlobalParamListAction.Action(_Controller, __GlobalParamListResultProps);

            WebGraph.SysActionGraph.SetUserOnClientAction.Action(_Controller);
        }
    }
}
