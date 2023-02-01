import React from "react";
import {  Class,  JSTypeOperator,  ObjectTypes } from "../../GenericCoreGraph/ClassFramework/Class";
import TObject from "../TObject";
import GenericWebGraph from "../../GenericWebController/GenericWebGraph";
import Actions from "../../GenericWebController/ActionGraph/Actions";
import { CommandIDs } from "../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs";
import { withStyles } from "@mui/styles";

import AdminStyles from "../../../ScriptStyles/AdminStyles";

var TBaseHeader = Class(TObject,
    {
        ObjectType: ObjectTypes.Get("TBaseHeader"),
        constructor: function (_Props) {
            TBaseHeader.BaseObject.constructor.call(this, _Props);
            this.state = {
                ...this.state,
                UnreadedNotificationCount : 0
            };
            window.App.Header = this;
        },
        Destroy: function () {
            TBaseHeader.BaseObject.Destroy.call(this);
        },
        AsyncLoad: function () {
            TBaseHeader.BaseObject.AsyncLoad.call(this);
        },
        HandleRefreshNotifications() {
            this.setState({
                UnreadedNotificationCount: window.App.Aside.HandleGetUnreadedCount(),
            });
        }
    },
  {}
);

export default TBaseHeader;
