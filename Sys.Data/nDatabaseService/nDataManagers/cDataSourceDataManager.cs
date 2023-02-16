using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Base.Data.nDatabaseService;
using Sys.Data.nDatabaseService;
using Sys.Data.nDatabaseService.nSystemEntities;
using System.Data;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Sys.Boundary.nDefaultValueTypes;

namespace Sys.Data.nDataService.nDataManagers
{
    public class cDataSourceDataManager : cBaseDataManager
    {
        public cDataSourceDataManager(cDataServiceContext _CoreServiceContext, IDataService _DataService, IFileDateService _FileDataService)
          : base(_CoreServiceContext, _DataService, _FileDataService)
        {
        }

        public List<cDataSourceColumnEntity> GetDataSourceColumnsByRoleAndDataSourceID(List<cRoleEntity> _RoleList, DataSourceIDs _DataSourceID)
        {

            var __Temp = cDataSourceColumnEntity.Get(__Item => __Item.DataSourceID == _DataSourceID.ID && __Item.Roles.Any()).ToList();

            return cDataSourceColumnEntity.Get(__Item => __Item.DataSourceID == _DataSourceID.ID && __Item.Roles.Any(__Item => _RoleList.Contains(__Item))).ToList();

            /*List<cDataSourceColumnEntity> __Result = cDataSourceColumnEntity.Get(
              __Item => __Item.DataSourceCode == _DataSourceID.Code && __Item.Roles.Any(__Item => __Item.ID == _RoleList.Average(__Item => __Item.ID))
          ).ToList();*/

            /*IQueryable<cRoleDataSourceColumnMapEntity> __Query = null;
            foreach (cRoleEntity __RoleEntity in _RoleList)
            {
                if (__Query == null)
                {
                    __Query = cRoleDataSourceColumnMapEntity.Get(__Item => __Item.DataSourceColumn.DataSourceCode == _DataSourceID.Code)
                    .Intersect(__RoleEntity.RoleDataSourceColumnMaps);
                }
                else
                {
                    __Query = cRoleDataSourceColumnMapEntity.Get(__Item => __Item.DataSourceColumn.DataSourceCode == _DataSourceID.Code)
                    .Intersect(__RoleEntity.RoleDataSourceColumnMaps)
                    .Intersect(__Query);
                }
            }


            List<cDataSourceColumnEntity> __Result = cDataSourceColumnEntity.Get(__Item => __Item.DataSourceCode == _DataSourceID.Code)
                .Intersect(__Query.Select(__Item => __Item.DataSourceColumn)).ToList();*/

            //return __Result;
        }

        public List<cDataSourceColumnEntity> GetDataSourceColumns (DataSourceIDs _DataSourceID)
        {
            return  cDataSourceColumnEntity.Get(__Item => __Item.DataSourceCode == _DataSourceID.Code).ToList();
        }

        public cDataSourceColumnEntity GetDataSourceColumnsByRoleAndDataSourceID(cRoleEntity _Role, DataSourceIDs _DataSourceID, cDataSourceColumnEntity _Column)
        {
            List<cDataSourceColumnEntity> __DataSourceColumnEntity = cDataSourceColumnEntity.Get(
                __Item => __Item.DataSourceCode == _DataSourceID.Code && __Item.ColumnName == _Column.ColumnName && __Item.Roles.Any(__Item => __Item.ID == _Role.ID)
            ).ToList();

            return __DataSourceColumnEntity.FirstOrDefault();
        }

        public bool IsDataSourceColumnExistsInRole(cRoleEntity _Role, DataSourceIDs _DataSourceID, cDataSourceColumnEntity _Column)
        {
            return GetDataSourceColumnsByRoleAndDataSourceID(_Role, _DataSourceID, _Column) != null;
        }

        public cDataSourceColumnEntity GetColumnByColumnNameInDataSource(DataSourceIDs _DataSourceID, string _ColumnName)
        {
            return cDataSourceColumnEntity.Get(__Item => __Item.ColumnName == _ColumnName && __Item.DataSourceCode == _DataSourceID.Code).FirstOrDefault();
        }

        public void AddColumnToDataSource(DataSourceIDs _DataSourceID, string _ColumnName)
        {
            if (GetColumnByColumnNameInDataSource(_DataSourceID, _ColumnName) == null)
            {
                cDataSourceColumnEntity __DataSourceColumnEntity = cDataSourceColumnEntity.Add(new cDataSourceColumnEntity()
                {
                    ColumnName = _ColumnName,
                    DataSourceCode = _DataSourceID.Code,
                    DataSourceID = _DataSourceID.ID
                });
                
                __DataSourceColumnEntity.Save();
            }
        }

