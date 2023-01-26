using Base.FileData;
using Base.FileData.nFileDataService;
using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using Data.Domain.nDatabaseService;
using Data.Domain.nDataService.nDataManagers.nLoaders.nLoaderIDs;
using Data.Domain.nDefaultValueTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Data.Domain.nDataService.nDataManagers.nLoaders
{
    public class cBaseDataLoader : cCoreObject
    { 
		public LoaderIDs LoaderID { get; set; }
		public cDataService DataService { get; set; }
        public IFileDateService FileDataService { get; set; }

        public cChecksumDataManager ChecksumDataManager { get; set; }


        public cBaseDataLoader(cApp _App, LoaderIDs _LoaderID, cDataService _DataService, IFileDateService _FileDataService, cChecksumDataManager _ChecksumDataManager)
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
