﻿using Newtonsoft.Json.Linq;
using Sys.Web.nWebGraph.nSessionManager;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootstrapper.Core.nApplication;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nHotSpotMessageAction;

using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nShowMessageAction;
using Sys.Web.Controllers;
using Sys.Boundary.nDefaultValueTypes;
using Microsoft.AspNetCore.Mvc;

namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nNoPermissionAction
{
    public class cNoPermissionAction : cBaseAction
    {

        public cNoPermissionAction(cApp _App, cBaseWebGraph _WebGraph)
           : base(_App, _WebGraph, EActionType.NoPermission)
        {
        }

        public override void Action(IController _Controller, List<cSession> _SignalSessions = null, bool _InstantSend = false)
        {
            cMessageProps __MessageProps = new cMessageProps();
            __MessageProps.CloseRequired = false;
            __MessageProps.Header = _Controller.GetWordValue("Error");
            __MessageProps.Message = _Controller.GetWordValue("NoPermission");
            int __CID = (int)_Controller.CommandJson["CID"];
            App.Loggers.CoreLogger.LogError(new Exception($"Yetkisiz işlem.Command ID {__CID} User Name/IP {(_Controller.ClientSession.IsLogined ? _Controller.ClientSession.User.Name + " " + _Controller.ClientSession.User.Surname :"No Logined" + "/" + _Controller.ClientSession.IpAddress) }"));
            WebGraph.SysActionGraph.ShowMessageAction.ErrorAction(new Exception("Yetkisiz işlem"), _Controller, __MessageProps, _SignalSessions, _InstantSend);
        }

    }
}
