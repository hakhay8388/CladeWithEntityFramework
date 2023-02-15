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

import { Grid, Button, CircularProgress } from "@mui/material";

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
      HandleLogout: function (_Event)
      {
          Actions.Logout();
      }
    ,
    render() {
      const { classes } = this.props;

      return (
          <Grid container>
              <Grid item>
                  <Button
                      fullWidth
                      variant="contained"
                      color="secondary"
                      onClick={this.HandleLogout}
                      block="true"
                  >
                      {this.state.Language.Logout}
                  </Button>
              </Grid>
          </Grid>
      )
    },
  },
  {}
);

export default withStyles(GlobalStyles)(TUserMainPage);
