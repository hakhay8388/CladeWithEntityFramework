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


var TPagesLoader = Class(TObject,
    {
        ObjectType: ObjectTypes.Get("TPagesLoader"),
        constructor: function (_Props)
        {
            TPagesLoader.BaseObject.constructor.call(this, _Props);
        },
        Destroy: function ()
        {
            TPagesLoader.BaseObject.Destroy.call(this);
        },
        componentDidMount: function ()
        {
            TPagesLoader.BaseObject.componentDidMount.call(this);
        },
        render: function ()
        {
            return (<div></div>);
        }
        ,
    },
  {}
);

export default TPagesLoader;
