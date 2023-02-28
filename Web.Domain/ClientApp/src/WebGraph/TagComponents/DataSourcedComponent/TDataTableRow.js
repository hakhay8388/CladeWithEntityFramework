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
import { WebGraph } from "../../GenericCoreGraph/WebGraph/WebGraph";
import { CommandInterfaces } from "../../GenericWebController/CommandInterpreter/cCommandInterpreter";
import { CommandIDs } from "../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs";

import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";

import { Typography, Toolbar, Tooltip, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, TablePagination } from "@mui/material";
import { withStyles } from "@mui/styles";
import MaterialTableStyles from "../../../ScriptStyles/MaterialTableStyles";



var TDataTableRow = Class(TObject,
    {
        ObjectType: ObjectTypes.Get("TDataTableRow"),
        constructor: function (_Props)
        {
            TDataTableRow.BaseObject.constructor.call(this, _Props);
            this.state = {
                ...this.state,
            };
        }
        ,
        Destroy: function ()
        {
            TDataTableRow.BaseObject.Destroy.call(this);
        },
        HandleGetActions: function ()
        {
            if (JSTypeOperator.IsArray(this.props.actions))
            {
                return this.props.actions.map(function (_Action, _Index)
                {
                    return <TableCell
                        align={"left"}
                        component="th"
                        key={WebGraph.GetNewCreateID()}
                        scope="row"
                    >
                        
                    </TableCell>
                });
            }
            return null;
        }
        ,
        render: function ()
        {
            var __This = this;
            return <TableRow hover /*sx={{ '&:last-child td, &:last-child th': { border: 0 } }}*/ >
               
                {this.HandleGetActions()}
                {__This.props.columns.map(function (_MetaItem, _Index)
                {
                    if (_MetaItem.Visible)
                    {
                        if (_Index == 0 && !JSTypeOperator.IsArray(__This.props.actions))
                        {
                            return <TableCell
                                align={_MetaItem.Type.Code == "Numeric" ? "right" : "left"}
                                component="th"
                                key={_MetaItem.FieldName + "_" + _MetaItem.ID + "_" + _Index}
                                scope="row"
                            >
                                {__This.props.row[_MetaItem.FieldName]}
                            </TableCell>
                        }
                        else
                        {
                            return <TableCell
                                align={_MetaItem.Type.Code == "Numeric" ? "right" : "left"}
                                key={_MetaItem.FieldName + "_" + _MetaItem.ID + "_" + _Index}>{__This.props.row[_MetaItem.FieldName]}
                            </TableCell>
                        }
                    }
                    return null;
                })}
            </TableRow>
        },
    },
    {}
);

export default withStyles(MaterialTableStyles)(TDataTableRow);
