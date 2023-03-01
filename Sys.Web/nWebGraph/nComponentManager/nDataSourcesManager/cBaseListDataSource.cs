using Base.Data.nConfiguration;
using Base.Data.nDatabaseService;
using Base.Data.nDatabaseService.nDatabase;
using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json.Linq;
using Npgsql;
using Npgsql.Internal;
using OfficeOpenXml.Style;
using Sys.Boundary.nData;
using Sys.Boundary.nDefaultValueTypes;
using Sys.Data.nDatabaseService.nSystemEntities;
using Sys.Data.nDataService.nDataManagers;
using Sys.Web.Controllers;
using Sys.Web.nWebGraph;
using Sys.Web.nWebGraph.nComponentManager.nDataSourcesManager;
using Sys.Web.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources;
using Sys.Web.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nDataSourceFieldTypes.nValueTypes;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nDataSourceMetadataResultAction;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nDataSourcePermissionResultAction;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nResultItemAction;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nResultListAction;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_CreateCommand;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_DeleteCommand;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_GetMetaAndSettingsCommand;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_ReadCommand;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_UpdateCommand;
using Sys.Web.nWebGraph.nWebApiGraph.nListenerGraph;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using IProperty = Microsoft.EntityFrameworkCore.Metadata.IProperty;

