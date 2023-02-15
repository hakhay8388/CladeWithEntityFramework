using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Sys.Boundary.nDefaultValueTypes;

using Bootstrapper.Core.nApplication;
using Sys.Data.nDatabaseService;
using Sys.Data.nDatabaseService.nSystemEntities;
using Base.Data.nDatabaseService;
using Sys.Boundary.nLoaderIDs;

namespace Sys.Data.nDataService.nDataManagers.nLoaders
{
    public class cRolePageLoader : cBaseDataLoader
    {
        public cRoleDataManager RoleDataManager { get; set; }
        public cPageDataManager PageDataManager { get; set; }
        public cRolePageLoader(cApp _App, IDataService _DataService, IFileDateService _FileDataService, cChecksumDataManager _ChecksumDataManager
            , cRoleDataManager _RoleDataManager
            , cPageDataManager _PageDataManager
            )
          : base(_App, LoaderIDs.RolePageLoader, _DataService, _FileDataService, _ChecksumDataManager)
        {
            RoleDataManager = _RoleDataManager;
            PageDataManager = _PageDataManager;
        }

        public void Init()
        {
            AddAdminPages();
            AddUserPages();
			AddDeveloperPages();

		}

        protected void AddPageToRole(cRoleEntity _Role, cPageEntity _PageEntity)
        {
            PageDataManager.AddPageToRole(_Role, _PageEntity);
        }


        public void AddAdminPages()
        {
            List<PageIDs> __Pages = PageIDs.TypeList.Where(__Item => __Item.MainRoles.Any(__Item => __Item.Code == RoleIDs.Admin.Code)).ToList();
            /*List<PageIDs> __Pages = new List<PageIDs>();

			__Pages.Add(PageIDs.AdminMainPage);
			__Pages.Add(PageIDs.BatchJobPage);
			__Pages.Add(PageIDs.ConfigurationPage);
			__Pages.Add(PageIDs.MenuPage);

			__Pages.Add(PageIDs.LanguagePage);
			__Pages.Add(PageIDs.UserList);*/


            cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code + "_Admin");
			string __TotalString = GetTotalString<PageIDs>(__Pages);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				cRoleEntity __Role = RoleDataManager.GetRoleByCode(RoleIDs.Admin.Code);
				for (int i = 0; i < __Pages.Count; i++)
				{
					AddPageToRole(__Role, PageDataManager.GetPageByUrl(__Pages[i].Url));
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code + "_Admin", __StringCheckSum);
			} 

        }

        

        public void AddUserPages()
        {
            List<PageIDs> __Pages = PageIDs.TypeList.Where(__Item => __Item.MainRoles.Any(__Item => __Item.Code == RoleIDs.User.Code)).ToList();
            //List<PageIDs> __Pages = new List<PageIDs>();

            //__Pages.Add(PageIDs.UserMainPage);


            cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code + "_User");
			string __TotalString = GetTotalString<PageIDs>(__Pages);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				cRoleEntity __Role = RoleDataManager.GetRoleByCode(RoleIDs.User.Code);
				for (int i = 0; i < __Pages.Count; i++)
				{
					AddPageToRole(__Role, PageDataManager.GetPageByUrl(__Pages[i].Url));
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code + "_User", __StringCheckSum);
			}
		}
		public void AddDeveloperPages()
		{
			List<PageIDs> __Pages = PageIDs.TypeList.Where(__Item => __Item.MainRoles.Any(__Item => __Item.Code == RoleIDs.Developer.Code)).ToList();

			/*__Pages.Add(PageIDs.DeveloperMainPage);
			__Pages.Add(PageIDs.SharedSessionPage);
			__Pages.Add(PageIDs.LiveSessionsPage);
			__Pages.Add(PageIDs.SystemSettingsPage);*/


			cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code + "_Developer");
			string __TotalString = GetTotalString<PageIDs>(__Pages);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				cRoleEntity __Role = RoleDataManager.GetRoleByCode(RoleIDs.Developer.Code);
				for (int i = 0; i < __Pages.Count; i++)
				{
					AddPageToRole(__Role, PageDataManager.GetPageByUrl(__Pages[i].Url));
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code + "_Developer", __StringCheckSum);
			}
		}
	}
}
