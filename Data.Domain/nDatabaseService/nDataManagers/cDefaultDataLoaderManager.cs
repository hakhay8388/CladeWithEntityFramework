using Base.Data.nDatabaseService;
using Base.Data.nDatabaseService.nDatabase;
using Base.FileData;
using Bootstrapper.Boundary.nCore.nObjectLifeTime;
using Bootstrapper.Core.nApplication.nStarter;
using Bootstrapper.Core.nAttributes;
using Data.Domain.nDatabaseService;
using Data.Domain.nDataService.nDataManagers.nLoaders;

namespace Data.Domain.nDataService.nDataManagers
{
    [Register(typeof(IDefaultDataLoader), false, false, false, false, LifeTime.ContainerControlledLifetimeManager)]
    public class cDefaultDataLoaderManager : cBaseDataManager, IDefaultDataLoader
    {
        public cLanguageDataLoader LanguageDataLoader { get; set; }
        public cLanguageDataManager LanguageDataManager { get; set; }

        public cGlobalParamsDataLoader GlobalParamsDataLoader { get; set; }
        public cRoleDataLoader RoleDataLoader { get; set; }
        public cMenuDataLoader MenuDataLoader { get; set; }
        public cPageDataLoader PageDataLoader { get; set; }

        public cRolePageLoader RolePageLoader { get; set; }

        public cRoleMenuLoader RoleMenuLoader { get; set; }
        public cRoleDataSourcePermissionLoader RoleDataSourcePermissionLoader { get; set; }
        public cRoleDataSourceColumnLoader RoleDataSourceColumnLoader { get; set; }

        public cDefaultUsersDataLoader DefaultUsersDataLoader { get; set; }



        public cDefaultDataLoaderManager(cDataServiceContext CoreServiceContext, cDataService _DataService, IFileDateService _FileDataService
            , cLanguageDataLoader _LanguageDataLoader
            , cGlobalParamsDataLoader _GlobalParamsDataLoader
            , cRoleDataLoader _RoleDataLoader
            , cLanguageDataManager _LanguageDataManager
            , cMenuDataLoader _MenuDataLoader
            , cPageDataLoader _PageDataLoader
            , cRolePageLoader _RolePageLoader
            , cRoleMenuLoader _RoleMenuLoader
            , cRoleDataSourcePermissionLoader _RoleDataSourcePermissionLoader
            , cRoleDataSourceColumnLoader _RoleDataSourceColumnLoader
            , cDefaultUsersDataLoader _DefaultUsersDataLoader
            )

          : base(CoreServiceContext, _DataService, _FileDataService)
        {
            LanguageDataLoader = _LanguageDataLoader;
            LanguageDataManager = _LanguageDataManager;
            RoleDataLoader = _RoleDataLoader;
            GlobalParamsDataLoader = _GlobalParamsDataLoader;
            MenuDataLoader = _MenuDataLoader;
            PageDataLoader = _PageDataLoader;
            RolePageLoader = _RolePageLoader;
            RoleMenuLoader = _RoleMenuLoader;
            RoleDataSourcePermissionLoader = _RoleDataSourcePermissionLoader;
            RoleDataSourceColumnLoader = _RoleDataSourceColumnLoader;
            DefaultUsersDataLoader = _DefaultUsersDataLoader;
        }

        public void Load()
        {
            cDatabaseContext __DatabaseContext = DataService.GetDatabaseContext();


            __DatabaseContext.Perform(() => { LanguageDataLoader.Init(); });
            __DatabaseContext.Perform(() => { GlobalParamsDataLoader.Init(); });
            __DatabaseContext.Perform(() => { RoleDataLoader.Init(); });
            __DatabaseContext.Perform(() => { PageDataLoader.Init(); });
            __DatabaseContext.Perform(() => { RolePageLoader.Init(); });
            __DatabaseContext.Perform(() => { MenuDataLoader.Init(); });
            __DatabaseContext.Perform(() => { RoleMenuLoader.Init(); });
            __DatabaseContext.Perform(() => { RoleDataSourcePermissionLoader.Init(); });
            __DatabaseContext.Perform(() => { RoleDataSourceColumnLoader.Init(); });
            __DatabaseContext.Perform(() => { DefaultUsersDataLoader.Init(); });


            __DatabaseContext.Perform(() => { LanguageDataManager.RefreshLanguageFromDB(); });
        }
    }
}

