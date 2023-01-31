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


var TUnloginedMainPage = Class(TObject,
  {
      ObjectType: ObjectTypes.Get("TUnloginedMainPage"),
    constructor: function (_Props) {
        TUnloginedMainPage.BaseObject.constructor.call(this, _Props);
        this.state = {
            ...this.state,
            ButtonEnabled: true,
            UserName: "admin@admin.com",
            Password: "1",
            StaySession: true
        };
    },
    AsyncLoad: function () {
      var __This = this;
    },
      Destroy: function () {
          TUnloginedMainPage.BaseObject.Destroy.call(this);
      }
      ,
      HandleSubmit(_Event) {
          _Event.preventDefault();
          var __This = this;
          this.setState(
              {
                  ButtonEnabled: false,
              },
              () => {
                  Actions.Login(
                      this.state.UserName,
                      this.state.Password,
                      this.state.StaySession,
                      function (_Message) {
                          CommandIDs.SuccessResultCommand.RunIfNotHas(
                              _Message,
                              function (_Data) {
                                  __This.setState({
                                      ButtonEnabled: true,
                                  });
                              }
                          );

                          CommandIDs.SuccessResultCommand.RunIfHas(
                              _Message,
                              function (_Data) {
                                  __This.setState({
                                      ButtonEnabled: false,
                                  });
                              }
                          );
                      }
                  );
              }
          );
      },
      render() {
          const { classes } = this.props;
          var __This = this;
          return (
              <Grid container>
                  <Button
                      fullWidth
                      variant="contained"
                      color="secondary"
                      disabled={!this.state.ButtonEnabled}
                      onClick={this.HandleSubmit}
                      block="true"
                  >
                      {!this.state.ButtonEnabled ? (
                          <CircularProgress
                              style={{ width: "21px", height: "21px" }}
                              color={"primary"}
                          />
                      ) : this.state.Language.Login
                      }
                  </Button>                 
              </Grid>
          );
      },
  },
  {}
);

export default withStyles(GlobalStyles)(TUnloginedMainPage);
