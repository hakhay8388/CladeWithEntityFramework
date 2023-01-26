using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Data.Domain.nDefaultValueTypes;
using Base.Data.nDatabaseService;
using Data.Domain.nDatabaseService;
using Data.Domain.nDatabaseService.nSystemEntities;

namespace Data.Domain.nDataService.nDataManagers
{
    public class cGlobalParamsDataManager : cBaseDataManager
    {
        public cGlobalParamsDataManager(cDataServiceContext _CoreServiceContext, cDataService _DataServiceManager, IFileDateService _FileDataService)
          : base(_CoreServiceContext, _DataServiceManager, _FileDataService)
        {
        }
        

        public cGlobalParamEntity GetParamByCode(string _Code)
        {
            return cGlobalParamEntity.Get(__Item => __Item.Code == _Code).FirstOrDefault();
        }
        public List<dynamic> GetAllParams()
        {
            return cGlobalParamEntity.GetAll().ToDynamicObjectList();
        }

        public cGlobalParamEntity AddGlobalParam(string _Code, string _Name, object _Value, string _TypeFullName, int _Order)
        {
            cGlobalParamEntity __GlobalParamEntity = cGlobalParamEntity.Add(new cGlobalParamEntity()
            {
                Name = _Name,
                Code = _Code,
                SortOrder = _Order,
                Value = _Value.ToString(),
                TypeFullName = _TypeFullName
            });
            
            __GlobalParamEntity.Save();

            return __GlobalParamEntity;
        }
        public cGlobalParamEntity UpdateGlobalParam(long _ID, string _Value)
        {

            cGlobalParamEntity __GlobalParamEntity = cGlobalParamEntity.GetEntityByID(_ID);

            __GlobalParamEntity.Value = _Value;
            __GlobalParamEntity.Save();

            return __GlobalParamEntity;
        }

        public cGlobalParamEntity CreateMenuIfNotExists(string _Code, string _Name, object _Value, string _TypeFullName, int _Order)
        {
            cGlobalParamEntity __GlobalParamEntity = GetParamByCode(_Code);
            if (__GlobalParamEntity == null)
            {
                __GlobalParamEntity = AddGlobalParam(_Code, _Name, _Value, _TypeFullName, _Order);
            }
            return __GlobalParamEntity;
        }

        public cGlobalParamEntity CreateMenuIfNotExists(DefaultGlobalParamsIDs _DefaultGlobalParamsID)
        {
            return CreateMenuIfNotExists(_DefaultGlobalParamsID.Code, _DefaultGlobalParamsID.Name, _DefaultGlobalParamsID.Value, _DefaultGlobalParamsID.Value.GetType().FullName, _DefaultGlobalParamsID.Order);
        }
        
    }
}
