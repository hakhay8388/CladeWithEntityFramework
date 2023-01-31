import React, { Component, Suspense } from "react";
import GenericWebGraph from "../../../GenericWebController/GenericWebGraph";
import { CommandInterfaces } from "../../../GenericWebController/CommandInterpreter/cCommandInterpreter";
import {
  DebugAlert,
  Class,
  Interface,
  Abstract,
  ObjectTypes,
  JSTypeOperator,
} from "../../../GenericCoreGraph/ClassFramework/Class";
import TObject from "../../TObject";
import Actions from "../../../GenericWebController/ActionGraph/Actions";
import { CommandIDs } from "../../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs";


import { withStyles } from "@mui/styles";
import GlobalStyles from "../../../../ScriptStyles/GlobalStyles";
import classNames from "classnames";

var TUserMainPage = Class(TObject,
  {
      ObjectType: ObjectTypes.Get("TUserMainPage"),
    constructor: function (_Props) {
        TUserMainPage.BaseObject.constructor.call(this, _Props);
      this.state = {
        ...this.state,
      };
    },
    AsyncLoad: function () {
      var __This = this;
    },
      Destroy: function () {
          TUserMainPage.BaseObject.Destroy.call(this);
      }
    ,
    render() {
      const { classes } = this.props;

      return (
          <div>
              User List Main
        </div>
      )
    },
  },
  {}
);

export default withStyles(GlobalStyles)(TUserMainPage);
