import React, { Component } from 'react';
import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator } from "../../../GenericCoreGraph/ClassFramework/Class";
import { WebGraph } from "../../../GenericCoreGraph/WebGraph/WebGraph";
import cBaseObject from "../../../GenericCoreGraph/BaseObject/cBaseObject";
//import $ from 'jquery';
import queryString from 'query-string';
import cBaseManagersWithListener from "../cBaseManagersWithListener";
import { CommandInterfaces } from "../../CommandInterpreter/cCommandInterpreter"
import { CommandIDs } from "../../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs"

var cGlobalParamsManager = Class(cBaseManagersWithListener
  , CommandInterfaces.ISetGlobalParamListCommandReceiver
  ,
    {
        ObjectType: ObjectTypes.Get("cGlobalParamsManager")
        ,
        constructor: function () {
            cGlobalParamsManager.BaseObject.constructor.call(this);
            window.GlobalParamsManager = this;
        }
        ,
        Receive_SetGlobalParamListCommand: function (_Data) {
            window.App.GlobalParams = {};
            for (var i = 0; i < _Data.ParamList.length; i++) {
                window.App.GlobalParams[_Data.ParamList[i].ParamName] = _Data.ParamList[i].ParamValue;
            }

            DebugAlert.WriteConsole = window.App.GlobalParams.FrontEndDebugMessage;
        }
        ,
        Destroy: function () {
            cGlobalParamsManager.BaseObject.Destroy.call(this);
        }
    }, {});

export default cGlobalParamsManager







