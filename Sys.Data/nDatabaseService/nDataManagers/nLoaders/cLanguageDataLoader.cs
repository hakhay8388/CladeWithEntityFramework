using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Sys.Data.nDataService.nDataManagers;
using Sys.Boundary.nDefaultValueTypes;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using Bootstrapper.Core.nApplication;
using Sys.Data.nDatabaseService;
using Base.Data.nDatabaseService;
using Sys.Data.nDatabaseService.nSystemEntities;
using Sys.Boundary.nLoaderIDs;

namespace Sys.Data.nDataService.nDataManagers.nLoaders
{
    public class cLanguageDataLoader : cBaseDataLoader
    {
        public cLanguageDataManager LanguageDataManager { get; set; }

        public cLanguageDataLoader(cApp _App, IDataService _DataService, IFileDateService _FileDataService, cChecksumDataManager _ChecksumDataManager
            , cLanguageDataManager _LanguageDataManager            
         )
          : base(_App, LoaderIDs.LanguageDataLoader, _DataService, _FileDataService, _ChecksumDataManager)
        {
            LanguageDataManager = _LanguageDataManager;
        }

        public override void Init()
        {
            
            for (int i = App.Handlers.LanguageHandler.LanguageList.Count - 1; i > -1; i--)
            {
                var __Item = App.Handlers.LanguageHandler.LanguageList.ElementAt(i);

                cLanguageEntity __LanguageEntity = LanguageDataManager.CreateLanguageIfNotExists(__Item.Value.Language.Code, __Item.Value.Language.Name, __Item.Value.Language.IconCode);


                cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code + "_" + __Item.Key);
                string __FileCheckSum = __Item.Value.GetCheckSum();

                if (__DBCheckSum == null || __DBCheckSum.CheckSum != __Item.Value.GetCheckSum())
                {
                    JObject __LanguageWordsObject = __Item.Value.LanguageObject;

                    foreach (var __Word in __LanguageWordsObject)
                    {
                        try
                        {
                            string __Message = __Word.Value["message"].ToString();
                            string __Description = __Word.Value["description"].ToString();

                            Regex __Regex = new Regex("{\\d+}");
                            int __Count = __Regex.Matches(__Message).Count;
                            cLanguageWordEntity __LanguageWordEntity = LanguageDataManager.CreateLanguageWordIfNotExists(__LanguageEntity, __Word.Key, __Message, __Description, __Count, true);
                        }
                        catch (Exception _Ex)
                        {
                            App.Loggers.CoreLogger.LogError(_Ex);
                        }
                    }
                    ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code + "_" + __Item.Key, __FileCheckSum);
                }
                foreach (PageIDs __PageID in PageIDs.TypeList)
                {
                    cLanguageWordEntity __LanguageWordEntity = LanguageDataManager.CreateLanguageWordIfNotExists(__LanguageEntity, "PageRoute_" + __PageID.OriginalCode, "", "", 0, false);
                }
            }
        }
    }
}
