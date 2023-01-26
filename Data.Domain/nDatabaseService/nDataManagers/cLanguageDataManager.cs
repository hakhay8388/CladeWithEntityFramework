using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Data.Domain.nDefaultValueTypes;

using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using Base.Data.nDatabaseService;
using Data.Domain.nDatabaseService;
using System.Net;
using Data.Domain.nDatabaseService.nSystemEntities;

namespace Data.Domain.nDataService.nDataManagers
{
    public class cLanguageDataManager : cBaseDataManager
    {
        public cLanguageDataManager(cDataServiceContext _CoreServiceContext, cDataService _DataService
            , IFileDateService _FileDataService
            )
          : base(_CoreServiceContext, _DataService, _FileDataService)
        {

        }



        public cLanguageEntity GetLanguageByCode(string _LanguageCode)
        {
            return cLanguageEntity.Get(__Item => __Item.Code == _LanguageCode).FirstOrDefault();
        }

        public cLanguageEntity AddLanguage(string _Code, string _Name, string _IconCode)
        {
            cLanguageEntity __LanguageEntity = cLanguageEntity.Add(new cLanguageEntity()
            {
                Name = _Name,
                Code = _Code,
                IconCode = _IconCode
            });

            __LanguageEntity.Save();

            return __LanguageEntity;
        }

        public cLanguageEntity CreateLanguageIfNotExists(string _Code, string _Name, string _IconCode)
        {
            cLanguageEntity __LanguageEntity = GetLanguageByCode(_Code);
            if (__LanguageEntity == null)
            {
                __LanguageEntity = AddLanguage(_Code, _Name, _IconCode);
            }

            return __LanguageEntity;
        }

        public cLanguageEntity GetLanguageByID(int _LanguageID)
        {
            return cLanguageEntity.GetEntityByID(_LanguageID);
        }

        public List<cLanguageEntity> GetLanguages()
        {
            return cLanguageEntity.GetAll().ToList();
        }

        public cLanguageWordEntity AddLanguageWord(cLanguageEntity _LanguageEntity, string _Code, string _Word, string _Description, int _ParamCount, string _CheckSum)
        {
            cLanguageWordEntity __LanguageWordEntity = new cLanguageWordEntity()
            {
                Code = _Code,
                Word = _Word,
                CheckSum = _CheckSum,
                Description = _Description,
                ParamCount = _ParamCount,
            };
            
            _LanguageEntity.Words.Add(__LanguageWordEntity);
            _LanguageEntity.Save();

            return __LanguageWordEntity;
        }
        public cLanguageWordEntity UpdateLanguageWordCheckSum(cLanguageWordEntity _LanguageWordEntity, string _Word, string _CheckSum)
        {
            _LanguageWordEntity.Word = _Word;
            _LanguageWordEntity.CheckSum = _CheckSum;

            _LanguageWordEntity.Save();

            return _LanguageWordEntity;
        }
        public cLanguageWordEntity UpdateLanguageWord(int _LanguageWordID, string _Word)
        {
            cLanguageWordEntity __LanguageWordEntity = cLanguageWordEntity.GetEntityByID(_LanguageWordID);
            __LanguageWordEntity.Word = _Word;
            __LanguageWordEntity.Save();
            return __LanguageWordEntity;
        }
       
        public cLanguageWordEntity GetLanguageWordByCode(cLanguageEntity _LanguageEntity, string _Code)
        {
            return cLanguageWordEntity.Get(__Item => __Item.Code == _Code && __Item.Language == _LanguageEntity).OrderBy(__Item => __Item.ID).FirstOrDefault();
        }

        public cLanguageWordEntity CreateLanguageWordIfNotExists(cLanguageEntity _LanguageEntity, string _Code, string _Word, string _Description, int _ParamCount,bool _Updatable)
        {
            cLanguageWordEntity __LanguageWordEntity = GetLanguageWordByCode(_LanguageEntity, _Code);
            string __NewCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(_Word);
            if (__LanguageWordEntity == null)
            {
                __LanguageWordEntity = AddLanguageWord(_LanguageEntity, _Code, _Word, _Description, _ParamCount, __NewCheckSum);
            }
            else if (__LanguageWordEntity.CheckSum != __NewCheckSum)
            {
                if (_Updatable)
                {
                    UpdateLanguageWordCheckSum(__LanguageWordEntity, _Word, __NewCheckSum);
                }
                
            }

            return __LanguageWordEntity;
        }

        public List<cLanguageWordEntity> GetLanguageWords(cLanguageEntity _LanguageEntity)
        {
            return cLanguageWordEntity.Get(__Item => __Item.Language == _LanguageEntity).ToList();
        }
        public List<dynamic> GetWordByCode(string _Code)
        {
            cLanguageEntity __LanguageEntity = null;
            cLanguageWordEntity __LanguageWordEntity = null;

            List<dynamic> __LanguageWordEntityList = cLanguageWordEntity.Get(__Item => __Item.Code == _Code)
              .Select(__Item => new
              {
                  Word = __Item.Word,
                  Name = __Item.Language.Name,
                  ID = __Item.ID,
                  ParamCount = __Item.ParamCount
              }).ToDynamicObjectList();

            return __LanguageWordEntityList;
        }

        public List<cLanguageWordEntity> GetLanguageWords(string _LanguageCode)
        {
            return GetLanguageWords(GetLanguageByCode(_LanguageCode));
        }

        public void RefreshLanguageFromDB()
        {
            foreach (var __LanguageItem in App.Handlers.LanguageHandler.LanguageList)
            {
                //__LanguageItem.Value
                JObject __JObject = new JObject();
                List<cLanguageWordEntity> __Words = GetLanguageWords(__LanguageItem.Key);
                List<string> __WordList = new List<string>();
                for (int i = 0; i < __Words.Count; i++)
                {
                    JObject __MesaageObject = new JObject();
                    __MesaageObject.Add("message", __Words[i].Word);
                    __MesaageObject.Add("description", __Words[i].Description);

                    if (__WordList.IndexOf(__Words[i].Code) == -1)
                    {
                        __WordList.Add(__Words[i].Code);
                        __JObject.Add(__Words[i].Code, __MesaageObject);
                    }
                    else
                    {
                        __JObject.Remove(__Words[i].Code);
                        __JObject.Add(__Words[i].Code, __MesaageObject);
                    }
                }
                __LanguageItem.Value.LanguageObject = __JObject;
            }
        }
        
    }
}
