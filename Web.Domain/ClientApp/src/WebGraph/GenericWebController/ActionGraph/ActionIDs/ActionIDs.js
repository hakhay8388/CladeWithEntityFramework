import React, { Component } from 'react';
import { JSTypeOperator, DebugAlert, Class, Interface, Abstract, ObjectTypes, cListForBase } from "../../../GenericCoreGraph/ClassFramework/Class"
import cActionID from "./cActionID"
//import $ from 'jquery';

var ActionIDs_Class = Class(Component,
  {
    ActionIDList: null,
    constructor: function ()
    {
      ActionIDs_Class.BaseObject.constructor.call(this);
      this.ActionIDList = new cListForBase();
      var __ActionList = null;
      }
        ,
      LoadActions: function (_ActionList) {


          for (var i = 0; i < this.ActionIDList.Count(); i++)
          {
              var __Item = this.ActionIDList.GetItem(i);
              delete this[__Item.Name + "Action"];
          }

          this.ActionIDList.Clear();

          var EActionID = cActionID;
          for (var i = 0; i < _ActionList.length; i++) {
              var __Parameters = "[";

              for (var j = 0; j < _ActionList[i].Parameters.length; j++) {
                  if (j > 0) __Parameters += ", ";
                  __Parameters += "\"" + _ActionList[i].Parameters[j] + "\"";
              }
              __Parameters += "]";

              var __Eval = "this." + _ActionList[i].Name + "Action = new EActionID(" + _ActionList[i].ID.toString() + ", \"" + _ActionList[i].Name + "\", \"" + _ActionList[i].Info + "\", " + _ActionList[i].Enabled.toString() + ", " + __Parameters + ")";
              eval(__Eval)
              __Eval = "this.ActionIDList.Add(this." + _ActionList[i].Name + "Action)";
              eval(__Eval)
          }
      }
    ,
    Destroy: function () {
      this.ActionIDList.DestroyWithItems();
      ActionIDs_Class.BaseObject.Destroy.call(this);
    }
  }, {});

export const ActionIDs = new ActionIDs_Class();

