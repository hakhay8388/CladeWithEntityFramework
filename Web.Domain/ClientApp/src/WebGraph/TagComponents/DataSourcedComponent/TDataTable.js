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

import { Typography, CircularProgress, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, TablePagination } from "@mui/material";
import { withStyles } from "@mui/styles";
import MaterialTableStyles from "../../../ScriptStyles/MaterialTableStyles";


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
                CanCreate: false,
                CanDelete: false,
                CanRead: false,
                CanUpdate: false,
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
                Page: _NewPage
            });
        }
        ,
        HandleChangeRowsPerPage: function (_Event)
        {
            this.setState({
                Page: 0,
                RowsPerPage: _Event.target.value
            });
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
                                    }, __This.HandleRead);

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
                var __This = this;

                Actions.DataSource_Read(__This.props.datasource,
                    __This.state.RowsPerPage,
                    __This.state.Page,
                    _Query.orderBy ? _Query.orderBy.field : "",
                    _Query.orderDirection ? _Query.orderDirection : "",
                    _Query.filters,
                    _Query.search,
                    __This.props.options ? __This.props.options : null,
                    function (_Message)
                    {

                    });
            }
        }
        ,
        Destroy: function ()
        {
            TDataTable.BaseObject.Destroy.call(this);
        },
        HandleGetRowStyle: function (_MetaItem)
        {
            if (_MetaItem.Visible)
            {
                return {};
            }
            else
            {
                return {
                    display: "none"
                };
            }
        }
        ,
        HandleGetTableHeader: function ()
        {
            var __This = this;

            __This.state.Columns.sort((_Item1, _Item2) =>
            {
                return _Item1.OrderFromLeft > _Item2.OrderFromLeft;
            });

            return <TableHead>
                <TableRow>
                    {__This.state.Columns.map(function (_MetaItem, _Index)
                    {
                        return <TableCell sx={__This.HandleGetRowStyle(_MetaItem)} key={_Index}>{_MetaItem.Title}</TableCell>
                    })}
                </TableRow>
            </TableHead>;
        }
        ,
        HandleGetDisplayedRows: function ({ from, to, count, page })
        {
            return `${from}–${to} ${this.state.Language.TotalRecord.format(count !== -1 ? count : this.Language.MoreThan.format(to))}`;
        }
        ,
        render: function ()
        {
            var __This = this;
            const { classes } = this.props;
            return (
                <div>
                    <TableContainer component={Paper}>
                        <Table sx={{ minWidth: 650 }} aria-label="simple table">
                            {__This.HandleGetTableHeader()}
                            <TableBody sx={{ minHeight: 200 }}>
                                {/*rows.map((row) => (
                                    <TableRow
                                        key={row.name}
                                        sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                                    >
                                        <TableCell component="th" scope="row">
                                            {row.name}
                                        </TableCell>
                                        <TableCell align="right">{row.calories}</TableCell>
                                        <TableCell align="right">{row.fat}</TableCell>
                                        <TableCell align="right">{row.carbs}</TableCell>
                                        <TableCell align="right">{row.protein}</TableCell>
                                    </TableRow>
                                ))*/}
                            </TableBody>
                        </Table>
                    </TableContainer>
                    <TablePagination
                        labelRowsPerPage={"Test"}
                        labelDisplayedRows={this.HandleGetDisplayedRows}
                        rowsPerPageOptions={__This.state.PageSizes}
                        component="div"
                        count={this.state.Rows.length}
                        rowsPerPage={this.state.RowsPerPage}
                        page={this.state.Page}
                        onPageChange={this.HandleChangePage}
                        onRowsPerPageChange={this.HandleChangeRowsPerPage}
                    />
                </div>
            );
        },
    },
    {}
);

export default withStyles(MaterialTableStyles)(TDataTable);
