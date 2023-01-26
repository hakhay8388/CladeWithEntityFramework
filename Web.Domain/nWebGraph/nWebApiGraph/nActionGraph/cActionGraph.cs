using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nActionListAction;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nAsyncLoadAction;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nCacheItAction;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nCommandListAction;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nDataSourceRefreshAction;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nDebugAlertAction;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nDoCheckLoginRequestAction;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nDoReconnectSignalRequestAction;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nForceLogoutAction;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nForceUpdateAction;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nGoPageAction;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nHotSpotMessageAction;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nLanguageAction;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nLogInOutAction;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nModalOpenAction;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nNoPermissionAction;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nProgressStatusAction;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nResultListAction;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSetClientLanguageAction;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSetGlobalParamListAction;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSetServerDateTimeAction;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSetStateAction;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSetVariableAction;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nShowMessageAction;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nShowMessageAndRunAction;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSuccessResultAction;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nValidationResultAction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bootstrapper.Core.nCore;
using Bootstrapper.Core.nApplication;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nPageResultAction;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nMenuResultAction;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nResultItemAction;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph
{
    public class cActionGraph : cCoreObject
    {

        public List<IAction> ActionList;


        public cSuccessResultAction SuccessResultAction { get; set; }
        public cCacheItAction CacheItAction { get; set; }

        public cCommandListAction CommandListAction { get; set; }
        public cActionListAction ActionListAction { get; set; }
        public cLogInOutAction LogInOutAction { get; set; }
        public cForceLogoutAction ForceLogoutAction { get; set; }
        public cSetStateAction SetStateAction { get; set; }
        public cSetVariableAction SetVariableAction { get; set; }
        public cShowMessageAction ShowMessageAction { get; set; }
        public cShowMessageAndRunCommandAction ShowMessageAndRunCommandAction { get; set; }
        public cHotSpotMessageAction HotSpotMessageAction { get; set; }
        public cHotSpotMessageAndRunCommandAction HotSpotMessageAndRunCommandAction { get; set; }
        public cResultListAction ResultListAction { get; set; }

        public cSetGlobalParamListAction SetGlobalParamListAction { get; set; }

        public cGoPageAction GoPageAction { get; set; }
        public cModalOpenAction ModalOpenAction { get; set; }
        public cNoPermissionAction NoPermissionAction { get; set; }

        public cLanguageAction LanguageAction { get; set; }
        public cSetClientLanguageAction SetClientLanguageAction { get; set; }
        

        public cSetServerDateTimeAction SetServerDateTimeAction { get; set; }
        public cProgressStatusAction ProgressStatusAction { get; set; }

		public cDataSourceRefreshAction DataSourceRefreshAction { get; set; }

		public cAsyncLoadAction AsyncLoadAction { get; set; }
		public cForceUpdateAction ForceUpdateAction { get; set; }


		public cDoReconnectSignalRequestAction DoReconnectSignalRequestAction { get; set; }

		public cDebugAlertAction DebugAlertAction { get; set; }

		public cDoCheckLoginRequestAction DoCheckLoginRequestAction { get; set; }
		public cValidationResultAction ValidationResultAction { get; set; }

        public cPageResultAction PageResultAction { get; set; }

        public cMenuResultAction MenuResultAction { get; set; }

        public cResultItemAction ResultItemAction { get; set; }


        public cActionGraph(cApp _App, cWebGraph _WebGraph)
            : base(_App)
        {
            ActionList = new List<IAction>();
        }

        public override void Init()
        {
			Type __ThisType = this.GetType();
			List<Type> __Templates = App.Handlers.AssemblyHandler.GetTypesFromBaseInterface<IAction>();
			__Templates.ForEach(__Type =>
            {
                IAction __Action = (IAction)App.Factories.ObjectFactory.ResolveInstance(__Type);
                PropertyInfo __PropertyInfo = __ThisType.GetAllProperties().Where(__Item => __Item.Name.StartsWith(__Action.ActionID.Name + "Action")).FirstOrDefault();
                if (__PropertyInfo == null)
                {
                    ////// Oluşturulan her action bu class'ın içine tanımlanmalı
                    //////
                    throw new Exception($"{__Action.ActionID.Name} Action ismi ActionIDs ile eşleşmiyor.");
                }
                __PropertyInfo.GetSetMethod().Invoke(this, new object[] { __Action });

            });
		}
    }
}