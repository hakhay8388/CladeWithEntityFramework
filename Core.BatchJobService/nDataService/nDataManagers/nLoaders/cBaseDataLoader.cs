using Base.Data.nDatabaseService;
using Base.FileData;
using Base.FileData.nFileDataService;
using Bootstrapper.Core.nCore;
using Sys.Data.nDatabaseService;
using Sys.Data.nDataService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.BatchJobService.nDataService.nDataManagers.nLoaders
{
    public class cBaseDataLoader : cCoreService<cDataServiceContext>
    {
        public IDataService DataService { get; set; }
        public IFileDateService FileDataService { get; set; }
        public cBaseDataLoader(cDataServiceContext _CoreServiceContext, IDataService _DataService, IFileDateService _FileDataService)
          : base(_CoreServiceContext)
        {
            DataService = _DataService;
            FileDataService = _FileDataService;
        }
    }
}
