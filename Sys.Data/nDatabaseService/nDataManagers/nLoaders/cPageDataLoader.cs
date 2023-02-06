using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Sys.Boundary.nData;
using Sys.Data.nDataService.nDataManagers.nLoaders;
using Bootstrapper.Core.nApplication;
using Sys.Data.nDatabaseService;
using Sys.Boundary.nDefaultValueTypes;
using Sys.Data.nDatabaseService.nSystemEntities;
using Base.Data.nDatabaseService;
using Sys.Boundary.nLoaderIDs;

namespace Sys.Data.nDataService.nDataManagers.nLoaders
{
    public class cPageDataLoader : cBaseDataLoader
    {
        public cPageDataManager PageDataManager { get; set; }
        public cPageDataLoader(cApp _App, IDataService _DataService, IFileDateService _FileDataService
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
