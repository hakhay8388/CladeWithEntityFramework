import React, { Component } from "react";
import
{
  DebugAlert,
  Class,
  Interface,
  Abstract,
  ObjectTypes,
  JSTypeOperator,
} from "../../../GenericCoreGraph/ClassFramework/Class";
import cBaseCommandListener from "../cBaseCommandListener";
import GenericWebGraph from "../../../GenericWebController/GenericWebGraph";
import { WebGraph } from "../../../GenericCoreGraph/WebGraph/WebGraph";
import { CommandInterfaces } from "../../../GenericWebController/CommandInterpreter/cCommandInterpreter";
import Actions from "../../../GenericWebController/ActionGraph/Actions";
import { CommandIDs } from "../../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs";
import queryString from "query-string";

var cLogInOutCommandListener = Class(
  cBaseCommandListener,
  CommandInterfaces.IReinitCommandReceiver,
  CommandInterfaces.ISetUserOnClientCommandReceiver,
  CommandInterfaces.IForceLogoutCommandReceiver,
  {
    ObjectType: ObjectTypes.Get("cLogInOutCommandListener"),
    constructor: function ()
    {
      cLogInOutCommandListener.BaseObject.constructor.call(this);
    },
    Destroy: function ()
    {
      cBaseCommandListener.prototype.Destroy.call(this);
      }
      ,
    Receive_SetUserOnClientCommand: function (_Data)
    {
        window.App.User = _Data.User;
        if (window.App.User != null)
        {
            GenericWebGraph.ManagersWithListener.SignalListerner.HandleConnect();   
        }
    }
    ,
    Receive_ReinitCommand: function (_Data)
    {        
        if (_Data.LoginState && window.App.User == null) {
            GenericWebGraph.CloseAllModals();
            DebugAlert.Show("Logined");
            GenericWebGraph.ManagersWithListener.SignalListerner.HandleConnect();

            window.App.SessionID = _Data.SessionID;
            GenericWebGraph.Reinit();
        }
        else if (_Data.LoginState) {
            GenericWebGraph.CloseAllModals();
            DebugAlert.Show("Logined");
            GenericWebGraph.ManagersWithListener.SignalListerner.HandleConnect();

            window.App.SessionID = _Data.SessionID;
            GenericWebGraph.Reinit();
        }
        else {
            GenericWebGraph.CloseAllModals();
            DebugAlert.Show("Unlogined");
            GenericWebGraph.ManagersWithListener.SignalListerner.HandleDisconnect();

            window.App.User = null;
            window.App.SessionID = null;
            GenericWebGraph.MainPage = null;
            GenericWebGraph.Reinit();
        }
    },
    Receive_ForceLogoutCommand: function (_Data)
    {
    },
  },
  {}
);

export default cLogInOutCommandListener;
