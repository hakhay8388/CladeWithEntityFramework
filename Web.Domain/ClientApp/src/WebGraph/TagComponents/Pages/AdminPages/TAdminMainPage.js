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

var TAdminMainPage = Class(TObject,
    {
        ObjectType: ObjectTypes.Get("TAdminMainPage"),
        constructor: function (_Props) {
            TAdminMainPage.BaseObject.constructor.call(this, _Props);
            this.state = {
                ...this.state,
                ButtonEnabled: true,
                ButtonName: "Admin Test"
            };
        },
        AsyncLoad: function (_Event) {
            var __This = this;
            __This.HandleGetName();
        },
        Destroy: function () {
            TAdminMainPage.BaseObject.Destroy.call(this);
        }
        ,
        HandleRedirect: function (_Event)
        {
            window.GoPage(window.Pages.UserList.Path);
        }
        ,
        HandleLogout: function (_Event) {
            Actions.Logout();
        }
        ,
        HandleSubmit: function () {
            var __This = this;
            window.App.ContainerLayout.HandleToggleLeftDrawer();
            /*
            alert("test");
            Actions.Test(11, "test", function (_Message) {
                CommandIDs.TestCommand.RunIfHas(
                    _Message,
                    function (_Data) {
                        __This.setState({
                            ButtonName: _Data.Params,
                        });
                    }
                );
            });*/
        }
        ,
        HandleGetName: function () {
            var __This = this;

            Actions.CheckLogin(function (_Message) {
                CommandIDs.ResultItemCommand.RunIfHas(
                    _Message,
                    function (_Data) {
                        __This.setState({
                            ButtonName: _Data.Item.Name,
                        });
                    }
                );
            });
        }
        ,
        render: function () {
            const { classes } = this.props;

            return (
                <Grid container>
                    <Grid item>
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
                            ) : this.state.ButtonName
                        }
                        </Button>
                    </Grid>
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
                    <Grid item>
                        <Button
                            fullWidth
                            variant="contained"
                            color="secondary"
                            onClick={this.HandleRedirect}
                            block="true"
                        >
                            {"Redirect"}
                        </Button>
                    </Grid>
                </Grid>
            )
        },
    },
  {}
);

export default withStyles(GlobalStyles)(TAdminMainPage);
