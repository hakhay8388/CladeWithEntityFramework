using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Base.Data.nDatabaseService;
using Data.Domain.nDatabaseService;

using System.Security.Policy;
using Data.Domain.nDefaultValueTypes;
using Data.Domain.nDatabaseService.nSystemEntities;
using Microsoft.EntityFrameworkCore;

namespace Data.Domain.nDataService.nDataManagers
{
    public class cPageDataManager : cBaseDataManager
    {
        public cPageDataManager(cDataServiceContext _CoreServiceContext, cDataService _DataService, IFileDateService _FileDataService)
          : base(_CoreServiceContext, _DataService, _FileDataService)
        {
        }

        public cPageEntity GetPageByUrl(string _Url)
        {
            return cPageEntity.Get(__Item => __Item.Url == _Url).FirstOrDefault();
        }

        public cPageEntity GetPageByCode(string _Code)
        {
            return cPageEntity.Get(__Item => __Item.Code == _Code).FirstOrDefault();
        }

        public cPageEntity AddPage(string _Name, string _Code, string _Url, string _ComponentName)
        {
            cPageEntity __PageEntity = cPageEntity.Add(new cPageEntity()
            {
                Name = _Name,
                Code = _Code,
                Url = _Url,
                ComponentName = _ComponentName
            });
            __PageEntity.Save();
            return __PageEntity;
        }
        public cPageEntity UpdatePage(cPageEntity __PageEntity, string _Name, string _Code, string _Url, string _ComponentName)
        {

            __PageEntity.Name = _Name;
            __PageEntity.Code = _Code;
            __PageEntity.Url = _Url;
            __PageEntity.ComponentName = _ComponentName;
            __PageEntity.Save();
            return __PageEntity;
        }

        public void CreatePageIfNotExists(PageIDs _PageID)
        {
            CreatePageIfNotExists(_PageID.Name, _PageID.Code, _PageID.Url, _PageID.Component);
        }

        public void CreatePageIfNotExists(string _Name, string _Code, string _Url, string _ComponentName)
        {
            cPageEntity __PageEntity = GetPageByUrl(_Url);
            if (__PageEntity == null)
            {
                AddPage(_Name, _Code, _Url, _ComponentName);
            }
#if DEBUG
            else
            {

                UpdatePage(__PageEntity, _Name, _Code, _Url, _ComponentName);

            }
#endif
        }

        public void AddPageToRole(cRoleEntity _Role, cPageEntity _Page)
        {
            if (_Page != null && !ControlRoleMenueExists(_Role, _Page))
            {
                _Role.Pages.Add(_Page);
                _Role.Save();
            }
        }

        public bool ControlRoleMenueExists(cRoleEntity _Role, cPageEntity _Page)
        {
            return cRoleEntity.Get(__Item => __Item.ID == _Role.ID && __Item.Pages.Any(__Item => __Item.ID == _Page.ID)).Count() > 0;
        }

        private IQueryable<cPageEntity> GetPageByUserQuery(cUserEntity _User)
        {
            IQueryable<cPageEntity> __Query = cPageEntity.Get();

                __Query = __Query.Where(__Item => __Item.Roles.Any(
                    __Item => __Item.ID == _User.ID)
                );

            return __Query;
        }

        public List<cPageEntity> GetPageByUser(cUserEntity _User)
        {
            return GetPageByUserQuery(_User).ToList();
        }


        public List<dynamic> GetPageByUserToDynamicList(cUserEntity _User, Action<dynamic> _Action)
        {
            IQueryable<cPageEntity> __Query = GetPageByUserQuery(_User);
            return __Query.ToDynamicObjectList(_Action);
        }
    }
}
