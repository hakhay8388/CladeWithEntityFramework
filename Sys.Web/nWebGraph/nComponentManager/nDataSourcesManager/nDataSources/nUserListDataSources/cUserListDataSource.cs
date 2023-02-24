using Base.Data.nDatabaseService;
using Bootstrapper.Core.nApplication;
using Newtonsoft.Json.Linq;
using Sys.Boundary.nData;
using Sys.Boundary.nDefaultValueTypes;
using Sys.Data.nDatabaseService.nSystemEntities;
using Sys.Data.nDataService.nDataManagers;
using Sys.Web.Controllers;
using Sys.Web.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nDataSourceFieldTypes.nValueTypes;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_ReadCommand;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_UpdateCommand;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace Sys.Web.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nUserListDataSources
{
    public class cUserListDataSource : cBaseListDataSource<cUserEntity, cDataSourceReadEmptyOptions>
    {
        cUserEntity m_UserAlias = null;
        public cUserDataManager UserDataManager { get; set; }
        public cRoleDataManager RoleDataManager { get; set; }

        public cUserListDataSource(
             cApp _App, cBaseWebGraph _WebGraph
            , IDataService _DataService
            , cDataSourceDataManager _DataSourceDataManager
            , cRoleDataManager _RoleDataManager
            , cUserDataManager _UserDataManager
        )
            : base(DataSourceIDs.UserList, _App, _WebGraph,  _DataService, _DataSourceDataManager)
        {
            UserDataManager = _UserDataManager;
            RoleDataManager = _RoleDataManager;
        }

        public override Dictionary<string, cBaseDataSourceObject> GetFieldTypes()
        {
            Dictionary<string, cBaseDataSourceObject> __Result = new Dictionary<string, cBaseDataSourceObject>();

            //__Result.Add("TestSelect", CreateDataSourceObject("TestSelect", false, ColumnTypeIDs.String));

            __Result.Add("ID", CreateDataSourceObject("ID", false, ColumnTypeIDs.Numeric, _OrderFromLeft: 3, _Visible: false));
            __Result.Add("Name", CreateDataSourceObject("Name", false, ColumnTypeIDs.String, _OrderFromLeft: 4));
            __Result.Add("Surname", CreateDataSourceObject("Surname", false, ColumnTypeIDs.String, _OrderFromLeft: 5));
            __Result.Add("Email", CreateDataSourceObject("Email", false, ColumnTypeIDs.String, _OrderFromLeft: 6));
            __Result.Add("Language", CreateDataSourceObject("Language", false, ColumnTypeIDs.String, _OrderFromLeft: 7));
            __Result.Add("CreateDate", CreateDataSourceObject("CreateDate", false, ColumnTypeIDs.Datetime, _OrderFromLeft: 1));
            __Result.Add("UpdateDate", CreateDataSourceObject("UpdateDate", false, ColumnTypeIDs.Datetime, _OrderFromLeft: 2));
            __Result.Add("State", CreateDataSourceObject("State", false, ColumnTypeIDs.Numeric, "StateText", _OrderFromLeft: 8, _Visible: true, _Editable: ColumnEditableIDs.Always));
            __Result.Add("RoleCode", CreateDataSourceObject("RoleID", false, ColumnTypeIDs.Numeric, "RoleCode", _OrderFromLeft: 9, _Visible: true));

            return __Result;

        }


        public override IQueryable<cUserEntity> ReadData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_ReadCommandData _ReceivedData, cDataSourceReadEmptyOptions _Options)
        {
            if (_Controller.ClientSession.IsLogined)
            {
                try
                {
                    if (_Controller.ClientSession.User.Roles.Find(__Item => __Item.Code == RoleIDs.Admin.Code) != null)
                    {
                        /*return (cQuery<cUserEntity>)__DataService.Database.Query<cUserEntity>(() => m_UserAlias,
                            UserDataManager.GetAllUserListQuery()
                         );*/
                        return cUserEntity.Get();
                    }
                    else
                    {
                        WebGraph.SysActionGraph.NoPermissionAction.Action(_Controller);
                    }
                }
                catch (Exception _Ex)
                {
					WebGraph.ErrorMessageManager.ErrorAction(_Ex, _Controller, _Controller.GetWordValue("Error"), _Controller.GetWordValue("UnknownError"));
				}
            }
            else
            {
                WebGraph.SysActionGraph.ReinitAction.Action(_Controller);
            }
            return null;
        }

        public override Action<dynamic> ToDynamicAction(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_ReadCommandData _ReceivedData)
        {
            return new Action<dynamic>(__Item =>
            {
                __Item.CreateDate = ((DateTime)__Item.CreateDate).ToString("dd.MM.yyyy HH:mm");
                __Item.UpdateDate = ((DateTime)__Item.UpdateDate).ToString("dd.MM.yyyy HH:mm");
                RoleIDs __RoleId = RoleIDs.GetByID((int)__Item.RoleID, null);
                __Item.RoleID = App.Handlers.LanguageHandler.GetWordValue(_Controller.ClientSession.Language, (__RoleId.Name));


            });
        }

        public override IQueryable<cUserEntity> SelectedColumns(IQueryable<cUserEntity> _Query)
        {
            return _Query;
        }

        public override ESortDirectionTypes DefaultDirection()
        {
            return ESortDirectionTypes.Descending;
        }

        public override string DefaultDirectionColumn()
        {
            return "UpdateDate";
        }
    }
}
