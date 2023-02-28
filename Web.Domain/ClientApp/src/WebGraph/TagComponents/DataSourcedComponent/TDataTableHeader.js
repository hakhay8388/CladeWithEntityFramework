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

import { Typography, TableSortLabel, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, TablePagination, Box } from "@mui/material";
import { withStyles } from "@mui/styles";
import MaterialTableStyles from "../../../ScriptStyles/MaterialTableStyles";
import { visuallyHidden } from '@mui/utils';


var TDataTableHeader = Class(TObject, 
    {
        ObjectType: ObjectTypes.Get("TDataTableHeader"),
        constructor: function (_Props)
        {
            TDataTableHeader.BaseObject.constructor.call(this, _Props);
            this.state = {
                ...this.state,
            };
        }
        ,
        Destroy: function ()
        {
            TDataTableHeader.BaseObject.Destroy.call(this);
        },
        HandleOnSortClick: function (_MetaItem, _Event)
        {
            for (var i = 0; i < this.props.columns.length; i++)
            {
                if (this.props.columns[i].FieldName != _MetaItem.FieldName)
                {
                    this.state[this.props.columns[i].FieldName] = { Active: false, Direction: 'asc' };
                }                
            }

            var __CurrentState = "asc";
            if (this.state[_MetaItem.FieldName].Active)
            {
                __CurrentState = this.state[_MetaItem.FieldName].Direction == "asc" ? "desc" : "asc";
            }
            var __Result = { Active: true, Direction: __CurrentState };

            this.setState({
                [_MetaItem.FieldName]: __Result
            });

            if (JSTypeOperator.IsFunction(this.props.onSortChange)) this.props.onSortChange(__Result, _MetaItem, _Event);
        }
        ,
        render: function ()
        {
            var __This = this;

            __This.props.columns.sort((_Item1, _Item2) =>
            {
                return _Item1.OrderFromLeft > _Item2.OrderFromLeft;
            });

            return <TableHead>
                <TableRow>
                    {__This.props.columns.map(function (_MetaItem, _Index)
                    {
                        if (!JSTypeOperator.IsDefined(__This.state[_MetaItem.FieldName])) __This.state[_MetaItem.FieldName] = { Active: false, Direction: 'asc' };
                        if (_MetaItem.Visible)
                        {
                            return <TableCell
                                key={_Index}
                                align={_MetaItem.Type.Code == "Numeric" ? "right" : "left"}
                                style={(_MetaItem.Width != 0 ? { minWidth: _MetaItem.Width } : {})}
                                //padding={headCell.disablePadding ? 'none' : 'normal'}
                                sortDirection={__This.state[_MetaItem.FieldName].Active ? __This.state[_MetaItem.FieldName].Direction : false}
                            >
                                <TableSortLabel
                                    active={__This.state[_MetaItem.FieldName].Active}
                                    direction={__This.state[_MetaItem.FieldName].Direction}
                                    onClick={(_Event) => __This.HandleOnSortClick(_MetaItem, _Event)}
                                    sx={{ fontWeight: 600, color:"#505050"}}
                                >
                                    {_MetaItem.Title}
                                </TableSortLabel>
                            </TableCell>
                        }
                        return null;
                    })}
                </TableRow>
            </TableHead>;
        },
    },
    {}
);

export default withStyles(MaterialTableStyles)(TDataTableHeader);
