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

import { Typography } from "@mui/material";
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
            };
            this.MaterialTableRef = React.createRef();
            this.ColumnCurrencySetting = {
                locale: "tr-TR",
                currencyCode: "TRY",
                minimumFractionDigits: 2,
            };
            this.Currency_symbol = "₺";
        },
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
            Actions.DataSource_GetMetaData(
                __This.props.datasource,
                function (_Message)
                {
                    CommandIDs.ResultListCommand.RunIfHas(_Message, function (_Data)
                    {
                    });
                }
            );
            /*
            Actions.DataSource_GetSettings(
                __This.props.datasource,
                function (_Message)
                {
                    CommandIDs.ResultListCommand.RunIfHas(_Message, function (_Data)
                    {
                        
                    });
                }
            );*/
        },
        Destroy: function ()
        {
            TDataTable.BaseObject.Destroy.call(this);
        },
        render: function ()
        {
            var __This = this;
            const { classes } = this.props;
            return (
                <div>
                   Hayri
                </div>
            );
        },
    },
    {}
);

export default withStyles(MaterialTableStyles)(TDataTable);