namespace Sys.Web.nWebGraph.nComponentManager.nDataSourcesManager
{
    public abstract class cBaseListDataSource<TEntity, TBaseDataSourceReadOptions> : cCoreObject, IDataSource
        where TBaseDataSourceReadOptions : class
        where TEntity : cBaseEntityType
    {
        public string QueryAsName { get; set; }
        public DataSourceIDs DataSourceID { get; set; }
        public cDataSourceManager DataSourceManager { get; set; }
        public IDataService DataService { get; set; }

        public cDataSourceDataManager DataSourceDataManager { get; set; }

        protected TEntity m_MainAlias = null;

        public cBaseWebGraph WebGraph { get; set; }
        public cBaseListDataSource(
            DataSourceIDs _DataSourceID
            , cApp _App, cBaseWebGraph _WebGraph
            , IDataService _DataService
            , cDataSourceDataManager _DataSourceDataManager
            )
            : base(_App)
        {
            QueryAsName = "DataSource_Query";
            DataSourceID = _DataSourceID;
            DataService = _DataService;
            WebGraph = _WebGraph;
            DataSourceDataManager = _DataSourceDataManager;
        }

        public cBaseDataSourceObject CreateDataSourceObject(
            string _FieldName,
            bool _Calculated = false,
            ColumnTypeIDs _Type = null,
            string _Title = null,
            int _OrderFromLeft = 1,
            bool _Visible = true,
            bool _ElasticSearch = false,
            ColumnEditableIDs _Editable = null,
            bool _Removable = false,
            bool _Readonly = false,
            bool _TranslateValue = false,
            int _Width = 0
        )
        {
            cBaseDataSourceObject __Props = new cBaseDataSourceObject()
            {
                Calculated = _Calculated,
                Editable = (_Editable == null ? ColumnEditableIDs.Never.Code : _Editable.Code),
                ElasticSearch = _ElasticSearch,
                Readonly = _Readonly,
                Removable = _Removable,
                TranslateValue = _TranslateValue,
                Visible = _Visible,
                Title = _Title,
                FieldName = _FieldName,
                Type = (_Type == null ? ColumnTypeIDs.String : _Type),
                OrderFromLeft = _OrderFromLeft,
                Width = _Width
            };
            return __Props;
        }

        public List<cBaseDataSourceObject> GetFieldList()
        {
            List<cBaseDataSourceObject> __Result = new List<cBaseDataSourceObject>();

            List<string> __Fields = GetFieldNames();

            Dictionary<string, cBaseDataSourceObject> __FieldTypes = GetFieldTypes();

            /* for (int i = 0; i < __Fields.Count; i++)
             {
                 if (__FieldTypes.ContainsKey(__Fields[i]))
                 {
                     __Result.Add(__FieldTypes[__Fields[i]]);
                 }
                 else
                 {
                     __Result.Add(CreateDataSourceObject(__Fields[i], false, ColumnTypeIDs.String, null,  i, true, false, ColumnEditableIDs.Never, false, true, null, false));
                 }
             }*/

            foreach (var __Item in __FieldTypes)
            {
                if (!__Result.Contains(__Item.Value))
                {
                    __Result.Add(__Item.Value);
                }
            }

            return __Result;
        }

        public bool IsFieldExist(string _FieldName)
        {
            List<cBaseDataSourceObject> __Result = GetFieldList();
            return __Result.Find(__Item => __Item.FieldName == _FieldName) != null;
        }


        public void SynchronizeColumnNames()
        {
            List<cDataSourceColumnEntity> __ColumnList = DataSourceDataManager.GetDataSourceColumns(DataSourceID);
            List<cDataSourceColumnEntity> __UnExistList = new List<cDataSourceColumnEntity>();

            List<cBaseDataSourceObject> __FieldNames = GetFieldList();

            foreach (var __Column in __ColumnList)
            {
                if (__FieldNames.Find(__Item =>
                    (DataSourceID.Code + "." + __Item.FieldName) == __Column.ColumnName && !__Item.Calculated) == null)
                {
                    __UnExistList.Add(__Column);
                }
            }

            DataSourceDataManager.DeleteColumnFromDataSourceAndRoles(__UnExistList);
            foreach (var __Item in __FieldNames)
            {
                if (!__Item.Calculated)
                {
                    DataSourceDataManager.AddColumnToDataSource(DataSourceID,
                        (DataSourceID.Code + "." + __Item.FieldName));
                }
            }
        }

        public List<string> GetFieldNames()
        {
            IEntityType __EntityType = DataService.GetCoreEFDatabaseContext().Model.FindEntityTypes(typeof(TEntity)).FirstOrDefault();
            List<IProperty> __Properties = __EntityType.GetProperties().ToList();

            List<string> __FieldList = __Properties.Select(__Item => __Item.Name).ToList();
            return __FieldList;
        }



        public List<cBaseDataSourceObject> GetFieldListForMyPermission(IController _Controller)
        {
            List<cDataSourceColumnEntity> __Colums = DataSourceDataManager.GetDataSourceColumnsByRoleAndDataSourceID(_Controller.ClientSession.User.Roles.ToList(), DataSourceID);
            List<cBaseDataSourceObject> __FullFieldList = GetFieldList();

            List<cBaseDataSourceObject> __Result = new List<cBaseDataSourceObject>();

            foreach (cDataSourceColumnEntity __ColumItem in __Colums)
            {
                cBaseDataSourceObject __Item = __FullFieldList.Find(__Item => (DataSourceID.Code + "." + __Item.FieldName) == __ColumItem.ColumnName);
                if (__Item != null)
                {
                    __Result.Add(__Item);
                }
            }

            __FullFieldList.ForEach(__Item =>
            {
                if (__Item.Calculated)
                {
                    __Result.Add(__Item);
                }
            });
            __Result = __Result.OrderBy(_Item => _Item.OrderFromLeft).ToList();
            return __Result;
        }


        public abstract Dictionary<string, cBaseDataSourceObject> GetFieldTypes();
        public abstract string DefaultDirectionColumn();
        public abstract ESortDirectionTypes DefaultDirection();
        public abstract IQueryable<TEntity> ReadData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_ReadCommandData _ReceivedData, TBaseDataSourceReadOptions _Options);

        public List<int> GetPageSizes()
        {
            List<int> __PageSizes = new List<int>();
            __PageSizes.Add(5);
            __PageSizes.Add(10);
            __PageSizes.Add(20);
            return __PageSizes;
        }
        public int GetDefaultPageSize()
        {
            return 5;
        }

