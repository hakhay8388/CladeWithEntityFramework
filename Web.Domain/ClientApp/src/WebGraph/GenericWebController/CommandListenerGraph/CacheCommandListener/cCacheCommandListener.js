import React, { Component } from 'react';
import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator, cListForBase} from "../../../GenericCoreGraph/ClassFramework/Class"
import cBaseCommandListener from "../cBaseCommandListener"
import GenericWebGraph from "../../../GenericWebController/GenericWebGraph"
import { WebGraph } from "../../../GenericCoreGraph/WebGraph/WebGraph"
import
{
  CommandInterfaces
} from "../../../GenericWebController/CommandInterpreter/cCommandInterpreter"
import Actions from "../../../GenericWebController/ActionGraph/Actions"
import { CommandIDs } from "../../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs"
import queryString from 'query-string';
import objectHash from 'object-hash';




var cCacheCommandListener = Class(cBaseCommandListener
  , CommandInterfaces.ICacheItCommandReceiver
  ,
  {
    ObjectType: ObjectTypes.Get("cCacheCommandListener")
    ,
    constructor: function ()
    {
      cCacheCommandListener.BaseObject.constructor.call(this);
      this.CacheList = new cListForBase();
    }
    ,
    HandleCacheControl: function (_ActionProps)
    {
      var __CheckSum = objectHash(_ActionProps.RequestAction)
      var __Result = this.CacheList.Find(__Item =>
      {
        if (__Item.CheckSum == __CheckSum)
        {
          return true;
        }
        return false;
      });

      return __Result ? __Result.CacheActionList : null;
    }
    ,
    Destroy: function ()
    {
      cBaseCommandListener.prototype.Destroy.call(this);
    }
    ,
    Receive_CacheItCommand: function (_Data)
    {
      var __CheckSum = objectHash(_Data.ActionProps.RequestAction)

      this.CacheList.Add({
        CheckSum : __CheckSum,
        CacheActionList : _Data.CacheActionList
      });


      _Data.CacheActionList.ActionProps = _Data.ActionProps;
      
      try
      {
        if (_Data.ActionProps.ResultFunction && JSTypeOperator.IsFunction(_Data.ActionProps.ResultFunction))
        {
          var __ResultValue = _Data.ActionProps.ResultFunction(_Data.CacheActionList);
          if (!JSTypeOperator.IsDefined(__ResultValue) || __ResultValue)
          {
            try
            {
              GenericWebGraph.CommandInterpreter.InterpretCommand(_Data.CacheActionList);
            }
            catch (_Ex)
            {
              DebugAlert.Show("CommandInterpreter'a data gelmedi..!", _Ex);
            }
          }
        }
        else
        {
          try
          {
            GenericWebGraph.CommandInterpreter.InterpretCommand(_Data.CacheActionList);
          }
          catch (_Ex)
          {
            DebugAlert.Show("CommandInterpreter'a data gelmedi..!", _Ex);
          }
        }
      }
      catch (_Ex)
      {
        DebugAlert.Show("Action sonrası çalışacak fonksiyonda sorun var!", _Ex);
      }

    }
  }, {});

export default cCacheCommandListener







