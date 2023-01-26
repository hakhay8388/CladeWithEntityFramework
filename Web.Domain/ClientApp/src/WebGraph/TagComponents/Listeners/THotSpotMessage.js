import React, { Component, View } from "react";
import {
  DebugAlert,
  Class,
  Interface,
  Abstract,
  ObjectTypes,
  JSTypeOperator,
} from "../../GenericCoreGraph/ClassFramework/Class";
import TObject from "../../TagComponents/TObject";
import { CommandInterfaces } from "../../../WebGraph/GenericWebController/CommandInterpreter/cCommandInterpreter";
import Actions from "../../../WebGraph/GenericWebController/ActionGraph/Actions";
import GenericWebGraph from "../../GenericWebController/GenericWebGraph";
import { Snackbar, Typography} from '@mui/material';


var THotSpotMessage = Class(
  TObject,
  CommandInterfaces.IHotSpotMessageCommandReceiver,
  CommandInterfaces.IHotSpotMessageAndRunCommandCommandReceiver,
  {
    ObjectType: ObjectTypes.Get("THotSpotMessage"),
    Current: null,
    constructor: function (_Props) {
      THotSpotMessage.BaseObject.constructor.call(this, _Props);
      this.notificationDOMRef = React.createRef();
      this.state = {
        ...this.state,
        header: null,
        message: null,
        colorType: null,
        duration: null,
        open: false,
      };
      window.App.HotSpotMessage = this;
    },
    Destroy: function () {
      THotSpotMessage.BaseObject.Destroy.call(this);
    },
    componentDidMount: function () {
      THotSpotMessage.BaseObject.componentDidMount.call(this);
      this.Current = this.notificationDOMRef.current;
    },
    Receive_HotSpotMessageAndRunCommandCommand: function (_Data) {
      if (_Data.RunBeforeCommand) {
        _Data.RunBeforeCommand.ActionProps = _Data.ActionProps;
        GenericWebGraph.CommandInterpreter.InterpretCommand(
          _Data.RunBeforeCommand
        );
      }

      var __ID = this.ShowHotSpot(
        _Data.Header,
        _Data.Message,
        _Data.ColorType.Name,
        _Data.DurationMS - 500
      );

      if (_Data.RunAfterCommand) {
        var __RunAfterCommand = _Data.RunAfterCommand;
        __RunAfterCommand.ActionProps = _Data.ActionProps;
        setTimeout(function () {
          if (
            __RunAfterCommand.ActionProps.ResultFunction &&
            JSTypeOperator.IsFunction(
              __RunAfterCommand.ActionProps.ResultFunction
            )
          ) {
            var __ResultValue =
              __RunAfterCommand.ActionProps.ResultFunction(__RunAfterCommand);
            if (!JSTypeOperator.IsDefined(__ResultValue) || __ResultValue) {
              try {
                GenericWebGraph.CommandInterpreter.InterpretCommand(
                  __RunAfterCommand
                );
              } catch (_Ex) {
                DebugAlert.Show("CommandInterpreter'a data gelmedi..!");
              }
            }
          } else {
            try {
              GenericWebGraph.CommandInterpreter.InterpretCommand(
                __RunAfterCommand
              );
            } catch (_Ex) {
              DebugAlert.Show("CommandInterpreter'a data gelmedi..!");
            }
          }
        }, _Data.DurationMS + 500);
      }
    },
    Receive_HotSpotMessageCommand: function (_Data) {
      var __This = this;
      if (_Data.WaitTime == 0) {
        this.ShowHotSpot(
          _Data.Header,
          _Data.Message,
          _Data.ColorType.Name,
          _Data.DurationMS
        );
      } else if (_Data.WaitTime > 0) {
        setTimeout(function () {
          __This.ShowHotSpot(
            _Data.Header,
            _Data.Message,
            _Data.ColorType.Name,
            _Data.DurationMS + 500
          );
        }, _Data.WaitTime);
      }
    },
    HandleClose: function () {
      var __This = this;
      __This.setState({
        header: "",
        message: "",
        duration: 0,
        open: false,
      });
    },
    ShowHotSpot: function (_Header, _Message, _Type, _Duration) {
      var __This = this;
      __This.setState({
        header: _Header,
        message: _Message,
        duration: _Duration,
        colorType: _Type,
        open: true,
      });
    },
    render: function () {
        return (

            <Snackbar
                open={this.state.open}
                autoHideDuration={this.state.duration}
                onClose={() => {
                    this.setState({ open: false });
                }}
                anchorOrigin={{ vertical: "top", horizontal: "center" }}
                message={this.state.header}
                /*action={action}*/
            />

        /*<Snackbar
          open={this.state.open}
          autoHideDuration={this.state.duration}
          onClose={() => {
            this.setState({ open: false });
          }}
          anchorOrigin={{ vertical: "top", horizontal: "center" }}
        >
          <Alert
            onClose={() => {
              this.setState({ open: false });
            }}
            severity={this.state.colorType}
          >
            <Typography style={{ fontWeight: "bold" }}>
              {this.state.header}
            </Typography>
            <Typography>{this.state.message}</Typography>
          </Alert>
        </Snackbar>*/
      );
    },
  },
  {}
);

export default THotSpotMessage;
