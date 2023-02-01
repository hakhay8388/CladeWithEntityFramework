import React, { Component } from "react";

import {  DebugAlert,  Class,  Interface,  Abstract,  ObjectTypes,  JSTypeOperator } from "../../../../GenericCoreGraph/ClassFramework/Class";
import TObject from "../../../TObject";
import Actions from "../../../../GenericWebController/ActionGraph/Actions";
import { CommandIDs } from "../../../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs";
import GenericWebGraph from "../../../../GenericWebController/GenericWebGraph";
import AdminStyles from "../../../../../ScriptStyles/AdminStyles";
import {withStyles} from "@mui/styles";

import TBaseFooter from "../../TBaseFooter";


var TAdminFooter = Class(TBaseFooter,
  {
      ObjectType: ObjectTypes.Get("TAdminFooter"),
    constructor: function (_Props) {
        TAdminFooter.BaseObject.constructor.call(this, _Props);
    },
    Destroy: function () {
        TAdminFooter.BaseObject.Destroy.call(this);
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


export default withStyles(AdminStyles)(TAdminFooter);
