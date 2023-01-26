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
    public class cChecksumDataManager : cBaseDataManager
    {
        public cChecksumDataManager(cDataServiceContext _CoreServiceContext, cDataService _DataService, IFileDateService _FileDataService)
          : base(_CoreServiceContext, _DataService, _FileDataService)
        {
        }

		public cDefaultDataChecksumEntity GetCheckSumByCode(string _CheckSumCode)
		{

			return  cDefaultDataChecksumEntity.Get(__Item => __Item.Code == _CheckSumCode).FirstOrDefault();
		}


		public cDefaultDataChecksumEntity AddCheckSum(string _Code, string _CheckSum)
		{
            cDefaultDataChecksumEntity __DefaultDataChecksumEntity = cDefaultDataChecksumEntity.Add(new cDefaultDataChecksumEntity()
            {
                Code = _Code,
                CheckSum = _CheckSum
            });

			__DefaultDataChecksumEntity.Save();
            return __DefaultDataChecksumEntity;
		}
		public cDefaultDataChecksumEntity UpdateCheckSum(cDefaultDataChecksumEntity _DefaultDataChecksumEntity, string _Code, string _CheckSum)
		{
            _DefaultDataChecksumEntity.Code = _Code;
			_DefaultDataChecksumEntity.CheckSum = _CheckSum;
            _DefaultDataChecksumEntity.Save();
            return _DefaultDataChecksumEntity;
		}

		public void CreateCheckSumIfNotExists(string _Code, string _CheckSum)
		{
            cDefaultDataChecksumEntity __DefaultDataChecksumEntity = GetCheckSumByCode(_Code);
			if (__DefaultDataChecksumEntity == null)
			{
				AddCheckSum(_Code, _CheckSum);
			}
			else
			{
				UpdateCheckSum(__DefaultDataChecksumEntity, _Code, _CheckSum);
			}
		}
	}
}
