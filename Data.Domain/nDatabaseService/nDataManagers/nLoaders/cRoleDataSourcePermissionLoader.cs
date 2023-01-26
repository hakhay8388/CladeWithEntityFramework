
using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Data.Domain.nDefaultValueTypes;
using Bootstrapper.Core.nApplication;
using Data.Domain.nDatabaseService;
using Data.Domain.nDataService.nDataManagers.nLoaders.nLoaderIDs;
using Data.Domain.nDatabaseService.nSystemEntities;

namespace Data.Domain.nDataService.nDataManagers.nLoaders
{
	public class cRoleDataSourcePermissionCheckSum
	{
		public DataSourceIDs DataSourceID { get; set; }
		public bool CanCreate { get; set; }
		public bool CanRead { get; set; }
		public bool CanUpdate { get; set; }
		public bool CanDelete { get; set; }

		public cRoleDataSourcePermissionCheckSum(DataSourceIDs _DataSourceID, bool _CanCreate, bool _CanRead, bool _CanUpdate, bool _CanDelete)
		{
			DataSourceID = _DataSourceID;
			CanCreate = _CanCreate;
			CanRead = _CanRead;
			CanUpdate = _CanUpdate;
			CanDelete = _CanDelete;
		}

	}

	public class cRoleDataSourcePermissionLoader : cBaseDataLoader
    {
        public cRoleDataManager RoleDataManager { get; set; }
        public cDataSourceDataManager DataSourceDataManager { get; set; }

        public cRoleDataSourcePermissionLoader(cApp _App, cDataService _DataService, IFileDateService _FileDataService, cChecksumDataManager _ChecksumDataManager
			, cRoleDataManager _RoleDataManager
            , cMenuDataManager _MenuDataManager
            , cPageDataManager _PageDataManager
            , cDataSourceDataManager _DataSourceDataManager
            )
          : base(_App, LoaderIDs.RoleDataSourcePermissionLoader, _DataService, _FileDataService, _ChecksumDataManager)
        {
            RoleDataManager = _RoleDataManager;
            DataSourceDataManager = _DataSourceDataManager;
        }

        public void Init()
        {
            AddAdminPages();
            AddUserPages();
			AddDeveloperPages();
        }


        public void AddAdminPages()
        { 
			List<cRoleDataSourcePermissionCheckSum> __RoleDataSourcePermissionCheckSumList = new List<cRoleDataSourcePermissionCheckSum>();

			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.UserList_CustomQuery, true, true, true, false)); 

			cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code + "_Admin");
			string __TotalString = GetTotalString<cRoleDataSourcePermissionCheckSum>(__RoleDataSourcePermissionCheckSumList);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				cRoleEntity __Role = RoleDataManager.GetRoleByCode(RoleIDs.Admin.Code);
				foreach (var __Item in __RoleDataSourcePermissionCheckSumList)
				{
					DataSourceDataManager.AddDataSourceToRole(__Role, __Item.DataSourceID, __Item.CanCreate, __Item.CanRead, __Item.CanUpdate, __Item.CanDelete);
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code + "_Admin", __StringCheckSum);
			}

		}

        public void AddUserPages()
        {
			List<cRoleDataSourcePermissionCheckSum> __RoleDataSourcePermissionCheckSumList = new List<cRoleDataSourcePermissionCheckSum>();


			cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code + "_Customer");
			string __TotalString = GetTotalString<cRoleDataSourcePermissionCheckSum>(__RoleDataSourcePermissionCheckSumList);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				cRoleEntity __Role = RoleDataManager.GetRoleByCode(RoleIDs.User.Code);
				foreach (var __Item in __RoleDataSourcePermissionCheckSumList)
				{
					DataSourceDataManager.AddDataSourceToRole(__Role, __Item.DataSourceID, __Item.CanCreate, __Item.CanRead, __Item.CanUpdate, __Item.CanDelete);
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code + "_Customer", __StringCheckSum);
			}

		}
		public void AddDeveloperPages()
		{
			List<cRoleDataSourcePermissionCheckSum> __RoleDataSourcePermissionCheckSumList = new List<cRoleDataSourcePermissionCheckSum>();

			cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code + "_Developer");
			string __TotalString = GetTotalString<cRoleDataSourcePermissionCheckSum>(__RoleDataSourcePermissionCheckSumList);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				cRoleEntity __Role = RoleDataManager.GetRoleByCode(RoleIDs.Developer.Code);
				foreach (var __Item in __RoleDataSourcePermissionCheckSumList)
				{
					DataSourceDataManager.AddDataSourceToRole(__Role, __Item.DataSourceID, __Item.CanCreate, __Item.CanRead, __Item.CanUpdate, __Item.CanDelete);
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code + "_Developer", __StringCheckSum);
			}

		}
	}
}
