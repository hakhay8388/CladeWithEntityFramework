import React, { Component } from 'react';
import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator } from "../../GenericCoreGraph/ClassFramework/Class"
import TObject from "../../TagComponents/TObject"
import { CommandInterfaces } from "../../GenericWebController/CommandInterpreter/cCommandInterpreter"
import Actions from "../../GenericWebController/ActionGraph/Actions"
import GenericWebGraph from "../../GenericWebController/GenericWebGraph"
import TBaseDialogModal from "../Utilities/TBaseDialogModal";

import { withStyles } from "@mui/styles";
import InfoModalStyles from "../../../ScriptStyles/InfoModalStyles";
import classNames from 'classnames';

import { Dialog, DialogTitle, DialogContent, DialogActions, Typography, Button, Paper, Divider } from '@mui/material';



var TMessageBox = Class(TBaseDialogModal,
    CommandInterfaces.IShowMessageCommandReceiver,
    CommandInterfaces.IShowMessageAndRunCommandCommandReceiver,
    {
        ObjectType: ObjectTypes.Get("TMessageBox")
        , Message: null
        , Buttons: null
        , MessageButtonsID: null
        , CloseRequired: null
        , DefaultButtonNo: null
        , RunAfterCommand: null
        , RequestObject: null
        ,
        constructor: function (_Props) {
            TMessageBox.BaseObject.constructor.call(this, _Props);
            this.state =
            {
                ...this.state,
                modalTitleType: "",
            };
            window.App.MessageBox = this;
        }
        ,
        Destroy: function () {
            TMessageBox.BaseObject.Destroy.call(this);
        }
        ,
        Receive_ShowMessageAndRunCommandCommand: function (_Data) {
            if (_Data.RunBeforeCommand) {
                _Data.RunBeforeCommand.ActionProps = _Data.ActionProps;
                GenericWebGraph.CommandInterpreter.InterpretCommand(_Data.RunBeforeCommand);
            }
            var __ID = this.Receive_ShowMessageCommand(_Data);
            if (_Data.RunAfterCommand) {
                this.RunAfterCommand = _Data.RunAfterCommand;
                this.RunAfterCommand.ActionProps = _Data.ActionProps;
            }
        }
        ,
        Receive_ShowMessageCommand: function (_Data) {
            this.MessageButtonsID = _Data.MessageButtonsID;
            this.Message = _Data.Message;
            this.Buttons = _Data.Buttons;
            this.CloseRequired = _Data.CloseRequired;
            this.DefaultButtonNo = _Data.DefaultButtonNo;
            this.RequestObject = _Data.RequestObject
            this.NeedUpdate = true;
            var __This = this;

            this.setState({
                open: true,
                modalTitleType: _Data.Message.Type
            }, () => {
                __This.HandleStopRenderWhenClosed();
            });

            if (!_Data.CloseRequired) {
                var __This = this;
                this.ThisSetTimeout = setTimeout(function () {
                    __This.setState({
                        open: false
                    });
                }, 3000);
            }
        }
        ,
        ShowMessage: function (MessageHeader, _Message, _Type, _CloseRequired) {
            this.Message = { Type: _Type, Header: MessageHeader, Message: _Message };
            this.Buttons = {
                First: { Type: _Type, Text: this.state.Language.Close, Enabled: _CloseRequired },
                Second: { Type: "", Text: "", Enabled: false },
                Third: { Type: "", Text: "", Enabled: false }
            };
            this.CloseRequired = _CloseRequired;
            this.DefaultButtonNo = 4;
            this.NeedUpdate = true;
            var __This = this;

            this.setState({
                open: true,
                modalTitleType: _Type
            }, () => {
                __This.HandleStopRenderWhenClosed();
            });

            if (!_CloseRequired) {
                var __This = this;
                this.ThisSetTimeout = setTimeout(function () {
                    __This.setState({
                        open: false,
                        modalTitleType: _Type
                    });
                }, 3000);
            }
        }
        ,

        //ButtonInner { Type: _Type, Text: this.state.Language.Yes, Enabled: true, Action: _Action1 }
        ShowMessageCustom: function (
            _MessageHeader
            , _Message
            , _Type
            , _Button1 = { Type: "", Text: "", Enabled: false, Action: null }
            , _Button2 = { Type: "", Text: "", Enabled: false, Action: null }
            , _Button3 = { Type: "", Text: "", Enabled: false, Action: null }
            , _DefaultButtonNo = 4
        ) {
            this.Message = { Type: _Type, Header: _MessageHeader, Message: _Message };
            this.Buttons = {
                First: _Button1,
                Second: _Button2,
                Third: _Button3
            };
            this.DefaultButtonNo = _DefaultButtonNo;
            var __This = this;
            this.NeedUpdate = true;
            this.setState({
                open: true,
                modalTitleType: _Type
            }, () => {
                __This.HandleStopRenderWhenClosed();
            });
        }
        ,
        HandleFirstButton: function () {
            if (this.Buttons && this.Buttons.First && this.Buttons.First.Action && JSTypeOperator.IsFunction(this.Buttons.First.Action)) {
                this.Buttons.First.Action(this);
            }
            else {
                if (this.DefaultButtonNo != 4) {
                    Actions.MessageResult(this.MessageButtonsID, 1, this.RequestObject, { ShowLoading: true });
                }
            }
            this.HandleClose();
        }
        ,
        HandleSecondButton: function () {
            if (this.Buttons && this.Buttons.Second && this.Buttons.Second.Action && JSTypeOperator.IsFunction(this.Buttons.Second.Action)) {
                this.Buttons.Second.Action(this);
            }
            else {
                Actions.MessageResult(this.MessageButtonsID, 2, this.RequestObject, { ShowLoading: true });
            }
            this.HandleClose();
        }
        ,
        HandleThirdButton: function () {
            if (this.Buttons && this.Buttons.Third && this.Buttons.Third.Action && JSTypeOperator.IsFunction(this.Buttons.Third.Action)) {
                this.Buttons.Third.Action(this);
            }
            else {
                Actions.MessageResult(this.MessageButtonsID, 3, this.RequestObject, { ShowLoading: true });
            }
            this.HandleClose();
        }
        ,
        HandleClose() {
            clearTimeout(this.ThisSetTimeout);
            this.setState({
                open: false
            });
            if (this.RunAfterCommand) {
                var __RunAfterCommand = this.RunAfterCommand;
                this.RunAfterCommand = null;

                if (JSTypeOperator.IsFunction(__RunAfterCommand.ActionProps.ResultFunction)) {
                    var __ResultValue = __RunAfterCommand.ActionProps.ResultFunction(__RunAfterCommand);
                    if (!JSTypeOperator.IsDefined(__ResultValue) || __ResultValue) {
                        try {
                            GenericWebGraph.CommandInterpreter.InterpretCommand(__RunAfterCommand);
                        }
                        catch (_Ex) {
                            DebugAlert.Show("CommandInterpreter'a data gelmedi..!");
                        }
                    }
                }
                else {
                    try {
                        GenericWebGraph.CommandInterpreter.InterpretCommand(__RunAfterCommand);
                    }
                    catch (_Ex) {
                        DebugAlert.Show("CommandInterpreter'a data gelmedi..!");
                    }
                }


            }
        }
        ,
        HandleOnCloseReaction: function () {
            var __This = this;
            if (__This.DefaultButtonNo === 1) this.HandleFirstButton();
            if (__This.DefaultButtonNo === 2) this.HandleSecondButton();
            if (__This.DefaultButtonNo === 3) this.HandleThirdButton();
            if (__This.DefaultButtonNo === 4) this.HandleClose();
        }
        ,
        HandleTypeBackgroundColorByStyle: function (_Type) {
            try {
                if (_Type == "none")
                {
                    return "background.paper"
                }
                else return _Type + ".main";
            }
            catch (_Ex) {
                return "primary.main"
            }
        }
        ,
        HandleTitleColorByStyle: function (_Type) {
            if (_Type === "default")
            {
                return "text.primary";
            }
            else if (_Type === "none") {
                return "text.primary";
            }
            else {
                return _Type + ".main";
            }
        }
        ,
        HandleTypeColorByStyle: function (_Type) {
            try {
                if (_Type == 'default') {
                    return "text.primary";
                }
                return _Type + ".main";
            }
            catch (_Ex) {
                return "text.primary";
            }
        }
        ,
        render: function () {
            const { classes } = this.props;
            var __Params = TMessageBox.BaseObject.render.call(this);
            if (!__Params.NeedRender) {
                return (<div></div>);
            }
            return (
                !this.Message ? (<div></div>) :
                    <Dialog maxWidth={"sm"} fullWidth={!this.state.IsFullScreen} open={this.state.open} onClose={() => { this.HandleOnCloseReaction() }} classes={{ paper: classes.dialog }}>
                        <DialogTitle style={{ backgroundColor: this.HandleTypeBackgroundColorByStyle(this.state.modalTitleType) }}>
                            <Typography sx={{ color: this.HandleTitleColorByStyle(this.state.modalTitleType), fontWeight: "bold" }} >
                                {this.Message.Header}
                            </Typography>
                        </DialogTitle>
                        <DialogContent dividers>
                            <Typography>
                                {this.Message.Message}
                            </Typography>
                        </DialogContent>
                        <DialogActions>
                            {this.Buttons.First.Enabled && (<Button color={this.Buttons.First.Type} variant="contained" onClick={() => { this.HandleFirstButton() }}>{this.Buttons.First.Text}</Button>)}{' '}
                            {this.Buttons.Second.Enabled && (<Button color={this.Buttons.Second.Type} variant="contained" onClick={() => { this.HandleSecondButton() }}>{this.Buttons.Second.Text}</Button>)}{' '}
                            {this.Buttons.Third.Enabled && (<Button color={this.Buttons.Third.Type} variant="contained" onClick={() => { this.HandleThirdButton() }}>{this.Buttons.Third.Text}</Button>)}{' '}
                        </DialogActions>
                    </Dialog>
            );
        }
    }, {});

export default withStyles(InfoModalStyles)(TMessageBox);