        public void TranslateObject(IController _Controller, List<dynamic> _ItemList, List<cBaseDataSourceObject> _FieldList)
        {

            foreach (var __Item in _ItemList)
            {
                foreach (var __FieldItem in _FieldList)
                {
                    if (__FieldItem.TranslateValue && __FieldItem.Type.Code != ColumnTypeIDs.Numeric.Code)
                    {
                        var __DictionaryItem = (IDictionary<string, object>)__Item;
                        object __Value = __DictionaryItem[__FieldItem.FieldName];
                        __DictionaryItem[__FieldItem.FieldName] = _Controller.GetWordValue(__Value.ToString());
                    }
                }
            }
        }

        public virtual Action<dynamic> ToDynamicAction(cListenerEvent _ListenerEvent, IController _Controller,
            cDataSource_ReadCommandData _ReceivedData)
        {
            return null;
        }

        public virtual string SelectCount(IQueryable<TEntity> _Query)
        {
            return "Count(*)";
        }

        public virtual string SelectedColumns(IController _Controller, IQueryable<TEntity> _Query)
        {
            List<cBaseDataSourceObject> __FieldList = GetFieldListForMyPermission(_Controller);

            __FieldList.Sort((_Item1, _Item2) =>
            {
                return _Item1.OrderFromLeft.CompareTo(_Item2.OrderFromLeft);
            });

            string __SelectionResult = "";

            for (int i = 0; i < __FieldList.Count; i++)
            {
                if (!__SelectionResult.IsNullOrEmpty()) __SelectionResult += ", ";
                if (!__FieldList[i].Calculated)
                {
                    __SelectionResult += QueryAsName + ".\"" + __FieldList[i].FieldName + "\"";
                }
            }
            
            return __SelectionResult;
        }

        public string CreateQuerySql(IQueryable _Query, string _Selection, string _Condition, string _Order, string _Pagination)
        {
            string __Sql = $"SELECT {_Selection} FROM ({_Query.ToQueryString()}) AS {QueryAsName}" +
                                    $"{(_Condition.IsNullOrEmpty() ? "" : " " + _Condition)}" +
                                    $"{(_Order.IsNullOrEmpty() ? "" : " " + _Order)}" +
                                    $"{(_Pagination.IsNullOrEmpty() ? "" : " " + _Pagination)}";
            return __Sql;
        }

