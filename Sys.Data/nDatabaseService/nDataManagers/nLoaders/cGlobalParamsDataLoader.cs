using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Sys.Data.nDataService.nDataManagers;
using Bootstrapper.Core.nApplication;
using Sys.Data.nDatabaseService;
using Sys.Data.nDatabaseService.nSystemEntities;
using Sys.Boundary.nDefaultValueTypes;
using Base.Data.nDatabaseService;
using Sys.Boundary.nLoaderIDs;

namespace Sys.Data.nDataService.nDataManagers.nLoaders
{
    public class cGlobalParamsDataLoader : cBaseDataLoader
    {
        public cGlobalParamsDataManager GlobalParamsDataManager { get; set; }


        public cGlobalParamsDataLoader(cApp _App, IDataService _DataService, IFileDateService _FileDataService, cChecksumDataManager _ChecksumDataManager,
            cGlobalParamsDataManager _GlobalParamsDataManager)
          : base(_App, LoaderIDs.GlobalParamsDataLoader, _DataService, _FileDataService, _ChecksumDataManager)
        {
            GlobalParamsDataManager = _GlobalParamsDataManager;
        }

        public override void Init()
        {
            Type __DefaultGlobalParamsIDType = App.Handlers.AssemblyHandler.GetTypeFromBaseInDomainHierarchy<DefaultGlobalParamsIDs>();
            __DefaultGlobalParamsIDType.GetMethod("Init").Invoke(null, null);

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
