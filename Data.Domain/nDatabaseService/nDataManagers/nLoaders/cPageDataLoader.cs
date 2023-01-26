using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Data.Boundary.nData;
using Data.Domain.nDataService.nDataManagers.nLoaders;
using Bootstrapper.Core.nApplication;
using Data.Domain.nDatabaseService;
using Data.Domain.nDataService.nDataManagers.nLoaders.nLoaderIDs;
using Data.Domain.nDefaultValueTypes;
using Data.Domain.nDatabaseService.nSystemEntities;

namespace Data.Domain.nDataService.nDataManagers.nLoaders
{
    public class cPageDataLoader : cBaseDataLoader
    {
        public cPageDataManager PageDataManager { get; set; }
        public cPageDataLoader(cApp _App, cDataService _DataService, IFileDateService _FileDataService
            , cPageDataManager _PageDataManager
            , cChecksumDataManager _ChecksumDataManager
         )
          : base(_App, LoaderIDs.PageDataLoader, _DataService, _FileDataService, _ChecksumDataManager)
        {
            PageDataManager = _PageDataManager;
        }

        public void Init()
        {			
			cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code);
			string __TotalString = GetTotalString<PageIDs>(PageIDs.TypeList);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				for (int i = 0; i < PageIDs.TypeList.Count; i++)
				{
					PageDataManager.CreatePageIfNotExists(PageIDs.TypeList[i]);
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code, __StringCheckSum);
			}
		}
    }
}
