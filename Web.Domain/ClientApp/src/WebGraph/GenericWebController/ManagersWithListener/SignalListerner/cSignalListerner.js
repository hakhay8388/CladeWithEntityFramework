import React, { Component } from 'react';
import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator } from "../../../GenericCoreGraph/ClassFramework/Class"
import cBaseObject from "../../../GenericCoreGraph/BaseObject/cBaseObject"
import GenericWebGraph from "../../GenericWebGraph"
import cBaseManagersWithListener from "../cBaseManagersWithListener";
import { CommandInterfaces } from "../../CommandInterpreter/cCommandInterpreter"
import { CommandIDs } from "../../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs"
import Actions from "../../../GenericWebController/ActionGraph/Actions";


import { HubConnection, HubConnectionBuilder, LogLevel, HubConnectionState } from '@microsoft/signalr';

var cSignalListerner = Class(cBaseManagersWithListener
  , CommandInterfaces.IDoReconnectSignalRequestCommandReceiver
  ,
    {
        ObjectType: ObjectTypes.Get("cSignalListerner")
        ,
        constructor: function () {
            cSignalListerner.BaseObject.constructor.call(this);
            this.Enabled = false;
            this.IsConnected = false
            this.HubConnection = null;
            this.SignalEvent = [];
            this.Initialize();
            this.ControlMessage = "Test";
            this.HandleControlConnection();
            var __This = this;
            this.PeriodicControlHandle = setTimeout(function () {
                __This.HandlePeriodicControl();
            }, 5000);
            window.SignalListerner = this;
        }
        ,
        HandleAddSignalEvent : function (_Function) {
            this.SignalEvent.push(_Function);
        }
        ,
        HandleRemoveSignalEvent : function (_Function) {
            for (var i = this.SignalEvent.length - 1; i > -1; i--) {
                if (this.SignalEvent[i] === _Function) {
                    this.SignalEvent.splice(i, 1);
                }
            }
        }
        ,
        HandleTriggerSignalEvent : function () {
            for (var i = 0; i < this.SignalEvent.length; i++) {
                this.SignalEvent[i](this.IsConnected);
            }
        }
        ,
        Receive_DoReconnectSignalRequestCommand: function (_Data) {
            this.HandleDisconnect();
            var __This = this;
            setTimeout(function () {
                __This.HandleConnect();
            }, 10000)
        }
        ,
        HandleSetIsConnected: function (__Value) {
            this.IsConnected = __Value;
            this.HandleTriggerSignalEvent();
        }
        ,
        HandleConnect: function () {
            this.Enabled = true;
            this.HubConnection.stop();
            DebugAlert.Show('Signal Connection Stoped');
            //this.Initialize();
            this.HandleControlConnection();
        }
        ,
        HandleDisconnect: function () {
            this.Enabled = false;
            this.HubConnection.stop();
        }
        ,
        HandlePeriodicControl: function () {
            var __This = this;
            this.HandleSetIsConnected(this.HubConnection.state == HubConnectionState.Connected);
            if (this.IsConnected) {
                DebugAlert.Show('Signal Connection Status : ' + this.IsConnected);
                this.HandleSendTestData();
            }
            else if (this.Enabled) {
                DebugAlert.Show('Signal Connection Status : ' + this.IsConnected);
                //this.Initialize();
                this.HandleControlConnection();
            }

            this.PeriodicControlHandle = setTimeout(function () {
                __This.HandlePeriodicControl();
            }, 5000);
        }
        ,
        Initialize: function () {
            var __This = this;
            var __Protocol = window.location.protocol;
            var __Slashes = __Protocol.concat("//");
            var __Host = __Slashes.concat(window.GetHost());

            this.HubConnection = new HubConnectionBuilder()
                .withUrl(__Host + "/signalr")
                .configureLogging(LogLevel.Debug)
                .build();

            this.HubConnection.onclose = this.HandleOnClose;

            this.HubConnection.on('CommandChannel', this.HandleReceivedData);
            this.HubConnection.on('ControlConnection', this.HandleControlReceivedData);
        }
        /*,
        HandleOnClose: function (_Error)
        {
            clearTimeout(this.PeriodicControlHandle);
            var __This = this;
            console.log("Signal sunucusuna yeniden bağlanılıyor.");
            this.IsConnected = false;
    
            this.HandlePeriodicControl();
        }*/
        ,
        HandleStartConnection: function (_Success, _Fail) {
            this.HubConnection
                .start({ waitForPageLoad: true })
                .then(() => {
                    DebugAlert.Show('Connection started!');
                    _Success();
                })
                .catch(err => {
                    DebugAlert.Show('Error while establishing connection', err)
                    _Fail();
                });

        }
        ,
        HandleSendTestData: function () {
            var __This = this;
            __This.HubConnection
                .invoke('ControlConnection', __This.ControlMessage)
                .catch(err => {
                    DebugAlert.Show(err);
                    __This.HandleSetIsConnected(false);
                });
        }
        ,
        HandleControlConnection: function () {
            if (this.Enabled) {
                var __This = this;
                this.HandleStartConnection(function () {
                    __This.HandleSendTestData();
                },
                    function () {
                        __This.HandleSetIsConnected(false);
                    });
            }
        }
        ,
        HandleControlReceivedData: function (_Message) {
            /*if (_Message == "Refresh=true")
            {
              Actions.CheckLogin();
            }*/
            DebugAlert.Show("Signal Test Message Recived : " + _Message);
            if (_Message == this.ControlMessage) {
                this.HandleSetIsConnected(true);
            }
        }
        ,
        SendMessage(_Message) {
            this.HubConnection
                .invoke('ReceiveMessage', _Message)
                .catch(err => console.error(err));
        }
        ,
        HandleReceivedData: function (_Message) {
            DebugAlert.Show(_Message);
            try {
                var __MessageObj = JSON.parse(_Message);
                __MessageObj.ActionProps =
                {
                    JsonableObject: null,
                    ResultFunction: null,
                    RequestAction: null
                };
                GenericWebGraph.CommandInterpreter.InterpretCommand(__MessageObj);
            }
            catch (_Ex) {
                DebugAlert.Show(_Ex);
            }
        }
        ,
        Destroy: function () {
            //GenericWebGraph.CommandInterpreter.DisconnectToCommands(this);
            cSignalListerner.BaseObject.Destroy.call(this);
        }
    }, {});

export default cSignalListerner







