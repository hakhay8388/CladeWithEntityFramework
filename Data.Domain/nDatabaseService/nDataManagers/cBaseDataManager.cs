using Base.Data.nDatabaseService;
using Base.FileData;
using Base.FileData.nFileDataService;
using Bootstrapper.Core.nCore;
using Data.Domain.nDatabaseService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Domain.nDataService.nDataManagers
{
    public class cBaseDataManager : cCoreService<cDataServiceContext>
    {
        public cDataService DataService { get; set; }
        public IFileDateService FileDataService { get; set; }
        public cBaseDataManager(cDataServiceContext _CoreServiceContext, cDataService _DataService, IFileDateService _FileDataService)
          : base(_CoreServiceContext)
        {
            DataService = _DataService;
            FileDataService = _FileDataService;
        }
    }
}
