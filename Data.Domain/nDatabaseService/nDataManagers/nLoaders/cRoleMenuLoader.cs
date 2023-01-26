using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Data.Domain.nDataService.nDataManagers.nLoaders.nLoaderIDs;
using Data.Domain.nDatabaseService;
using Bootstrapper.Core.nApplication;

using Data.Domain.nDefaultValueTypes;
using Data.Domain.nDatabaseService.nSystemEntities;

namespace Data.Domain.nDataService.nDataManagers.nLoaders
{
    public class cRoleMenuLoader : cBaseDataLoader
    {
        public cRoleDataManager RoleDataManager { get; set; }
        public cMenuDataManager MenuDataManager { get; set; }
        public cPageDataManager PageDataManager { get; set; }
        public cRoleMenuLoader(cApp _App, cDataService _DataService, IFileDateService _FileDataService, cChecksumDataManager _ChecksumDataManager
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

			List<MenuIDs> __Menus = new List<MenuIDs>();

			__Menus.Add(MenuIDs.AdminMainPage);
			__Menus.Add(MenuIDs.BatchJobPage);
			__Menus.Add(MenuIDs.ConfigurationPage);
			__Menus.Add(MenuIDs.UserList);
			__Menus.Add(MenuIDs.UsersMenu);
			__Menus.Add(MenuIDs.LanguagePage);			

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

			List<MenuIDs> __Menus = new List<MenuIDs>();

			__Menus.Add(MenuIDs.UserMainPage);


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

			List<MenuIDs> __Menus = new List<MenuIDs>();

			__Menus.Add(MenuIDs.DeveloperMainPage);
			__Menus.Add(MenuIDs.SharedSessionPage);
			__Menus.Add(MenuIDs.SystemSettingsPage);
			__Menus.Add(MenuIDs.LiveSessionsPage);



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
