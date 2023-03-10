using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Sys.Data.nDatabaseService;
using Bootstrapper.Core.nApplication;

using Sys.Boundary.nDefaultValueTypes;
using Sys.Data.nDatabaseService.nSystemEntities;
using Base.Data.nDatabaseService;
using Sys.Boundary.nLoaderIDs;

namespace Sys.Data.nDataService.nDataManagers.nLoaders
{
    public class cRoleMenuLoader : cBaseDataLoader
    {
        public cRoleDataManager RoleDataManager { get; set; }
        public cMenuDataManager MenuDataManager { get; set; }
        public cPageDataManager PageDataManager { get; set; }
        public cRoleMenuLoader(cApp _App, IDataService _DataService, IFileDateService _FileDataService, cChecksumDataManager _ChecksumDataManager
			, cRoleDataManager _RoleDataManager
            , cMenuDataManager _MenuDataManager
            , cPageDataManager _PageDataManager
            )
          : base(_App, LoaderIDs.RoleMenuLoader, _DataService, _FileDataService, _ChecksumDataManager)
        {
            RoleDataManager = _RoleDataManager;
            MenuDataManager = _MenuDataManager;
            PageDataManager = _PageDataManager;
        }

        public void Init()
        {
            AddAdminMenus();
			AddUserMenus();
			AddDeveloperMenus();

		}

        protected void AddMenuToRole(cRoleEntity _Role, cMenuEntity _MenuEntity)
        {
            RoleDataManager.AddMenuToRole(_Role, _MenuEntity);
        }

        public void AddAdminMenus()
        {
            List<MenuIDs> __Menus = MenuIDs.TypeList.Where(__Item => __Item.MainRoles.Any(__Item => __Item.Code == RoleIDs.Admin.Code)).ToList();


			cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code + "_Admin");
			string __TotalString = GetTotalString<MenuIDs>(__Menus);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				cRoleEntity __Role = RoleDataManager.GetRoleByCode(RoleIDs.Admin.Code);
				for (int i = 0; i < __Menus.Count; i++)
				{
					AddMenuToRole(__Role, MenuDataManager.GetMenuByCode(__Menus[i].Code));
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code + "_Admin", __StringCheckSum);
			}

		}


        public void AddUserMenus()
        {
            List<MenuIDs> __Menus = MenuIDs.TypeList.Where(__Item => __Item.MainRoles.Any(__Item => __Item.Code == RoleIDs.User.Code)).ToList();


			cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code + "_Customer");
			string __TotalString = GetTotalString<MenuIDs>(__Menus);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				cRoleEntity __Role = RoleDataManager.GetRoleByCode(RoleIDs.User.Code);
				for (int i = 0; i < __Menus.Count; i++)
				{
					AddMenuToRole(__Role, MenuDataManager.GetMenuByCode(__Menus[i].Code));
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code + "_Customer", __StringCheckSum);
			}
		}
		public void AddDeveloperMenus()
		{

            List<MenuIDs> __Menus = MenuIDs.TypeList.Where(__Item => __Item.MainRoles.Any(__Item => __Item.Code == RoleIDs.Developer.Code)).ToList();



            cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code + "_Developer");
			string __TotalString = GetTotalString<MenuIDs>(__Menus);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				cRoleEntity __Role = RoleDataManager.GetRoleByCode(RoleIDs.Developer.Code);
				for (int i = 0; i < __Menus.Count; i++)
				{
					AddMenuToRole(__Role, MenuDataManager.GetMenuByCode(__Menus[i].Code));
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code + "_Developer", __StringCheckSum);
			}
		}
	}
}
