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
using Sys.Data.nDataService.nDataManagers.nLoaders;
using Sys.Data.nDataService.nDataManagers;
using Domain.Boundary.nLoaderIDs;

namespace Domain.Data.nDataService.nDataManagers.nLoaders
{
    public class cTestLoader : cBaseDataLoader
    {
        public cTestLoader(cApp _App, IDataService _DataService, IFileDateService _FileDataService, cChecksumDataManager _ChecksumDataManager)
          : base(_App, DomainLoaderIDs.TestDataLoader, _DataService, _FileDataService, _ChecksumDataManager)
        {
        }

        public void Init()
        {

		}


	}
}
