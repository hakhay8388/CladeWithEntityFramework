using Base.Data.nConfiguration;
using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using Sys.Web.Controllers;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nDebugAlertAction;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nShowMessageAction;
using Sys.Data.nDatabaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sys.Web.nWebGraph;

using Base.Data.nDatabaseService;

namespace Sys.Web.nWebGraph.nErrorMessageManager
{
    public class cErrorMessageManager : cCoreObject
    {
		public cBaseWebGraph WebGraph { get; set; }
		public IDataService DataService { get; set; }
		public cErrorMessageManager(cApp _App, cBaseWebGraph _WebGraph, IDataService _DataService)
           : base(_App)
        {
			WebGraph = _WebGraph;
            DataService = _DataService;
		}

		public void ErrorAction(Exception _Ex, IController _Controller, string _Header, string _Message)
		{

			if (Params.GlobalParams.BackendDebugMessageShowToUser)
			{
				WebGraph.SysActionGraph.ShowMessageAction.ErrorAction(_Ex, _Controller,
					   new cMessageProps()
					   {
						   Header = _Header,
						   Message = _Message
					   });
			}
			else
			{
				WebGraph.SysActionGraph.DebugAlertAction.ErrorAction(_Ex, _Controller,
				   new cDebugAlertProps()
				   {
					   Header = _Header,
					   Message = _Message
				   });
			}
		}

		public void ErrorAction(IController _Controller, string _Header, string _Message)
		{
			if (Params.GlobalParams.BackendDebugMessageShowToUser)
			{
				WebGraph.SysActionGraph.ShowMessageAction.ErrorAction(_Controller,
					   new cMessageProps()
					   {
						   Header = _Header,
						   Message = _Message
					   });
			}
			else
			{
				WebGraph.SysActionGraph.DebugAlertAction.ErrorAction(_Controller,
				   new cDebugAlertProps()
				   {
					   Header = _Header,
					   Message = _Message
				   });
			}
		}
	}
}

