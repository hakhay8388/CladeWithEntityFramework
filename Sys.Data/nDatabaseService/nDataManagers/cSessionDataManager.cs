using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Base.Data.nDatabaseService;
using Sys.Data.nDatabaseService;
using Sys.Data.nDatabaseService.nSystemEntities;
using Microsoft.EntityFrameworkCore;

namespace Sys.Data.nDataService.nDataManagers
{
    public class cSessionDataManager : cBaseDataManager
    {
        public cSessionDataManager(cDataServiceContext _CoreServiceContext, IDataService _DataService, IFileDateService _FileDataService)
          : base(_CoreServiceContext, _DataService, _FileDataService)
        {
        }

        public cUserEntity GetUserBySessionID(string _SessionID)
        {
            cUserEntity __User = cUserEntity.Get(__Item => __Item.Sessions.Any(__Item => __Item.SessionHash == _SessionID))
                                 .Include(__Item => __Item.UserDetail)
                                 .Include(__Item => __Item.Roles)
                                .FirstOrDefault();
            return __User;

        }

        
        public void DeleteOldSessionTempDate(DateTime _Date)
        {
            cUserSessionEntity.RemoveRange(__Item => __Item.CreateDate < _Date);
        }
        public int DeleteSession(string _SessionID)
        {

            return cUserSessionEntity.RemoveRange(__Item => __Item.SessionHash == _SessionID);
        }

        public cUserSessionEntity AddUserSession(cUserEntity _UserEntity, string _SessionID, string _IpAddress)
        {
            cUserSessionEntity __UserSessionEntity = new cUserSessionEntity()
            {   
                IpAddress = _IpAddress,
                SessionHash = _SessionID,
                User = _UserEntity
            };

            cUserSessionEntity.Add(__UserSessionEntity);
            __UserSessionEntity.Save();
            
            return __UserSessionEntity;
        }
        
        

    }
}
