import React, { Component } from 'react';
import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator, cListForBase } from "../../../GenericCoreGraph/ClassFramework/Class";
import cBaseObject from "../../../GenericCoreGraph/BaseObject/cBaseObject";
import Actions from "../../../GenericWebController/ActionGraph/Actions"
import cDelegate from "../../../GenericCoreGraph/Delegate/cDelegate"
import cBaseManagersWithListener from "../cBaseManagersWithListener";
import { CommandInterfaces } from "../../CommandInterpreter/cCommandInterpreter"
import { CommandIDs } from "../../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs"


var cNotificationManager = Class(cBaseManagersWithListener, CommandInterfaces.INotificationCommandReceiver,
  {
    ObjectType: ObjectTypes.Get("cNotificationManager")
    , TimerEvent: null
    ,
    constructor: function ()
    {
      cNotificationManager.BaseObject.constructor.call(this);
      this.ChannelList = new cListForBase();
      this.UnReadedChannelList = new cListForBase();
    }
    ,
    HandleLoadNotifications: function ()
    {
      var __This = this;
      Actions.GetNotifications(function (_Message)
      {
        CommandIDs.ResultListCommand.RunIfHas(_Message, function (_Data)
        {
          const __Readed = _Data.ResultList.filter((__Item) => {
            return !__Item.Readed && __Item.ChannelID == window.Enums.ENotificationChannels.GlobalChannel;
          });

          if (__Readed != null && __Readed.length > 0)
          {
            window.App.ContainerLayout.HandleOpenRightDrawer();
          }


          for (var i = 0; i < _Data.ResultList.length; i++)
          {
            var ___ChannelItem = __This.HandleGetNotificationChannel(_Data.ResultList[i].ChannelID);
            if (___ChannelItem)
            {
              ___ChannelItem.NotificationEvent.Run(_Data.ResultList[i]);
            }
          }
        });
      });
    }
    ,
    Receive_NotificationCommand: function (_Data)
    {
      if (window.App.Header && window.App.Header != null)
      {
        window.App.ContainerLayout.HandleOpenRightDrawer();
      }

      var ___ChannelItem = this.HandleGetNotificationChannel(_Data.ChannelID);
      if (___ChannelItem)
      {
        ___ChannelItem.NotificationEvent.Run(_Data);
      }
    }
    ,
    HandleGetNotificationChannel: function (_ChannelID)
    {
      return this.ChannelList.Find(__Item =>
      {
        return __Item.ChannelID == _ChannelID;
      });
    }
    ,
    HandleAddChannelNotificationEvent: function (_Sender, _ChannelID, _Function)
    {
      var ___ChannelItem = this.HandleGetNotificationChannel(_ChannelID);
      if (___ChannelItem)
      {
        ___ChannelItem.NotificationEvent.Add(_Sender, _Function);
      }
      else
      {
        var __NotificationEvent = new cDelegate(Object, false);
        __NotificationEvent.Add(_Sender, _Function);
        this.ChannelList.Add(
          {
            ChannelID: _ChannelID,
            NotificationEvent: __NotificationEvent
          });
      }
    }
    ,
    HandleSetUnReadedChannel : function (_UnReadedList) {
      this.UnReadedChannelList = _UnReadedList;
    }
    ,
    HandleGetUnReadedChannels: function () {
     return this.UnReadedChannelList;
    }
    ,
    HandleRemoveChannelNotificationEvent: function (_ChannelID, _Function)
    {
      var ___ChannelItem = this.HandleGetNotificationChannel(_ChannelID);
      if (___ChannelItem)
      {
        ___ChannelItem.NotificationEvent.Remove(_Function);
      }
    }
    ,
    Destroy: function ()
    {
      cNotificationManager.BaseObject.Destroy.call(this);
    }
  }, {});

export default cNotificationManager;