        public void DeleteColumnFromDataSourceAndRoles(List<cDataSourceColumnEntity> _Columns)
        {
            foreach (cDataSourceColumnEntity __Column in _Columns)
            {
                DeleteColumnFromDataSourceAndRoles(__Column);
            }
        }

        public void DeleteColumnFromDataSourceAndRoles(cDataSourceColumnEntity _Column)
        {
            cDataSourceColumnEntity __Colum = cDataSourceColumnEntity.Get(__Item => __Item.ID == _Column.ID).Include(__Item => __Item.Roles).FirstOrDefault();
            __Colum.Roles.RemoveAll();

            _Column.Delete();
        }

        public bool IsDataSourceExistsInRole(cRoleEntity _Role, DataSourceIDs _DataSourceID)
        {
            return GetDataSourceInRoleByDataSourceID(_Role, _DataSourceID) != null;
        }

        public List<cDataSourcePermissionEntity> GetDataSourceInRoleByDataSourceID(List<cRoleEntity> _RoleList, DataSourceIDs _DataSourceIDs)
        {
            return cDataSourcePermissionEntity.Get(__Item => __Item.DataSourceID == _DataSourceIDs.ID && __Item.Roles.Any(__Item => _RoleList.Contains(__Item))).ToList();

            /*List<cDataSourcePermissionEntity> __Result = new List<cDataSourcePermissionEntity>();
            foreach (cRoleEntity __Item in _RoleList)
            {
                cDataSourcePermissionEntity __Permission = GetDataSourceInRoleByDataSourceID(__Item, _DataSourceIDs);
                if (__Permission != null)
                {
                    __Result.Add(__Permission);
                }
            }
            return __Result;*/
        }

        public cDataSourcePermissionEntity GetDataSourceInRoleByDataSourceID(cRoleEntity _Role, DataSourceIDs _DataSourceIDs)
        {
            return cDataSourcePermissionEntity.Get(__Item => __Item.DataSourceID == _DataSourceIDs.ID && __Item.Roles.Any(__Item => __Item.ID == _Role.ID)).FirstOrDefault();
        }


        public void AddDataSourceColumnToRole(cRoleEntity _Role, DataSourceIDs _DataSourceID, cDataSourceColumnEntity _Column)
        {
            hayrii
            /*if (!IsDataSourceColumnExistsInRole(_Role, _DataSourceID, _Column))
            {
                _Role.Da Add(new cRoleDataSourceColumnMapEntity()
                {
                    Role = _Role,
                    DataSourceColumn = _Column
                });
                _Role.Save();
            }*/
        }



        public void AddDataSourceColumnToRole(cRoleEntity _Role, DataSourceIDs _DataSourceID, List<cDataSourceColumnEntity> _Columns)
        {
            foreach (var _Column in _Columns)
            {
                AddDataSourceColumnToRole(_Role, _DataSourceID, _Column);
            }
        }

        public void AddAllDatasourceColumnToRole(cRoleEntity _Role, DataSourceIDs _DataSourceID)
        {
            List<cDataSourceColumnEntity> __Columns = GetDataSourceColumns(_DataSourceID);
            AddDataSourceColumnToRole(_Role, _DataSourceID, __Columns);
        }

        public void AddDataSourceToRole(cRoleEntity _Role, DataSourceIDs _DataSourceID, bool _CanCreate, bool _CanRead, bool _CanUpdate, bool _CanDelete)
        {
            if (!IsDataSourceExistsInRole(_Role, _DataSourceID))
            {

                cDataSourcePermissionEntity __DataSourcePermissionEntity = cDataSourcePermissionEntity.Add(new cDataSourcePermissionEntity()
                {
                    CanCreate = _CanCreate,
                    CanRead = _CanRead,
                    CanUpdate = _CanUpdate,
                    CanDelete = _CanDelete,
                    DataSourceID = _DataSourceID.ID,
                    DataSourceCode = _DataSourceID.Code
                });
                
                __DataSourcePermissionEntity.Save();

                _Role.DataSourcePermissions.Add(__DataSourcePermissionEntity);

                _Role.Save();
            }
            else
            {
                cDataSourcePermissionEntity __DataSourcePermissionEntity = GetDataSourceInRoleByDataSourceID(_Role, _DataSourceID);

                __DataSourcePermissionEntity.CanCreate = _CanCreate;
                __DataSourcePermissionEntity.CanRead = _CanRead;
                __DataSourcePermissionEntity.CanUpdate = _CanUpdate;
                __DataSourcePermissionEntity.CanDelete = _CanDelete;
                __DataSourcePermissionEntity.DataSourceID = _DataSourceID.ID;
                __DataSourcePermissionEntity.DataSourceCode = _DataSourceID.Code;
                __DataSourcePermissionEntity.Save();
            }
        }
    }
}
