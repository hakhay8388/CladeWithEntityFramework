import React, { Component } from 'react';
import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator } from "../../GenericCoreGraph/ClassFramework/Class";
import cBaseObject from "../../GenericCoreGraph/BaseObject/cBaseObject";
import cGlobalParamsManager from "./GlobalParamsManager/cGlobalParamsManager";
import cSignalListerner from "./SignalListerner/cSignalListerner";



var cManagersWithListener = Class(cBaseObject,
  {
    ObjectType: ObjectTypes.Get("cManagersWithListener")
    ,
    constructor: function ()
    {
      cManagersWithListener.BaseObject.constructor.call(this);
      this.InitManagers();
    }
    ,
    InitManagers: function ()
    {
      this.GlobalParamsManager = new cGlobalParamsManager();
      this.SignalListerner = new cSignalListerner();
    }
    ,
    Destroy: function ()
    {
      cManagersWithListener.BaseObject.Destroy.call(this);
    }
  }, {});

export default cManagersWithListener;







