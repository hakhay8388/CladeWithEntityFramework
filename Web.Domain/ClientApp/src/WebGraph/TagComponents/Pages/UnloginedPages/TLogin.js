import React from "react";

import GenericWebGraph from "../../../../WebGraph/GenericWebController/GenericWebGraph";
import { CommandInterfaces } from "../../../GenericWebController/CommandInterpreter/cCommandInterpreter";
import {
  Class,
  ObjectTypes,
} from "../../../../WebGraph/GenericCoreGraph/ClassFramework/Class";
import TObject from "../../../../WebGraph/TagComponents/TObject";
import Actions from "../../../../WebGraph/GenericWebController/ActionGraph/Actions";
import { CommandIDs } from "../../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs";
import { withStyles } from "@mui/styles";
import GlobalStyles from "../../../../ScriptStyles/GlobalStyles";

import { Grid, Button, CircularProgress } from "@mui/material";


var TLogin = Class(
  TObject,
    CommandInterfaces.ILogInOutCommandReceiver,
    CommandInterfaces.IForceLogoutCommandReceiver,
    {
        ObjectType: ObjectTypes.Get("TLogin"),
        constructor: function (_Props) {
            TLogin.BaseObject.constructor.call(this, _Props);
            this.state = {
                ...this.state,
                ButtonEnabled: true,
                UserName: "user@user.com",
                Password: "1",
                StaySession: true
            };
        },
        AsyncLoad: function () {
            TLogin.BaseObject.AsyncLoad.call(this);
        },
        Destroy: function () {
            TLogin.BaseObject.Destroy.call(this);
        }
        ,
        Receive_ValidationResultCommand: function (_Data) {
            alert("ValidationResult")
            console.log(_Data);
        }
        ,
        Receive_ForceLogoutCommand: function (_Data) {
            alert("ForceLogout")
        }
        ,
        Receive_LogInOutCommand: function (_Data) {
            if (_Data.LoginState) {
                GenericWebGraph.ManagersWithListener.SignalListerner.HandleConnect();
            }
            else {
                GenericWebGraph.ManagersWithListener.SignalListerner.HandleDisconnect();
            }
        },
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
        HandleSubmit2(_Event) {
            _Event.preventDefault();
            alert(window.App.GlobalParams.TestParm1);
            var __This = this;
            this.setState(
                {
                    ButtonEnabled: false,
                },
                () => {
                    Actions.Login(
                        "user2@user2.com",
                        "1",
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
        HandleTest(_Event) {
            _Event.preventDefault();
            var __This = this;

            Actions.CheckLogin(function (_Message) {
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
        ,
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

                    <Button
                        fullWidth
                        variant="contained"
                        color="secondary"
                        disabled={!this.state.ButtonEnabled}
                        onClick={this.HandleSubmit2}
                        block="true"
                    >
                        {!this.state.ButtonEnabled ? (
                            <CircularProgress
                                style={{ width: "21px", height: "21px" }}
                                color={"primary"}
                            />
                        ) : this.state.Language.Actions
                        }
                    </Button>

                    <Button
                        fullWidth
                        variant="contained"
                        color="secondary"
                        disabled={!this.state.ButtonEnabled}
                        onClick={this.HandleTest}
                        block="true"
                    >
                        {!this.state.ButtonEnabled ? (
                            <CircularProgress
                                style={{ width: "21px", height: "21px" }}
                                color={"primary"}
                            />
                        ) : this.state.Language.Yes
                        }
                    </Button>
                </Grid>
            );
        },
    },
  {}
);

export default withStyles(GlobalStyles)(TLogin);
