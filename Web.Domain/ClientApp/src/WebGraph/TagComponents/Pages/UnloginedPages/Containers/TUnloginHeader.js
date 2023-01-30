import React from "react";
import {  Class,  JSTypeOperator,  ObjectTypes } from "../../../../GenericCoreGraph/ClassFramework/Class";
import TObject from "../../../TObject";
import GenericWebGraph from "../../../../GenericWebController/GenericWebGraph";
import Actions from "../../../../GenericWebController/ActionGraph/Actions";
import { CommandIDs } from "../../../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs";
import { withStyles } from "@mui/styles";

import UnLoginStyles from "../../../../../ScriptStyles/UnLoginStyles";

var TUnloginHeader = Class(
  TObject,
  {
      ObjectType: ObjectTypes.Get("TUnloginHeader"),
    constructor: function (_Props) {
        TUnloginHeader.BaseObject.constructor.call(this, _Props);
      this.state = {
        ...this.state        
      };
      window.App.UnloginHeader = this;
    },
    Destroy: function () {
        TUnloginHeader.BaseObject.Destroy.call(this);
    },
    AsyncLoad: function () {
        TUnloginHeader.BaseObject.AsyncLoad.call(this);
    },
    render() 
    {
      var __This = this;
      const { children, classes, ...attributes } = this.props;
      return (
        <div style={{ width: "100%" }}>
          Header
        </div>
      );
    },
  },
  {}
);

export default withStyles(UnLoginStyles)(TUnloginHeader);
