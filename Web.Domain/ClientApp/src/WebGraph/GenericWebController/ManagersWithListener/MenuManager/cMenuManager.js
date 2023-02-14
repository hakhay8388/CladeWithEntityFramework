import React, { Component } from 'react';
import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator } from "../../../GenericCoreGraph/ClassFramework/Class"
import cBaseManagersWithListener from "../cBaseManagersWithListener";
import GenericWebGraph from "../../GenericWebGraph"
import { WebGraph } from "../../../GenericCoreGraph/WebGraph/WebGraph"
import {
    CommandInterfaces
} from "../../CommandInterpreter/cCommandInterpreter"
import Actions from "../../ActionGraph/Actions"
import { CommandIDs } from "../../CommandInterpreter/CommandIDs/CommandIDs"
import queryString from 'query-string';


var cMenuManager = Class(cBaseManagersWithListener
    , CommandInterfaces.IMenuResultCommandReceiver
    ,
    {
        ObjectType: ObjectTypes.Get("cMenuManager")
        ,
        constructor: function () {
            cMenuManager.BaseObject.constructor.call(this);
            this.MenuItems = [];
            window.MenuManager = this;
        }
        ,
        Destroy: function () {
            cMenuManager.BaseObject.Destroy.call(this);
        }
        ,
        Receive_MenuResultCommand: function (_Data)
        {
            this.MenuItems = _Data.MenuItems;
        }
    }, {});

export default cMenuManager







