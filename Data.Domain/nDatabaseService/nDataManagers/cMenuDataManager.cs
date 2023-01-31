using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Base.Data.nDatabaseService;
using Data.Domain.nDatabaseService;

using System.Drawing;
using Data.Domain.nDefaultValueTypes;
using Data.Domain.nDatabaseService.nSystemEntities;
using System.Data;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Numerics;

namespace Data.Domain.nDataService.nDataManagers
{
    public class cMenuDataManager : cBaseDataManager
    {
        public cMenuDataManager(cDataServiceContext _CoreServiceContext, cDataService _DataService, IFileDateService _FileDataService)
          : base(_CoreServiceContext, _DataService, _FileDataService)
        {
        }

        public cMenuEntity GetMenuByCode(string _Code)
        {
            return cMenuEntity.Get(__Item => __Item.Code == _Code).FirstOrDefault();
        }

        public cMenuEntity AddMenu(string _MenuTypeCode, string _Name, string _Code, string _Icon, int _SortValue, cPageEntity _PageEntity)
        {
            cMenuEntity __MenuEntity = cMenuEntity.Add(new cMenuEntity()
            {
                Name = _Name,
                Code = _Code,
                Icon = _Icon,
                MenuTypeCode = _MenuTypeCode,
                SortValue = _SortValue,
                Page = _PageEntity
            }); ;
            
            __MenuEntity.Save();

            return __MenuEntity;
        }
        public cMenuEntity UpdateMenu(cMenuEntity __MenuEntity, string _MenuTypeCode, string _Name, string _Code, string _Icon, int _SortValue)
        {
			__MenuEntity.Name = _Name;
            __MenuEntity.Code = _Code;
            __MenuEntity.Icon = _Icon;
            __MenuEntity.MenuTypeCode = _MenuTypeCode;
            __MenuEntity.SortValue = _SortValue;
            __MenuEntity.Save();
            return __MenuEntity;
        }

        public cMenuEntity CreateMenuIfNotExists(string _MenuTypeCode, string _Name, string _Code, string _Icon,
            int _SortValue, cPageEntity _PageEntity)
        {
            cMenuEntity __MenuEntity = GetMenuByCode(_Code);
            if (__MenuEntity == null)
            {
                __MenuEntity = AddMenu(_MenuTypeCode, _Name, _Code, _Icon, _SortValue, _PageEntity);
            }
#if DEBUG
            else
            {
                __MenuEntity = UpdateMenu(__MenuEntity, _MenuTypeCode, _Name, _Code, _Icon, _SortValue);
            }
#endif
            return __MenuEntity;
        }

        public cMenuEntity CreateMenuIfNotExists(MenuIDs _MenuID, cPageEntity _PageEntity = null)
        {
            return CreateMenuIfNotExists(_MenuID.MenuType.Code, _MenuID.Name, _MenuID.Code, _MenuID.Icon, _MenuID.ID, _PageEntity);
        }

        public cMenuEntity CreateSubMenuIfNotExists(MenuIDs _RootMenuID, string _MenuTypeCode, string _Name, string _Code, string _Icon, int _SortValue, cPageEntity _PageEntity)
        {
            cMenuEntity __MenuEntity = GetMenuByCode(_Code);
            if (__MenuEntity == null)
            {
                __MenuEntity = AddMenu(_MenuTypeCode, _Name, _Code, _Icon, _SortValue,  _PageEntity);
            }

            cMenuEntity __RootMenuEntity = GetMenuByCode(_RootMenuID.Code);
            __MenuEntity.RootMenu = __RootMenuEntity;
			__MenuEntity.MenuTypeCode = _MenuTypeCode;
			__MenuEntity.Save();
            return __MenuEntity;
        }

        public cMenuEntity CreateSubMenuIfNotExists(MenuIDs _RootMenuID, MenuIDs _MenuID, cPageEntity _PageEntity)
        {
            return CreateSubMenuIfNotExists(_RootMenuID, _MenuID.MenuType.Code, _MenuID.Name, _MenuID.Code, _MenuID.Icon, _MenuID.ID, _PageEntity);
        }

        private IQueryable<cMenuEntity> GetMenuByUserQuery(cUserEntity _User, string _MenuTypeCode, string _RootMenuCode, bool _IncludePages = false)
        {
            IQueryable<cMenuEntity> __Query = cMenuEntity.Get(__Item => __Item.MenuTypeCode == _MenuTypeCode);

            if (_RootMenuCode.IsNullOrEmpty())
            {
                __Query = __Query.Where(__Item => __Item.RootMenu.Code == _RootMenuCode);
            }

            if (_User != null)
            {
                __Query = __Query.Where(__Item => __Item.Roles.Any(
                    __Item => __Item.Role.Users.Any(__Item => __Item.ID == _User.ID)
                ));
            }
            else
            {
                __Query = __Query.Where(__Item => __Item.Roles.Any(
                    __Item => __Item.Role.Code == RoleIDs.Unlogined.Code
                ));
            }

            if (!_IncludePages)
            {
                __Query = __Query.Include(__Item => __Item.Page);
            }

            

           /*List <cMenuEntity> __MenuEntity = cMenuEntity.Get(
               __Item => __Item.MenuTypeCode == _MenuTypeCode && __Item.Roles.Any(
                   __Item => __Item.Role.Users.Any(__Item => __Item.ID == _User.ID)
               )

           ).ToList()*/;

            return __Query;
        }

        public List<cMenuEntity> GetMenuByUser(cUserEntity _User, string _MenuTypeCode, string _RootMenuCode, bool _IncludePages = false)
        {
            return GetMenuByUserQuery(_User, _MenuTypeCode, _RootMenuCode, _IncludePages).ToList(); ;
        }


        public List<dynamic> GetMenuByActorToDynamicList(cUserEntity _User, string _MenuTypeCode, string _RootMenuCode, bool _IncludePages, Action<dynamic> _Action = null)
        {
            IQueryable<cMenuEntity> __Query = GetMenuByUserQuery(_User, _MenuTypeCode, _RootMenuCode, _IncludePages);
            return __Query.Select(__Item => new 
            {
                ID = __Item.ID,
                Name = __Item.Name,
                Code = __Item.Code,
                CreateDate = __Item.CreateDate,
                Icon = __Item.Icon,
                MenuTypeCode = __Item.MenuTypeCode,
                Url = __Item.Page.Url,
                SortValue = __Item.SortValue
            })
            .ToDynamicObjectList(_Action);
        }



        




        /* public List<object> GetSubMenus(List<MenuIDs> _MenuIDsList)
         {
             List<object> __List = new List<object>();
             foreach (MenuIDs _Menu in _MenuIDsList)
             {
                 cMenuEntity __MenuEntity = GetMenuByCode(_Menu.Code);
                 cPageEntity __Page = __MenuEntity.Page.GetValue();
                 __List.Add(new
                 {
                     url = __Page.Url,
                     icon = __MenuEntity.Icon,
                     name = __MenuEntity.Name,
                     Active = false
                 });

             }
             return __List;
         }*/
    }
}
