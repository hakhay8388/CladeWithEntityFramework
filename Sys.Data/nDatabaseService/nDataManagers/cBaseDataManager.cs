using Base.Data.nDatabaseService;
using Base.FileData;
using Base.FileData.nFileDataService;
using Bootstrapper.Core.nCore;
using Sys.Data.nDatabaseService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sys.Data.nDataService.nDataManagers
{
    public class cBaseDataManager : cCoreService<cDataServiceContext>
    {
        public IDataService DataService { get; set; }
        public IFileDateService FileDataService { get; set; }
        public cBaseDataManager(cDataServiceContext _CoreServiceContext, IDataService _DataService, IFileDateService _FileDataService)
          : base(_CoreServiceContext)
        {
            DataService = _DataService;
            FileDataService = _FileDataService;
        }
    }
}