        public void ReceiveDataSource_ReadData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_ReadCommandData _ReceivedData)
        {
            if (_Controller.ClientSession.IsLogined)
            {
                TBaseDataSourceReadOptions __Option = null;
                if (_ReceivedData.Options != null)
                {
                    __Option = ((JObject)_ReceivedData.Options).ToObject<TBaseDataSourceReadOptions>();
                }
                else
                {
                    __Option = (TBaseDataSourceReadOptions)typeof(TBaseDataSourceReadOptions).CreateInstance();
                }


                List<cDataSourcePermissionEntity> __Permissions = DataSourceDataManager.GetDataSourceInRoleByDataSourceID(_Controller.ClientSession.User.Roles.ToList(), DataSourceID);
                if (__Permissions.Find(__Item => __Item.CanRead) != null)
                {
                    IQueryable<TEntity> __Query = ReadData(_ListenerEvent, _Controller, _ReceivedData, __Option);

                    string __OrderByField = _ReceivedData.OrderByField.IsNullOrEmpty() ? DefaultDirectionColumn() : _ReceivedData.OrderByField;
                    string __OrderDirection = _ReceivedData.OrderDirection.IsNullOrEmpty() ? ESortDirectionTypes.GetForDB(DefaultDirection()) : _ReceivedData.OrderDirection;


                    int __Count = 0;
                    DataTable __Result = null;

                    DbConnection __Connection = DataService.GetCoreEFDatabaseContext().Database.GetDbConnection();

                    if (__Connection.State != ConnectionState.Open) __Connection.Open();


                    NpgsqlConnection __NpgsqlConnection = __Connection as NpgsqlConnection;
                    if (__NpgsqlConnection == null)
                    {
                        throw new InvalidOperationException("Connection must be a NpgsqlConnection");
                    }

                    string __Sql = CreateQuerySql(__Query, SelectCount(__Query), null, null, null);
                    NpgsqlCommand __Command = new NpgsqlCommand(__Sql, __NpgsqlConnection);
                    __Count = Convert.ToInt32(__Command.ExecuteScalar());


                    DataSet __DataSet = new DataSet();
                    NpgsqlDataAdapter __Adapter = new NpgsqlDataAdapter();


                    string __Pagination = "LIMIT @PageSize OFFSET(@PageIndex) * @PageSize";
                    string __OrderBy = "";

                    if (IsFieldExist(__OrderByField))
                    {
                        __OrderBy = $"ORDER BY {QueryAsName}.\"{(__OrderByField)}\" {(__OrderDirection == "asc" ? "ASC" : "DESC")}";
                    }
                    
                    __Sql = CreateQuerySql(__Query, SelectedColumns(_Controller, __Query), null, __OrderBy, __Pagination);
                    __Command = new NpgsqlCommand(__Sql, __NpgsqlConnection);
                    __Command.Parameters.Add(new NpgsqlParameter("@PageIndex", _ReceivedData.Page < 0 ? 0 : _ReceivedData.Page));
                    __Command.Parameters.Add(new NpgsqlParameter("@PageSize", _ReceivedData.PageSize > 100 ? 100 : _ReceivedData.PageSize));


                    __Adapter.SelectCommand = __Command;

                    __Adapter.Fill(__DataSet);

                    if (__DataSet.Tables.Count == 1)
                    {
                        __Result = __DataSet.Tables[0];
                    }

                    List<dynamic> __ResultList = __Result.ToDynamicObjectList(ToDynamicAction(_ListenerEvent, _Controller, _ReceivedData));

                    List<cBaseDataSourceObject> __FieldList = GetFieldListForMyPermission(_Controller);

                    TranslateObject(_Controller, __ResultList, __FieldList);

                    WebGraph.SysActionGraph.ResultListAction.Action(_Controller,
                        new cResultListProps()
                        {
                            ResultList = __ResultList,
                            Total = __Count,
                            Page = _ReceivedData.Page
                        });

                }
                else
                {
                    WebGraph.SysActionGraph.NoPermissionAction.Action(_Controller);
                }
            }
            else
            {
                WebGraph.SysActionGraph.ReinitAction.Action(_Controller);
            }
        }

        public void ReceiveDataSource_CreateData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_CreateCommandData _ReceivedData)
        {
            throw new NotImplementedException();
        }

        public void ReceiveDataSource_UpdateData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_UpdateCommandData _ReceivedData)
        {
            throw new NotImplementedException();
        }

        public void ReceiveDataSource_DeleteData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_DeleteCommandData _ReceivedData)
        {
            throw new NotImplementedException();
        }

        public void ReceiveDataSource_GetMetaAndSettingsData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_GetMetaAndSettingsCommandData _ReceivedData)
        {
            if (_Controller.ClientSession.IsLogined)
            {
                List<cDataSourcePermissionEntity> __Permissions = DataSourceDataManager.GetDataSourceInRoleByDataSourceID(_Controller.ClientSession.User.Roles.ToList(), DataSourceID);
                WebGraph.SysActionGraph.DataSourcePermissionResultAction.Action(_Controller, new cDataSourcePermissionResultProps() { ResultList = __Permissions });



                List<cBaseDataSourceObject> __Result = GetFieldListForMyPermission(_Controller);

                __Result.ForEach(__Item =>
                {
                    /*
                    ----Visible False Column İsmini setlenecek
                    */
                    __Item.Title = _Controller.GetWordValue((__Item.Title.IsNullOrEmpty() ? __Item.FieldName : __Item.Title));
                });
                __Result = __Result.OrderBy(__Item => __Item.OrderFromLeft).ToList();

                WebGraph.SysActionGraph.DataSourceMetadataResultAction.Action(_Controller, new cDataSourceMetadataResultProps() { ResultList = __Result });

                List<int> __PageSizes = GetPageSizes();
                WebGraph.SysActionGraph.ResultItemAction.Action(_Controller, new cResultItemProps() { Item = new { PageSizes = __PageSizes } });

            }
            else
            {
                WebGraph.SysActionGraph.ReinitAction.Action(_Controller);
            }
        }
    }
}
