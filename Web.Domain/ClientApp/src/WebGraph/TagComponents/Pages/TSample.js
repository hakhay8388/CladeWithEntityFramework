import React from "react";

import GenericWebGraph from "../../../WebGraph/GenericWebController/GenericWebGraph";
import { CommandInterfaces } from "../../GenericWebController/CommandInterpreter/cCommandInterpreter";
import {
  Class,
  ObjectTypes,
} from "../../../WebGraph/GenericCoreGraph/ClassFramework/Class";
import TObject from "../../../WebGraph/TagComponents/TObject";
import Actions from "../../../WebGraph/GenericWebController/ActionGraph/Actions";
import { CommandIDs } from "../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs";
import { withStyles } from "@mui/styles";
import GlobalStyles from "../../../ScriptStyles/GlobalStyles";

import { Grid, Button, CircularProgress } from "@mui/material";


var TSample = Class(TObject,
{
    ObjectType: ObjectTypes.Get("TSample"),
        constructor: function (_Props) {
            TSample.BaseObject.constructor.call(this, _Props);
            this.state = {
                ...this.state,
            };
        },
        AsyncLoad: function () {
            TSample.BaseObject.AsyncLoad.call(this);
        },
        Destroy: function () {
            TSample.BaseObject.Destroy.call(this);
        }
        ,
        render() {
            const { classes } = this.props;
            var __This = this;
            return (null);
        },
    },
  {}
);

export default withStyles(GlobalStyles)(TSample);
