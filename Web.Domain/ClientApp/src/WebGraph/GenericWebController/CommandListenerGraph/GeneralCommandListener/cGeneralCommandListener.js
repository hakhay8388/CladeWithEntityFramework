import React, { Component } from 'react';
import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator } from "../../../GenericCoreGraph/ClassFramework/Class"
import cBaseCommandListener from "../cBaseCommandListener"
import GenericWebGraph from "../../../GenericWebController/GenericWebGraph"
import { WebGraph } from "../../../GenericCoreGraph/WebGraph/WebGraph"
import {
  CommandInterfaces
} from "../../../GenericWebController/CommandInterpreter/cCommandInterpreter"
import Actions from "../../../GenericWebController/ActionGraph/Actions"
import { CommandIDs } from "../../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs"
import queryString from 'query-string';



var cGeneralCommandListener = Class(cBaseCommandListener
  , CommandInterfaces.ISetServerDateTimeCommandReceiver
  , CommandInterfaces.INoPermissionCommandReceiver
  , CommandInterfaces.IModalOpenCommandReceiver
  , CommandInterfaces.ISetClientLanguageCommandReceiver
  , CommandInterfaces.IDebugAlertCommandReceiver
  ,
  {
    ObjectType: ObjectTypes.Get("cGeneralCommandListener")
    ,
    constructor: function () {
      cGeneralCommandListener.BaseObject.constructor.call(this);
      this.Timer = null;
    }
    ,
    Destroy: function ()
    {
      cBaseCommandListener.prototype.Destroy.call(this);
    }
    ,
    Receive_DebugAlertCommand: function (_Data)
    {
      var __Header = _Data.Header;
      var __Message = _Data.Message;
      
      DebugAlert.Show("Header :" + __Header);
      DebugAlert.Show("Message :" + __Message);
    }
    ,
    Receive_SetClientLanguageCommand: function (_Data)
    {
      GenericWebGraph.Managers.LanguageManager.HandleSetActiveLanguage(_Data);
      var __List = WebGraph.GetInstancesByBaseClass(ObjectTypes.TObject);
      for (var i = 0; i < __List.length; i++) {
        __List[i].setState({
          Language: (GenericWebGraph.Managers && GenericWebGraph.Managers.LanguageManager && GenericWebGraph.Managers.LanguageManager.ActiveLanguage) ? GenericWebGraph.Managers.LanguageManager.ActiveLanguage : {}
        });
      }

      // GenericWebGraph.Managers.LanguageManager.SetLanguage("tr");
    },
    Receive_SetServerDateTimeCommand: function (_Data)
    {
      DebugAlert.Show("Server Date :" + _Data.ServerDate);

      /*var __Temp = new Date(_Data.ServerDate);
      let __HoursDiff = __Temp.getHours() - __Temp.getTimezoneOffset() / 60;
      let __MinutesDiff = (__Temp.getHours() - __Temp.getTimezoneOffset()) % 60;
      __Temp.setHours(__HoursDiff);
      __Temp.setMinutes(__MinutesDiff);

      window.App.ServerDate = __Temp;*/

      window.App.ServerDate = new Date(_Data.ServerDate);


      if (this.Timer != null) {
        clearInterval(this.Timer);
      }
      this.Timer = setInterval(this.HandleStartTimer, 1000);
    }
    ,
    HandleStartTimer: function () {
      if (window.App.ServerDate) {
        window.App.ServerDate.setSeconds(window.App.ServerDate.getSeconds() + 1);
      }
    }
    ,
    Receive_ModalOpenCommand: function (_Data) {
      if (window.App != null && window.App[_Data.ModalName] != null) {
        if (_Data.ParamObject != null) {
          _Data.ParamObject.ModalLock = _Data.ModalLock;
        }
        if (_Data.ParamObject == null) {
          _Data.ParamObject = {};
          _Data.ParamObject.ModalLock = _Data.ModalLock;
        }
        window.App[_Data.ModalName].HandleClickOpen(_Data.ParamObject, window.App[_Data.ModalName].HandleOnCloseDefaultFunction);
      }

    }
    ,
    Receive_NoPermissionCommand: function (_Data) {
      window.App.MessageBox.ShowMessage(this.state.Language.Error, this.state.Language.NoPermission, "danger", true);
    }
  }, {});

export default cGeneralCommandListener







