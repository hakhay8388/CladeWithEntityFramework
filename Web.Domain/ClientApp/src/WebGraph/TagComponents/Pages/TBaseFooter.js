import React, { Component } from "react";

import {  DebugAlert,  Class,  Interface,  Abstract,  ObjectTypes,  JSTypeOperator } from "../../GenericCoreGraph/ClassFramework/Class";
import TObject from "../TObject";
import Actions from "../../GenericWebController/ActionGraph/Actions";
import { CommandIDs } from "../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs";
import GenericWebGraph from "../../GenericWebController/GenericWebGraph";
import AdminStyles from "../../../ScriptStyles/AdminStyles";
import {withStyles} from "@mui/styles";


var TBaseFooter = Class(
  TObject,
  {
      ObjectType: ObjectTypes.Get("TBaseFooter"),
    constructor: function (_Props) {
        TBaseFooter.BaseObject.constructor.call(this, _Props);
    },
    Destroy: function () {
        TBaseFooter.BaseObject.Destroy.call(this);
    },
    render() {
      // eslint-disable-next-line
      const { children, ...attributes } = this.props;
      const { classes } = this.props;

      return (
        <div style={{ width: "100%" }}>
          admin footer
        </div>
      );
    },
  },
  {}
);


export default TBaseFooter;
