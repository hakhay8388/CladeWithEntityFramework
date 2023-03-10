import React, { Component } from "react";

import {  DebugAlert,  Class,  Interface,  Abstract,  ObjectTypes,  JSTypeOperator } from "../../../../GenericCoreGraph/ClassFramework/Class";
import TObject from "../../../TObject";
import Actions from "../../../../GenericWebController/ActionGraph/Actions";
import { CommandIDs } from "../../../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs";
import GenericWebGraph from "../../../../GenericWebController/GenericWebGraph";
import UserStyles from "../../../../../ScriptStyles/UserStyles";
import {withStyles} from "@mui/styles";

import TBaseFooter from "../../TBaseFooter";


var TUserFooter = Class(TBaseFooter,
  {
      ObjectType: ObjectTypes.Get("TUserFooter"),
    constructor: function (_Props) {
        TUserFooter.BaseObject.constructor.call(this, _Props);
    },
    Destroy: function () {
        TUserFooter.BaseObject.Destroy.call(this);
    },
    render() {
      // eslint-disable-next-line
      const { children, ...attributes } = this.props;
      const { classes } = this.props;

      return (
        <div style={{ width: "100%" }}>
          User footer
        </div>
      );
    },
  },
  {}
);


export default withStyles(UserStyles)(TUserFooter);
