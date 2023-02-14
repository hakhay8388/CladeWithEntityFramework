import React from "react";
import { Class, JSTypeOperator, ObjectTypes } from "../../../../GenericCoreGraph/ClassFramework/Class";
import TObject from "../../../TObject";
import GenericWebGraph from "../../../../GenericWebController/GenericWebGraph";
import Actions from "../../../../GenericWebController/ActionGraph/Actions";
import { CommandIDs } from "../../../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs";
import { withStyles } from "@mui/styles";

import AdminStyles from "../../../../../ScriptStyles/AdminStyles";
import UserStyles from "../../../../../ScriptStyles/UserStyles";
import TBaseHeader from "../../TBaseHeader";
import {
    ListItem, ListItemButton, ListItemIcon, ListItemText,
    Grid, Typography, Accordion, AccordionDetails, Breadcrumbs, Drawer, Link, AccordionSummary
} from "@mui/material";


var TUserHeader = Class(TBaseHeader,
  {
      ObjectType: ObjectTypes.Get("TUserHeader"),
    constructor: function (_Props) {
        TUserHeader.BaseObject.constructor.call(this, _Props);
      this.state = {
          ...this.state,
        };
        window.App.Header = this;
    },
    Destroy: function () {
        TUserHeader.BaseObject.Destroy.call(this);
    },
    AsyncLoad: function () {
        TUserHeader.BaseObject.AsyncLoad.call(this);
    },

    render() 
    {
      var __This = this;
      const { children, classes, ...attributes } = this.props;
        return this.HandleWrapWithApp(
          <div>
              Admin Header
          </div>
      );
    },
  },
  {}
);

export default withStyles(UserStyles)(TUserHeader);
