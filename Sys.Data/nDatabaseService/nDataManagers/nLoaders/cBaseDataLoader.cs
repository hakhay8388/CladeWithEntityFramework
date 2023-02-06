using Base.FileData;
using Base.FileData.nFileDataService;
using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using Sys.Data.nDatabaseService;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Base.Data.nDatabaseService;
using Sys.Boundary.nLoaderIDs;

namespace Sys.Data.nDataService.nDataManagers.nLoaders
{
    public class cBaseDataLoader : cCoreObject
    { 
		public LoaderIDs LoaderID { get; set; }
		public IDataService DataService { get; set; }
        public IFileDateService FileDataService { get; set; }

        public cChecksumDataManager ChecksumDataManager { get; set; }


        public cBaseDataLoader(cApp _App, LoaderIDs _LoaderID, IDataService _DataService, IFileDateService _FileDataService, cChecksumDataManager _ChecksumDataManager)
          : base(_App)
        {
			LoaderID = _LoaderID;
            DataService = _DataService;
            FileDataService = _FileDataService;
            ChecksumDataManager = _ChecksumDataManager;
        }

		public string GetTotalString<TType>(List<TType> _List)
		{
			JArray __JArray = JArray.FromObject(_List);
			return __JArray.ToString();
		}
	}
}
