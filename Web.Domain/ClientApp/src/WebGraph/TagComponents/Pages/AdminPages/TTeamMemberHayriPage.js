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

import { Button, MenuItem, Select, TextField, CircularProgress } from "@mui/material";

var TTeamMemberHayriPage = Class(TObject,
    {
        ObjectType: ObjectTypes.Get("TTeamMemberHayriPage"),
        constructor: function (_Props) {
            TTeamMemberHayriPage.BaseObject.constructor.call(this, _Props);
            this.state = {
                ...this.state,
                UserName : "",
                Name: "Test",
                Loading: false,
                ListItems : []
            };
        },
        AsyncLoad: function () {
            var __This = this;
            Actions.AddLawsuit(__This.state.UserName, "Deneme", function (_Message)
            {
                CommandIDs.ResultListCommand.RunIfHas(_Message, function (_Data)
                {
                    __This.setState({
                        ListItems: _Data.ResultList
                    });
                });
            });
        },
        Destroy: function () {
            TTeamMemberHayriPage.BaseObject.Destroy.call(this);
        }
        ,
        HandleTestOnClick: function (_Event, _Data)
        {
            var __This = this;
            this.setState({
                Loading : false
            }, () =>
            {
                Actions.AddLawsuit(__This.state.UserName, "Deneme", function (_Message)
                {
                    CommandIDs.ResultListCommand.RunIfHas(_Message, function (_Data)
                    {
                        __This.setState({
                            ListItems: _Data.ResultList
                        });
                    });
                });
            });
        }
        ,
        HandleOnUserNameChange: function (_Event)
        {
            this.setState({
                UserName: _Event.target.value
            });
        }
        ,
        HandleChange: function (_Event)
        {
            this.setState({
                Name: _Event.target.value,
            });
        }
        ,
        render()
        {
            var __This = this;
            const { classes } = this.props;

            return (
                <div>
                    <TextField id="outlined-basic" label="Outlined" variant="outlined" value={this.state.UserName} onChange={this.HandleOnUserNameChange} />
                    <Button onClick={(_Event) => __This.HandleTestOnClick(_Event, "asdajhkjh")} variant="contained" disabled={this.state.Loading} >
                        {this.state.Loading && <CircularProgress />}
                        {this.state.Name}
                    </Button>
                    <Select
                        labelId="demo-simple-select-label"
                        id="demo-simple-select"
                        value={this.state.Name}
                        label="Name"
                        onChange={this.HandleChange}
                    >
                        {__This.state.ListItems.map(function (__Item)
                        {
                            return <MenuItem value={__Item.ID}>{__Item.Name}</MenuItem>
                        })
                        }
                    </Select>
                </div>
            )
        },
    },
  {}
);

export default withStyles(GlobalStyles)(TTeamMemberHayriPage);
