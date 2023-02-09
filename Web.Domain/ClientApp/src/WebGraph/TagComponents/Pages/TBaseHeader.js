import React from "react";
import {  Class,  JSTypeOperator,  ObjectTypes } from "../../GenericCoreGraph/ClassFramework/Class";
import TObject from "../TObject";
import GenericWebGraph from "../../GenericWebController/GenericWebGraph";
import Actions from "../../GenericWebController/ActionGraph/Actions";
import { CommandIDs } from "../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs";
import classNames from "classnames";
import { withStyles } from "@mui/styles";
import AdminStyles from "../../../ScriptStyles/AdminStyles";

import {
    AppBar, Toolbar, IconButton
} from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";

var TBaseHeader = Class(TObject,
    {
        ObjectType: ObjectTypes.Get("TBaseHeader"),
        constructor: function (_Props) {
            TBaseHeader.BaseObject.constructor.call(this, _Props);
            this.state = {
                ...this.state,
                UnreadedNotificationCount: 0,
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
        },
        HandleToggleLeftDrawer: function ()
        {
            window.App.ContainerLayout.HandleToggleLeftDrawer();
        },
        HandleWrapWithApp:function(_Childs) {
            var __This = this;
            const { children, classes, ...attributes } = this.props;
            return (
                <div className={classNames(classes.grow)} onClick={this.HandleCloseMenu}>
                    <AppBar
                        id="pageheader"
                        position="static"
                        color="default"
                        sx={[
                            {
                                width: `calc(100%)`,
                                marginLeft: 0,
                                transition: (theme) => theme.transitions.create(['margin', 'width', 'transform'], {
                                    easing: theme.transitions.easing.easeOut,
                                    duration: theme.transitions.duration.standard,
                                })

                            },
                            window.App.ContainerLayout.state.leftMenu && {
                                marginLeft: `${window.Settings.DrawerWidth}px`,
                                width: `calc(100% - ${window.Settings.DrawerWidth}px)`,
                            }
                        ]}
                    >
                        <Toolbar>
                            <div>
                                <IconButton
                                    onClick={(_Event) => {
                                        __This.HandleToggleLeftDrawer();
                                        _Event.preventDefault();
                                        _Event.stopPropagation();
                                    }}
                                >
                                    <MenuIcon />
                                </IconButton>
                            </div>
                            {_Childs}
                        </Toolbar>
                    </AppBar>
                </div>
            );
        },
    },
  {}
);

export default TBaseHeader;
