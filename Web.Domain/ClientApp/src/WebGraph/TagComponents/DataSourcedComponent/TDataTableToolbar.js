import React from "react";
import Actions from "../../GenericWebController/ActionGraph/Actions";
import
{
    Class,
    JSTypeOperator,
    ObjectTypes,
} from "../../GenericCoreGraph/ClassFramework/Class";
import TObject from "../TObject";
import GenericWebGraph from "../../GenericWebController/GenericWebGraph";
import { CommandInterfaces } from "../../GenericWebController/CommandInterpreter/cCommandInterpreter";
import { CommandIDs } from "../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs";

import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";

import { Typography, Toolbar, Tooltip, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, TablePagination } from "@mui/material";
import { withStyles } from "@mui/styles";
import MaterialTableStyles from "../../../ScriptStyles/MaterialTableStyles";



var TDataTableToolbar = Class(TObject,
    {
        ObjectType: ObjectTypes.Get("TDataTableToolbar"),
        constructor: function (_Props)
        {
            TDataTableToolbar.BaseObject.constructor.call(this, _Props);
            this.state = {
                ...this.state,
            };
        }
        ,
        Destroy: function ()
        {
            TDataTableToolbar.BaseObject.Destroy.call(this);
        },
        render: function ()
        {
            return (
                <Toolbar>
                    <Typography
                        sx={{ flex: '1 1 100%' }}
                        variant="h6"
                        id="tableTitle"
                        component="div"
                    >
                        {this.state.Language[this.props.title]}
                    </Typography>
                </Toolbar>
            );
        },
    },
    {}
);

export default withStyles(MaterialTableStyles)(TDataTableToolbar);
