using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Data.Boundary.nData;
using Bootstrapper.Core.nApplication;
using Data.Domain.nDatabaseService;
using Data.Domain.nDataService.nDataManagers.nLoaders.nLoaderIDs;
using Data.Domain.nDatabaseService.nSystemEntities;
using Data.Domain.nDefaultValueTypes;

namespace Data.Domain.nDataService.nDataManagers.nLoaders
{
    public class cDefaultUsersDataLoader : cBaseDataLoader
    {
        public int AdminCount = 10;
        public int UserCount = 10;
        public int DeveloperCount = 10;

        public cRoleDataManager RoleDataManager { get; set; }
        public cPageDataManager PageDataManager { get; set; }
        public cUserDataManager UserDataManager { get; set; }
        public cDefaultUsersDataLoader(cApp _App, cDataService _DataService, IFileDateService _FileDataService, cChecksumDataManager _ChecksumDataManager
            , cRoleDataManager _RoleDataManager
            , cUserDataManager _UserDataManager
            , cPageDataManager _PageDataManager
            )
          : base(_App, LoaderIDs.DefaultUsersDataLoader, _DataService, _FileDataService, _ChecksumDataManager)
        {
            RoleDataManager = _RoleDataManager;
            UserDataManager = _UserDataManager;
            PageDataManager = _PageDataManager;
        }

        public void Init()
        {
            cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code);
            string __TotalString = "AdminCount=" + AdminCount.ToString() + ";" + "UserCount=" + UserCount.ToString() + ";" + "DeveloperCount=" + DeveloperCount.ToString() + ";";
            string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

            if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
            {
                InitAdmins();
                InitUser();
                InitDeveloper();

                ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code, __StringCheckSum);
            }
        }



        public void InitAdmins()
        {
            for (var i = 0; i < AdminCount; i++)
            {
                string __Add = "";
                if (i > 0)
                {
                    __Add = i.ToString();
                }
                cUserEntity __Admin = null;
                try
                {
                    if (UserDataManager.GetUserByEmail("admin" + __Add + "@admin" + __Add + ".com") == null)
                    {
                        __Admin = cUserEntity.Add(new cUserEntity()
                        {
                            Name = "Sistem" + __Add,
                            Surname = "Sistem" + __Add,
                            Email = "admin" + __Add + "@admin" + __Add + ".com",
                            Password = "1",
                            State = EUserState.Confirmed.ID,
                            Language = "tr",
                            UserDetail = new cUserDetailEntity()
                            {
                                Telephone = (!__Add.IsNullOrEmpty() ? __Add + "-" + __Add + "-" + __Add : "")
                            },
                            MainPage = PageDataManager.GetPageByCode(PageIDs.AdminMainPage.Code)
                        });
                        
                        __Admin.Save();


                        cRoleEntity __RoleEntity = RoleDataManager.GetRoleByCode(RoleIDs.Admin.Code);
                        __Admin.Roles.Add(__RoleEntity);
                        __Admin.Save();
  
                    }

                }
                catch (Exception _Ex)
                {
                    App.Loggers.CoreLogger.LogError(_Ex);
                    throw _Ex;
                }
            }
        }

        public void InitUser()
        {
            for (var i = 0; i < UserCount; i++)
            {
                string __Add = "";
                if (i > 0)
                {
                    __Add = i.ToString();
                }
                cUserEntity __User = null;
                try
                {
                    if (UserDataManager.GetUserByEmail("user" + __Add + "@user" + __Add + ".com") == null)
                    {
                        __User = cUserEntity.Add(new cUserEntity()
                        {
                            Name = "User" + __Add,
                            Surname = "User" + __Add,
                            Email = "user" + __Add + "@user" + __Add + ".com",
                            Password = "1",
                            State = EUserState.Confirmed.ID,
                            Language = "tr",
                            UserDetail = new cUserDetailEntity()
                            {
                                Telephone = (!__Add.IsNullOrEmpty() ? __Add + "-" + __Add + "-" + __Add : "")
                            },
                            MainPage = PageDataManager.GetPageByCode(PageIDs.UserMainPage.Code)
                        });

                        __User.Save();


                        cRoleEntity __RoleEntity = RoleDataManager.GetRoleByCode(RoleIDs.User.Code);
                       __User.Roles.Add(__RoleEntity);
                        __User.Save();

                    }

                }
                catch (Exception _Ex)
                {
                    App.Loggers.CoreLogger.LogError(_Ex);
                    throw _Ex;
                }
            }
        }

        public void InitDeveloper()
        {
            for (var i = 0; i < DeveloperCount; i++)
            {
                string __Add = "";
                if (i > 0)
                {
                    __Add = i.ToString();
                }
                cUserEntity __Developer = null;
                try
                {
                    if (UserDataManager.GetUserByEmail("developer" + __Add + "@developer" + __Add + ".com") == null)
                    {
                        __Developer = cUserEntity.Add(new cUserEntity()
                        {
                            Name = "Developer" + __Add,
                            Surname = "Developer" + __Add,
                            Email = "developer" + __Add + "@developer" + __Add + ".com",
                            Password = "1",
                            State = EUserState.Confirmed.ID,
                            Language = "tr",
                            UserDetail = new cUserDetailEntity()
                            {
                                Telephone = (!__Add.IsNullOrEmpty() ? __Add + "-" + __Add + "-" + __Add : "")
                            },
                            MainPage = PageDataManager.GetPageByCode(PageIDs.DeveloperMainPage.Code)
                        });

                        __Developer.Save();


                        cRoleEntity __RoleEntity = RoleDataManager.GetRoleByCode(RoleIDs.Developer.Code);
                        __Developer.Roles.Add(__RoleEntity);
                        __Developer.Save();

                    }

                }
                catch (Exception _Ex)
                {
                    App.Loggers.CoreLogger.LogError(_Ex);
                    throw _Ex;
                }
            }
        }
    }
}
