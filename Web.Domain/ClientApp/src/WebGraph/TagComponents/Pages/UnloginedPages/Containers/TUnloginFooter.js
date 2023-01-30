import React, { Component } from "react";

import {  DebugAlert,  Class,  Interface,  Abstract,  ObjectTypes,  JSTypeOperator } from "../../../../GenericCoreGraph/ClassFramework/Class";
import TObject from "../../../TObject";
import Actions from "../../../../GenericWebController/ActionGraph/Actions";
import { CommandIDs } from "../../../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs";
import GenericWebGraph from "../../../../GenericWebController/GenericWebGraph";
import UnLoginStyles from "../../../../../ScriptStyles/UnLoginStyles";
import {withStyles} from "@mui/styles";


var TUnloginFooter = Class(
  TObject,
  {
      ObjectType: ObjectTypes.Get("TUnloginFooter"),
    constructor: function (_Props) {
        TUnloginFooter.BaseObject.constructor.call(this, _Props);
    },
    Destroy: function () {
        TUnloginFooter.BaseObject.Destroy.call(this);
    },
    render() {
      // eslint-disable-next-line
      const { children, ...attributes } = this.props;
      const { classes } = this.props;

      return (
        <div style={{ width: "100%" }}>
          footer
        </div>
      );
    },
  },
  {}
);


export default withStyles(UnLoginStyles)(TUnloginFooter);
