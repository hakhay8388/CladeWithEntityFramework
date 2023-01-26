using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Data.Domain.nDataService.nDataManagers;
using Data.Domain.nDefaultValueTypes;
using Data.Domain.nDataService.nDataManagers.nLoaders.nLoaderIDs;
using Bootstrapper.Core.nApplication;
using Data.Domain.nDatabaseService;
using Data.Domain.nDatabaseService.nSystemEntities;

namespace Data.Domain.nDataService.nDataManagers.nLoaders
{
    public class cGlobalParamsDataLoader : cBaseDataLoader
    {
        public cGlobalParamsDataManager GlobalParamsDataManager { get; set; }


        public cGlobalParamsDataLoader(cApp _App, cDataService _DataService, IFileDateService _FileDataService, cChecksumDataManager _ChecksumDataManager,
            cGlobalParamsDataManager _GlobalParamsDataManager)
          : base(_App, LoaderIDs.GlobalParamsDataLoader, _DataService, _FileDataService, _ChecksumDataManager)
        {
            GlobalParamsDataManager = _GlobalParamsDataManager;
        }

        public override void Init()
        {
            ////////////// Customer //////////////////

            cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code);
            string __TotalString = GetTotalString<DefaultGlobalParamsIDs>(DefaultGlobalParamsIDs.TypeList);
            string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

            if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
            {
                for (int i = 0; i < DefaultGlobalParamsIDs.TypeList.Count; i++)
                {
                    GlobalParamsDataManager.CreateMenuIfNotExists(DefaultGlobalParamsIDs.TypeList[i]);
                }

                ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code, __StringCheckSum);
            }

            /////////////////////////////////////////////////////////////////////////////////
        }
    }
}
