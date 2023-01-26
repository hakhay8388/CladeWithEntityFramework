using Base.Data.nConfiguration;
using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using Web.Domain.Controllers;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nDebugAlertAction;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nShowMessageAction;
using Data.Domain.nDatabaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.nWebGraph;

namespace Web.Domain.nWebGraph.nErrorMessageManager
{
    public class cErrorMessageManager : cCoreObject
    {
		public cWebGraph WebGraph { get; set; }
		public cDataService DataService { get; set; }
		public cErrorMessageManager(cApp _App, cWebGraph _WebGraph, cDataService _DataService)
           : base(_App)
        {
			WebGraph = _WebGraph;
            DataService = _DataService;
		}

		public void ErrorAction(Exception _Ex, IController _Controller, string _Header, string _Message)
		{
			/*cGenericWebScaffoldDataService __DataService = (cGenericWebScaffoldDataService)DataServiceManager.GetDataService();

			if (__DataService.BackendDebugMessageShowToUser)
			{
				WebGraph.ActionGraph.ShowMessageAction.ErrorAction(_Ex, _Controller,
					   new cMessageProps()
					   {
						   Header = _Header,
						   Message = _Message
					   });
			}
			else
			{
				WebGraph.ActionGraph.DebugAlertAction.ErrorAction(_Ex, _Controller,
				   new cDebugAlertProps()
				   {
					   Header = _Header,
					   Message = _Message
				   });
			}*/
		}

		public void ErrorAction(IController _Controller, string _Header, string _Message)
		{
			/*cGenericWebScaffoldDataService __DataService = (cGenericWebScaffoldDataService)DataServiceManager.GetDataService();

			if (__DataService.BackendDebugMessageShowToUser)
			{
				WebGraph.ActionGraph.ShowMessageAction.ErrorAction(_Controller,
					   new cMessageProps()
					   {
						   Header = _Header,
						   Message = _Message
					   });
			}
			else
			{
				WebGraph.ActionGraph.DebugAlertAction.ErrorAction(_Controller,
				   new cDebugAlertProps()
				   {
					   Header = _Header,
					   Message = _Message
				   });
			}*/
		}
	}
}

