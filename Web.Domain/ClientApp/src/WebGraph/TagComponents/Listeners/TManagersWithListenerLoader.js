import React, { Component, View } from "react";
import {
  DebugAlert,
  Class,
  Interface,
  Abstract,
  ObjectTypes,
  JSTypeOperator,
} from "../../GenericCoreGraph/ClassFramework/Class";
import TObject from "../../TagComponents/TObject";

import cManagersWithListener from "../../GenericWebController/ManagersWithListener/cManagersWithListener"
import GenericWebGraph from "../../GenericWebController/GenericWebGraph"


var TManagersWithListenerLoader = Class(
  TObject,
    {
        ObjectType: ObjectTypes.Get("TManagersWithListenerLoader"),
        constructor: function (_Props) {
            TManagersWithListenerLoader.BaseObject.constructor.call(this, _Props);
            GenericWebGraph.ManagersWithListener = new cManagersWithListener();
        },
        Destroy: function () {
            TManagersWithListenerLoader.BaseObject.Destroy.call(this);
        },
        componentDidMount: function () {
            TManagersWithListenerLoader.BaseObject.componentDidMount.call(this);
        },
        render: function () {
            return (<div></div>);
        }
        ,
    },
  {}
);

export default TManagersWithListenerLoader;
