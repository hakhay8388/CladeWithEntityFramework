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

import { Typography, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, TablePagination } from "@mui/material";
import { withStyles } from "@mui/styles";
import MaterialTableStyles from "../../../ScriptStyles/MaterialTableStyles";
import TDataTableToolbar from "./TDataTableToolbar";
import TDataTableHeader from "./TDataTableHeader";
import TDataTableRow from "./TDataTableRow";



var TDataTable = Class(
    TObject,
    CommandInterfaces.IDataSourceRefreshCommandReceiver,

    {
        ObjectType: ObjectTypes.Get("TDataTable"),
        constructor: function (_Props)
        {
            TDataTable.BaseObject.constructor.call(this, _Props);
            this.state = {
                ...this.state,
                Columns: [],
                Permissions: {},
                CustomActions: [],
                PageSizes: [],
                Rows: [],
                Page: 0,
                RowsPerPage: 5,
                TotalCount: 0,
                OrderField: "",
                OrderDirection: "",
                Search: "",
                CanCreate: false,
                CanDelete: false,
                CanRead: false,
                CanUpdate: false,
                DataSourceCode: null,
                Loading: true
            };

            /*
             * 
             *  rowsPerPageOptions={this.state.PageSizes}
                        component="div"
                        count={rows.length}
                        rowsPerPage={this.state.RowsPerPage}
                        page={this.state.Page}
                        onPageChange={this.HandleChangePage}
                        onRowsPerPageChange={this.HandleChangeRowsPerPage}
                        */

            this.MaterialTableRef = React.createRef();
            this.ColumnCurrencySetting = {
                locale: "tr-TR",
                currencyCode: "TRY",
                minimumFractionDigits: 2,
            };
            this.Currency_symbol = "₺";
        }
        ,
        HandleChangePage: function (_Event, _NewPage)
        {
            this.setState({
                Page: _NewPage,
                Loading: true
            }, this.HandleRead);
        }
        ,
        HandleChangeRowsPerPage: function (_Event)
        {
            this.setState({
                Page: 0,
                RowsPerPage: _Event.target.value,
                Loading: true
            }, this.HandleRead);
        }
        ,
        Receive_DataSourceRefreshCommand: function (_Data)
        {
            if (_Data.DataSource.ClientComponentName == this.props.datasource) 
            {
                this.Refresh();
            }
        },
        AsyncLoad: function ()
        {
            TDataTable.BaseObject.AsyncLoad.call(this);
            var __This = this;
            Actions.DataSource_GetMetaAndSettings(__This.props.datasource, function (_Message)
            {
                CommandIDs.DataSourceMetadataResultCommand.RunIfHas(_Message, function (_Data)
                {
                    __This.setState({
                        Columns: _Data.ResultList
                    }, () =>
                    {
                        CommandIDs.ResultItemCommand.RunIfHas(_Message, function (_Data2)
                        {
                            __This.setState({
                                PageSizes: _Data2.Item.PageSizes
                            }, () =>
                            {
                                CommandIDs.DataSourcePermissionResultCommand.RunIfHas(_Message, function (_Data3)
                                {
                                    if (_Data3.ResultList.length > 0)
                                    {
                                        var __CanCreate = false;
                                        var __CanDelete = false;
                                        var __CanRead = false;
                                        var __CanUpdate = false;

                                        for (var i = 0; i < _Data3.ResultList.length; i++)
                                        {
                                            if (_Data3.ResultList[i].CanCreate) __CanCreate = true;
                                            if (_Data3.ResultList[i].CanDelete) __CanDelete = true;
                                            if (_Data3.ResultList[i].CanRead) __CanRead = true;
                                            if (_Data3.ResultList[i].CanUpdate) __CanUpdate = true;
                                        }

                                        __This.setState({
                                            CanCreate: __CanCreate,
                                            CanDelete: __CanDelete,
                                            CanRead: __CanRead,
                                            CanUpdate: __CanUpdate,
                                            DataSourceCode: _Data3.ResultList[0].DataSourceCode
                                        }, __This.HandleRead);
                                    }

                                });
                            });
                        });
                    });
                });
            });
        },
        HandleRead: function ()
        {
            if (this.state.CanRead)
            {
                this.setState({
                    Loading: true
                }, () =>
                {
                    var __This = this;

                    Actions.DataSource_Read(__This.props.datasource,
                        __This.state.RowsPerPage,
                        __This.state.Page,
                        __This.state.OrderField ? __This.state.OrderField : "",
                        __This.state.OrderDirection ? __This.state.OrderDirection : "",
                        __This.state.Search,
                        __This.props.options ? __This.props.options : null,
                        function (_Message)
                        {
                            CommandIDs.ResultListCommand.RunIfHas(_Message, function (_Data)
                            {
                                __This.setState({
                                    TotalCount: _Data.Total,
                                    Page: _Data.Page,
                                    Rows: _Data.ResultList,
                                    Loading: false
                                });

                            });

                        });
                });
            }
        }
        ,
        Destroy: function ()
        {
            TDataTable.BaseObject.Destroy.call(this);
        }
        ,
        HandleGetDisplayedRows: function ({ from, to, count, page })
        {
            return `${from}–${to} ${this.state.Language.TotalRecord.format(count !== -1 ? count : this.Language.MoreThan.format(to))}`;
        }
        ,
        HandleGetRows: function ()
        {
            return this.state.Rows.map((_Row) =>
            {
                return <TDataTableRow
                    actions={this.props.actions}
                    editAction={this.props.editAction}
                    deleteAction={this.props.deleteAction}
                    key={_Row.ID}
                    row={_Row}
                    columns={this.state.Columns}
                />
            });
        }
        ,
        HandleOnSortChange: function (_Result, _MetaItem, _Event)
        {
            this.state.OrderField = _MetaItem.FieldName;
            this.state.OrderDirection = _Result.Direction;
            this.HandleRead();
        }
        ,
        render: function ()
        {
            var __This = this;
            const { classes } = this.props;
            return (
                <Paper sx={{ width: '100%', overflow: 'hidden' }}>
                    <TDataTableToolbar title={this.state.DataSourceCode} />
                    <TableContainer sx={{ maxHeight: 700 }}>
                        <Table sx={{ minWidth: 600 }} stickyHeader aria-label="simple table">
                            {<TDataTableHeader
                                actions={this.props.actions}
                                deleteAction={this.props.deleteAction}
                                editAction={this.props.editAction}
                                columns={this.state.Columns}
                                onSortChange={this.HandleOnSortChange} />}
                            <TableBody>
                                {this.HandleGetRows()}
                            </TableBody>
                        </Table>
                    </TableContainer>
                    <TablePagination
                        labelRowsPerPage={this.state.Language.LineCount}
                        labelDisplayedRows={this.HandleGetDisplayedRows}
                        rowsPerPageOptions={this.state.PageSizes}
                        component="div"
                        count={this.state.TotalCount}
                        rowsPerPage={this.state.RowsPerPage}
                        page={this.state.Page}
                        onPageChange={this.HandleChangePage}
                        onRowsPerPageChange={this.HandleChangeRowsPerPage}
                    />
                </Paper>
            );
        },
    },
    {}
);

export default withStyles(MaterialTableStyles)(TDataTable);
