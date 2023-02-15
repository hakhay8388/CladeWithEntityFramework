using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Bootstrapper.Core.nApplication;
using Sys.Data.nDatabaseService;
using Sys.Boundary.nDefaultValueTypes;
using Sys.Data.nDatabaseService.nSystemEntities;
using Base.Data.nDatabaseService;
using Sys.Boundary.nLoaderIDs;

namespace Sys.Data.nDataService.nDataManagers.nLoaders
{
    public class cRoleDataSourceColumnLoader : cBaseDataLoader
    {
        public cRoleDataManager RoleDataManager { get; set; }
        public cDataSourceDataManager DataSourceDataManager { get; set; }

        public cRoleDataSourceColumnLoader(cApp _App, IDataService _DataService, IFileDateService _FileDataService, cChecksumDataManager _ChecksumDataManager
			, cRoleDataManager _RoleDataManager
            , cMenuDataManager _MenuDataManager
            , cPageDataManager _PageDataManager
            , cDataSourceDataManager _DataSourceDataManager
            )
          : base(_App, LoaderIDs.RoleDataSourceColumnLoader, _DataService, _FileDataService, _ChecksumDataManager)
        {
            RoleDataManager = _RoleDataManager;
            DataSourceDataManager = _DataSourceDataManager;
        }

        public void Init()
        {
			AddAdminRoleDataSourceColumns();
			AddUserRoleDataSourceColumns();
			AddDeveloperRoleDataSourceColumns();
		}


        public void AddAdminRoleDataSourceColumns()
        {
            List<DataSourceIDs> __DataSources = DataSourceIDs.TypeList.Where(__Item => __Item.MainRoles.Any(__Item => __Item.Code == RoleIDs.Admin.Code)).ToList();

			cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code + "_Admin");
			string __TotalString = GetTotalString<DataSourceIDs>(__DataSources);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				cRoleEntity __Role = RoleDataManager.GetRoleByCode(RoleIDs.Admin.Code);
				for (int i = 0; i < __DataSources.Count; i++)
				{
					DataSourceDataManager.AddAllDatasourceColumnToRole(__Role, __DataSources[i]);
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code + "_Admin", __StringCheckSum);
			}
		} 


        public void AddUserRoleDataSourceColumns()
        {
            List<DataSourceIDs> __DataSources = DataSourceIDs.TypeList.Where(__Item => __Item.MainRoles.Any(__Item => __Item.Code == RoleIDs.User.Code)).ToList();

            cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code + "_Customer");
			string __TotalString = GetTotalString<DataSourceIDs>(__DataSources);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				cRoleEntity __Role = RoleDataManager.GetRoleByCode(RoleIDs.User.Code);
				for (int i = 0; i < __DataSources.Count; i++)
				{
					DataSourceDataManager.AddAllDatasourceColumnToRole(__Role, __DataSources[i]);
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code + "_Customer", __StringCheckSum);
			}
		}
		public void AddDeveloperRoleDataSourceColumns()
		{
            List<DataSourceIDs> __DataSources = DataSourceIDs.TypeList.Where(__Item => __Item.MainRoles.Any(__Item => __Item.Code == RoleIDs.Developer.Code)).ToList();

            cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code + "_Developer");
			string __TotalString = GetTotalString<DataSourceIDs>(__DataSources);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				cRoleEntity __Role = RoleDataManager.GetRoleByCode(RoleIDs.Developer.Code);
				for (int i = 0; i < __DataSources.Count; i++)
				{
					DataSourceDataManager.AddAllDatasourceColumnToRole(__Role, __DataSources[i]);
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code + "_Developer", __StringCheckSum);
			}
		}
	}
}
