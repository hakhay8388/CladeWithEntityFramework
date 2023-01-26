using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Data.Boundary.nData;
using Bootstrapper.Core.nApplication;
using Data.Domain.nDatabaseService;
using Data.Domain.nDataService.nDataManagers.nLoaders.nLoaderIDs;
using Data.Domain.nDefaultValueTypes;
using Data.Domain.nDatabaseService.nSystemEntities;

namespace Data.Domain.nDataService.nDataManagers.nLoaders
{
    public class cMenuDataLoader : cBaseDataLoader
    {
        public cPageDataManager PageDataManager { get; set; }

        public cMenuDataManager MenuDataManager { get; set; }

        public cMenuDataLoader(cApp _App, cDataService _DataService, IFileDateService _FileDataService, cChecksumDataManager _ChecksumDataManager
			   , cPageDataManager _PageDataManager
			   , cMenuDataManager _MenuDataManager
         )
          : base(_App, LoaderIDs.MenuDataLoader,  _DataService,_FileDataService, _ChecksumDataManager)
        {
            PageDataManager = _PageDataManager;
            MenuDataManager = _MenuDataManager;
        }

        public void Init()
        {
			cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code);
			string __TotalString = GetTotalString<MenuIDs>(MenuIDs.TypeList);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				for (int i = 0; i < MenuIDs.TypeList.Count; i++)
				{
					if (!MenuIDs.TypeList[i].IsMainMenu)
					{
						if (MenuIDs.TypeList[i].RootMenu == null)
						{
							MenuDataManager.CreateMenuIfNotExists(MenuIDs.TypeList[i], PageDataManager.GetPageByUrl(PageIDs.GetByCode(MenuIDs.TypeList[i].Code, null).Url));
						}
						else
						{
							MenuDataManager.CreateSubMenuIfNotExists(MenuIDs.TypeList[i].RootMenu, MenuIDs.TypeList[i], PageDataManager.GetPageByUrl(PageIDs.GetByCode(MenuIDs.TypeList[i].Code, null).Url));
						}
					}
					else
					{
						MenuDataManager.CreateMenuIfNotExists(MenuIDs.TypeList[i]);
					}
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code, __StringCheckSum);
			}

        }
    }
}
