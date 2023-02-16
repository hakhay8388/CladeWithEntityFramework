import React from "react";
import Actions from "../../GenericWebController/ActionGraph/Actions";
import {
  Class,
  JSTypeOperator,
  ObjectTypes,
} from "../../GenericCoreGraph/ClassFramework/Class";
import TObject from "../TObject";
import GenericWebGraph from "../../GenericWebController/GenericWebGraph";
import { CommandInterfaces } from "../../GenericWebController/CommandInterpreter/cCommandInterpreter";
import { CommandIDs } from "../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs";

import MaterialTable, { MTableToolbar } from "material-table";
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
    constructor: function (_Props) {
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
    Receive_DataSourceRefreshCommand: function (_Data) {
      if (_Data.DataSource.ClientComponentName == this.props.datasource) {
        this.Refresh();
      }
    },
    GetLookup: function (_ParamObject) {
      var __Obj = _ParamObject.reduce(function (acc, cur, i) {
        acc[cur.id] = cur.name;

        return acc;
      }, {});
      return __Obj;
    },
    Refresh: function () {
      this.MaterialTableRef.current.onQueryChange();
    },
    Reread: function () {
      var __This = this;
      if (
        __This.props.detailPanel != null &&
        __This.props.detailPanel.length > 0 &&
        JSTypeOperator.IsDefined(__This.props.selectedId) &&
        __This.props.selectedId != null &&
        __This.props.selectedId != "" &&
        JSTypeOperator.IsDefined(__This.props.columnName) &&
        __This.props.columnName != null &&
        __This.props.columnName != "" &&
        JSTypeOperator.IsDefined(__This.props.detailIndex) &&
        __This.MaterialTableRef.current != null
      ) {
        var __Items = [...this.MaterialTableRef.current.state.data];

        for (var i = 0; i < __Items.length; i++) {
          if (JSTypeOperator.IsDefined(__Items[i].tableData)) {
            __Items[i].tableData.showDetailPanel = undefined;
          } else {
            if (JSTypeOperator.IsDefined(__This.props.detailIndex)) {
              __Items[i].tableData = {
                showDetailPanel:
                  __This.props.detailPanel[__This.props.detailIndex].render,
              };
            }
          }
        }

        var __FilteredArray = __Items.filter(
          (__Item) => __Item[__This.props.columnName] == __This.props.selectedId
        );

        if (__FilteredArray.length > 0) {
          __FilteredArray[0].tableData = {
            showDetailPanel:
              __This.props.detailPanel[__This.props.detailIndex].render,
          };
        }

        if (__This.MaterialTableRef.current != null) {
          __This.MaterialTableRef.current.setState({
            data: __Items,
          });
        }
      }
    },
    AsyncLoad: function () {
      TDataTable.BaseObject.AsyncLoad.call(this);
      var __This = this;
      Actions.DataSource_GetMetaData(
        __This.props.datasource,
        function (_Message) {
          CommandIDs.ResultListCommand.RunIfHas(_Message, function (_Data) {
            var __Columns = [];
            _Data.ResultList.map(function (_Item, _Index) {
              if (_Item.Type == "avatar") {
                __Columns.push({
                  field: _Item.FieldName,
                  title: _Item.Title,
                  sorting: !_Item.Calculated,
                  editable: false,
                  readonly: true,
                  render: function (_RowData) {
                    return (
                      <img
                        src={_RowData[_Item.FieldName]}
                        style={{ width: 80, borderRadius: "50%" }}
                      />
                    );
                  },
                  cellStyle: {
                    maxWidth: _Item.Width == 0 ? null : _Item.Width,
                  },
                });
              } else if (_Item.Type == "currency") {
                __Columns.push({
                  field: _Item.FieldName,
                  title: _Item.Title,
                  sorting: !_Item.Calculated,
                  editable: false,
                  readonly: true,
                  render: function (_RowData) {
                    return (
                      <span>
                        {_RowData[_Item.FieldName] +
                          " " +
                          __This.Currency_symbol}
                      </span>
                    );
                  },
                  cellStyle: {
                    maxWidth: _Item.Width == 0 ? null : _Item.Width,
                  },
                  currencySetting: __This.ColumnCurrencySetting,
                });
              } else {
                __Columns.push({
                  cellStyle: {
                    maxWidth: _Item.Width == 0 ? null : _Item.Width,
                  },
                  /*headerStyle: {
                 backgroundColor: '#039be5',
               },*/
                  //cellStyle: { whiteSpace: 'nowrap' },
                  TranslateValue: _Item.TranslateValue,
                  title: _Item.Title,
                  field: _Item.FieldName,
                  type: _Item.Type, //'numeric'
                  hidden: !_Item.Visible,
                  editable: _Item.Editable,
                  removable: _Item.Removable,
                  readonly: _Item.Readonly,
                  sorting: !_Item.Calculated,
                  customSort: (_Item1, _Item2) => {
                    //a.name.length - b.name.length
                    for (var __ItemIn1 in _Item1) {
                      if (typeof __ItemIn1.startsWith("RowNumber")) {
                        for (var __ItemIn2 in _Item2) {
                          if (typeof __ItemIn2.startsWith("RowNumber")) {
                            return _Item1[__ItemIn1] > _Item2[__ItemIn2];
                          }
                        }
                      }
                    }
                    return 0;
                  },
                  editComponent:
                    _Item.EditComponent != null && _Item.EditComponent != ""
                      ? _Item.EditComponent
                      : null,
                  lookup:
                    _Item.LookUpDataSource && _Item.LookUpDataSource != null
                      ? __This.GetLookup(_Item.LookUpDataSource)
                      : null,
                });
              }
            });

            CommandIDs.ResultItemCommand.RunIfHas(_Message, function (_Data2) {
              __This.setState({
                PageSizes: _Data2.Item.PageSizes,
                Columns: __Columns,
              });
              //__This.setState({ Columns: __Columns });
            });
          });
        }
      );

      Actions.DataSource_GetSettings(
        __This.props.datasource,
        function (_Message) {
          CommandIDs.ResultListCommand.RunIfHas(_Message, function (_Data) {
            var __Permissions = {};
            for (var i = 0; i < _Data.ResultList.length; i++) {
              var __CustomActions = [];
              if (_Data.ResultList[i].CanUpdate) {
                __Permissions.onRowUpdate = __This.UpdateData();

                __CustomActions =
                  __This.props.customEdit &&
                  JSTypeOperator.IsFunction(__This.props.customEdit)
                    ? [
                        {
                          icon: EditIcon,
                          tooltip: __This.state.Language.Edit,
                          onClick: (event, rowData) => {
                            __This.props.customEdit(event, rowData);
                          },
                        },
                      ]
                    : [];
              }

              if (_Data.ResultList[i].CanDelete) {
                if (
                  __This.props.onDelete &&
                  JSTypeOperator.IsFunction(__This.props.onDelete)
                ) {
                  __CustomActions = [
                    ...__CustomActions,
                    {
                      icon: DeleteIcon,
                      tooltip: __This.state.Language.Delete,
                      onClick: (_Event, rowData) => {
                        __This.props.onDelete(_Event, rowData);
                      },
                    },
                  ];
                }
              }
            }

            __This.setState({
              Permissions: __Permissions,
              CustomActions: __CustomActions,
            });
          });
        }
      );

      /*Actions.DataSource_Create(this.ObjectType.ObjectName, function (_Message) {

      });


      Actions.DataSource_Delete(this.ObjectType.ObjectName, function (_Message) {

      });*/
    },
    ReadData: function () {
      var __This = this;
      return (_Query) =>
        new Promise((_Resolve, _Reject) => {
          Actions.DataSource_Read(
            __This.props.datasource,
            _Query.pageSize,
            _Query.page,
            _Query.orderBy ? _Query.orderBy.field : "",
            _Query.orderDirection ? _Query.orderDirection : "",
            _Query.filters,
            _Query.search,
            __This.props.options ? __This.props.options : null,
            function (_Message) {
              CommandIDs.ResultListCommand.RunIfHas(_Message, function (_Data) {
                /// Burasını state içindeki column bölümünden kullanılamadı, State nesnesinin içindeki Column değişkeni arasıra doldurulamıyor
                /// React kaynaklı bir hata var.
                /// Aşağıdaki commentli bölüm columndan alarak işletiyor.
                Actions.DataSource_GetMetaData(
                  __This.props.datasource,
                  function (_Message2) {
                    CommandIDs.ResultListCommand.RunIfHas(
                      _Message2,
                      function (_Data2) {
                        for (var i = 0; i < _Data.ResultList.length; i++) {
                          for (var __Item in _Data.ResultList[i]) {
                            for (var j = 0; j < _Data2.ResultList.length; j++) {
                              var __ColumItem = _Data2.ResultList[j];
                              if (
                                __ColumItem.FieldName == __Item &&
                                __ColumItem.TranslateValue &&
                                __ColumItem.Type == "numeric"
                              ) {
                                _Data.ResultList[i][__ColumItem.FieldName] =
                                  GenericWebGraph.GetDayNameByID(
                                    _Data.ResultList[i][__ColumItem.FieldName],
                                    "dddd"
                                  );
                              }
                            }
                          }
                        }

                        setTimeout(function () {
                          __This.Reread();
                        }, 1000);

                        _Resolve({
                          data: _Data.ResultList,
                          page: _Data.Page,
                          totalCount: _Data.Total,
                        });
                      }
                    );
                  }
                );

                /*
              for (var i = 0; i < _Data.ResultList.length; i++)
              {
                for (var __Item in _Data.ResultList[i])
                {
                  for (var j = 0; j < __This.state.Columns.length;j++)
                  {
                    if (__This.state.Columns[j].field == __Item && __This.state.Columns[j].TranslateValue && __This.state.Columns[j].type == "numeric")
                    {
                      _Data.ResultList[i][__This.state.Columns[j].field] = GenericWebGraph.GetDayNameByID(_Data.ResultList[i][__This.state.Columns[j].field], "dddd");
                    }
                  }
                }
              }

              _Resolve({
                data: _Data.ResultList,
                page: _Data.Page,
                totalCount: _Data.Total,
              })*/
              });

              CommandIDs.ResultListCommand.RunIfNotHas(
                _Message,
                function (_Data) {
                  _Resolve({
                    data: [],
                    page: 0,
                    totalCount: 0,
                  });
                }
              );
            }
          );
        });
    },
    UpdateData: function () {
      var __This = this;
      return (_NewData, _OldData) =>
        new Promise((_Resolve, _Reject) => {
          Actions.DataSource_Update(
            __This.props.datasource,
            { NewData: _NewData, OldData: _OldData },
            function (_Message) {
              CommandIDs.SuccessResultCommand.RunIfHas(
                _Message,
                function (_Data) {
                  _Resolve();
                }
              );

              CommandIDs.SuccessResultCommand.RunIfNotHas(
                _Message,
                function (_Data) {
                  _Reject();
                }
              );
            }
          );
        });
    },
    Destroy: function () {
      TDataTable.BaseObject.Destroy.call(this);
    },
    HandleOnClick: function () {},
    HandleClickModal: function (_RowData) {
      var __TempData = [];
      this.state.Columns.map((column) => {
        __TempData.push(_RowData[column.field]);
      });
      window.App.DataTableModal.HandleClickOpen({
        DataList: __TempData,
        ColumnList: this.state.Columns,
      });
    },
    render: function () {
      var __This = this;
      const { classes } = this.props;
      return (
        <div>
          <MaterialTable
            tableRef={this.MaterialTableRef}
            components={{...this.props.components, ...{
              Toolbar: (props) => (
                <div style={{ width: "100%" }}>
                  <MTableToolbar {...props} />
                  {this.props.inneraction}
                </div>
              ),
            }}}
            options={{
              search: true,
              debounceInterval: 1500,
              filtering: false,
              pageSizeOptions: this.state.PageSizes,
              searchFieldStyle: {
                fontSize: "14px",
              },
            }}
            actions={this.state.CustomActions.concat(
              this.props.actions ? this.props.actions : []
            )}
            localization={{
              pagination: {
                labelDisplayedRows: this.state.Language.CurrentPaging,
                labelRowsSelect: this.state.Language.Row,
              },
              toolbar: {
                nRowsSelected: this.state.Language.LineSelected,
                searchPlaceholder: this.state.Language.Search,
              },

              header: {
                actions: this.state.Language.Actions,
              },
              body: {
                emptyDataSourceMessage: this.state.Language.RecordNotFound,
                filterRow: {
                  filterTooltip: this.state.Language.Filter,
                },
              },
            }}
            title={
              <Typography className={classes.title}>
                {this.props.title}
              </Typography>
            }
            columns={
              this.state.IsXs
                ? this.state.Columns.filter(function (_Item) {
                    return !_Item.hidden;
                  }).slice(
                    0,
                    this.props.showXsColumnCount
                      ? this.props.showXsColumnCount
                      : 2
                  )
                : this.state.Columns
            }
            data={this.ReadData()}
            editable={
              this.props.customEdit &&
              JSTypeOperator.IsFunction(this.props.customEdit)
                ? false
                : this.state.Permissions
            }
            detailPanel={this.props.detailPanel}
            onRowClick={
              this.state.IsXs
                ? (event, rowData) => {
                    if (JSTypeOperator.IsFunction(__This.props.onRowClick)) {
                      if (__This.props.onRowClick(event, rowData)) {
                        __This.HandleClickModal(rowData);
                      }
                    } else {
                      __This.HandleClickModal(rowData);
                    }
                  }
                : null
            }
          />
        </div>
      );
    },
  },
  {}
);

export default withStyles(MaterialTableStyles)(TDataTable);
