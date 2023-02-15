using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Sys.Data.nDataService.nDataManagers;
using Sys.Boundary.nDefaultValueTypes;
using Bootstrapper.Core.nApplication;
using Sys.Data.nDatabaseService;
using Sys.Data.nDatabaseService.nSystemEntities;
using Base.Data.nDatabaseService;
using Sys.Boundary.nLoaderIDs;

namespace Sys.Data.nDataService.nDataManagers.nLoaders
{
    public class cRoleDataLoader : cBaseDataLoader
    {
        public cRoleDataManager RoleDataManager { get; set; }
        public cRoleDataLoader(cApp _App, IDataService _DataService, IFileDateService _FileDataService, cChecksumDataManager _ChecksumDataManager
            , cRoleDataManager _RoleDataManager)
          : base(_App, LoaderIDs.RoleDataLoader, _DataService, _FileDataService, _ChecksumDataManager)
        {
            RoleDataManager = _RoleDataManager;
        }

        public void Init()
        {
            cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code);
            string __TotalString = GetTotalString<RoleIDs>(RoleIDs.TypeList);
            string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

            if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
            {
                for (int i = 0; i < RoleIDs.TypeList.Count; i++)
                {
                    RoleDataManager.CreateRuleByCodeAndNameIfNotExists(RoleIDs.TypeList[i]);
                }

                ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code, __StringCheckSum);
            }

        }
    }
}
