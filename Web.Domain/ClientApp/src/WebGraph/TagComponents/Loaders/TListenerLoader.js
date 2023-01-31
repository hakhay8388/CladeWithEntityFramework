import React, { Component, View } from "react";
import {
  DebugAlert,
  Class,
  Interface,
  Abstract,
  ObjectTypes,
  JSTypeOperator,
} from "../../GenericCoreGraph/ClassFramework/Class";
import TObject from "../TObject";

import cManagersWithListener from "../../GenericWebController/ManagersWithListener/cManagersWithListener"
import GenericWebGraph from "../../GenericWebController/GenericWebGraph"
import cCommandListenerGraph from "../../GenericWebController/CommandListenerGraph/cCommandListenerGraph"


var TListenerLoader = Class(TObject,
    {
        ObjectType: ObjectTypes.Get("TListenerLoader"),
        constructor: function (_Props) {
            TListenerLoader.BaseObject.constructor.call(this, _Props);
            GenericWebGraph.ManagersWithListener = new cManagersWithListener();
            GenericWebGraph.CommandListenerGraph = new cCommandListenerGraph();
            this.InitCommand();
        },
        InitCommand: function () {
            GenericWebGraph.CommandInterpreter.InterpretCommand(this.props.RunAfterLoad);
        }
        ,
        Destroy: function () {
            TListenerLoader.BaseObject.Destroy.call(this);
        },
        componentDidMount: function () {
            TListenerLoader.BaseObject.componentDidMount.call(this);
        },
        render: function () {
            return (<div></div>);
        }
        ,
    },
  {}
);

export default TListenerLoader;
