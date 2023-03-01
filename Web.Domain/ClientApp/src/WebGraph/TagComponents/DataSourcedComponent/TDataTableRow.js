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

import { Typography, Toolbar, Tooltip, IconButton, Table, Checkbox, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, TablePagination } from "@mui/material";
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
            var __This = this;
            if (JSTypeOperator.IsArray(this.props.actions))
            {
                return this.props.actions.map(function (_Action, _Index)
                {
                    return __This.HandleGetActionInner(_Action);
                });
            }
            return null;
        }
        ,
        HandleGetActionInner: function (_Action)
        {
            var __This = this;
            if (JSTypeOperator.IsDefined(_Action))
            {
                return <TableCell
                    align={"left"}
                    component="th"
                    key={WebGraph.GetNewCreateID()}
                    scope="row"
                    padding={'none'}
                    style={{ width: 30, paddingLeft: 10 }}
                >
                    {_Action.component ? _Action.component :
                        <Tooltip title={_Action.tooltip}>
                            <IconButton onClick={function (_Event)
                            {
                                if (JSTypeOperator.IsFunction(_Action.onClick))
                                {
                                    _Action.onClick(_Event, __This.props.row);
                                }
                            }} >
                                <_Action.icon />
                            </IconButton>
                        </Tooltip>
                    }
                </TableCell>
            }
            return null;
        }
        ,
        HandleGetEdit: function (_)
        {
            var __This = this;
            if (JSTypeOperator.IsFunction(this.props.editAction))
            {
                return this.HandleGetActionInner({
                    icon: EditIcon,
                    tooltip: this.state.Language.Edit,
                    onClick: this.props.editAction
                });
            }
            return null;
        }
        ,
        HandleGetDelete: function (_)
        {
            var __This = this;
            if (JSTypeOperator.IsFunction(this.props.deleteAction))
            {
                return this.HandleGetActionInner({
                    icon: DeleteIcon,
                    tooltip: this.state.Language.Delete,
                    onClick: this.props.deleteAction
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
                {this.HandleGetEdit()}
                {this.HandleGetDelete()}
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
