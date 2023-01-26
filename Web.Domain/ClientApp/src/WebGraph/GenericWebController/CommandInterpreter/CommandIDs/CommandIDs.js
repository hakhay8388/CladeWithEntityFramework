import React, { Component } from "react";
import {
  JSTypeOperator,
  DebugAlert,
  Class,
  Interface,
  Abstract,
  ObjectTypes,
  cListForBase,
} from "../../../GenericCoreGraph/ClassFramework/Class";
import cBaseObject from "../../../GenericCoreGraph/BaseObject/cBaseObject";
import cCommandID from "./cCommandID";
//import $ from "jquery";

var CommandIDs_Class = Class(
  Component,
  {
    CommandIDList: null,
    constructor: function () {
      CommandIDs_Class.BaseObject.constructor.call(this);
      this.CommandIDList = new cListForBase();
      
        },
      LoadCommands: function (_CommandList)
      {
          var ECommandID = cCommandID;
          for (var i = 0; i < _CommandList.length; i++) {
              var __Eval =
                  "this." +
                  _CommandList[i].Name +
                  "Command = new ECommandID(" +
                  _CommandList[i].ID.toString() +
                  ', "' +
                  _CommandList[i].Name +
                  '", "' +
                  _CommandList[i].Info +
                  '", ' +
                  _CommandList[i].Enabled.toString() +
                  ")";
              eval(__Eval);
              __Eval =
                  "this.CommandIDList.Add(this." + _CommandList[i].Name + "Command)";
              eval(__Eval);
          }
      }
    ,
    Destroy: function () {
      this.CommandIDList.DestroyWithItems();
      CommandIDs_Class.BaseObject.Destroy.call(this);
    },
  },
  {}
);

export const CommandIDs = new CommandIDs_Class();
