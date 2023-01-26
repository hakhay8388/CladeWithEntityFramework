import React, { Component } from 'react';
import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator } from "../../GenericCoreGraph/ClassFramework/Class";
import cBaseObject from "../../GenericCoreGraph/BaseObject/cBaseObject";
import cLanguageManager from "./LanguageManager/cLanguageManager";


var cManagers = Class(cBaseObject,
  {
    ObjectType: ObjectTypes.Get("cManagers")
    ,
    constructor: function () {
      cManagers.BaseObject.constructor.call(this);
      this.InitManagers();
    }
    ,
    InitManagers: function () {
      this.LanguageManager = new cLanguageManager();
    }
    ,
    Destroy: function () {
      cManagers.BaseObject.Destroy.call(this);
    }
  }, {});

export default cManagers;







