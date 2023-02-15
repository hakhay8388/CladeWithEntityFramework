using Base.Data.nDatabaseService;
using Base.Data.nDatabaseService.nDatabase;
using Base.FileData;
using Bootstrapper.Boundary.nCore.nObjectLifeTime;
using Bootstrapper.Core.nApplication.nStarter;
using Bootstrapper.Core.nAttributes;
using Sys.Boundary.nDefaultValueTypes;
using Sys.Data.nDatabaseService;
using Sys.Data.nDataService.nDataManagers.nLoaders;

namespace Sys.Data.nDataService.nDataManagers
{
    [Register(typeof(ISysDefaultDataLoader), false, false, false, false, LifeTime.ContainerControlledLifetimeManager)]
    public class cDefaultDataLoaderManager : cBaseDataManager, ISysDefaultDataLoader
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



        public cDefaultDataLoaderManager(cDataServiceContext CoreServiceContext, IDataService _DataService, IFileDateService _FileDataService
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
            Type __Type = App.Handlers.AssemblyHandler.GetTypeFromBaseInDomainHierarchy<DefaultGlobalParamsIDs>();
            __Type.GetMethod("Init").Invoke(null, null);

            __Type = App.Handlers.AssemblyHandler.GetTypeFromBaseInDomainHierarchy<RoleIDs>();
            __Type.GetMethod("Init").Invoke(null, null);

            __Type = App.Handlers.AssemblyHandler.GetTypeFromBaseInDomainHierarchy<DataSourceIDs>();
            __Type.GetMethod("Init").Invoke(null, null);

            __Type = App.Handlers.AssemblyHandler.GetTypeFromBaseInDomainHierarchy<MenuIDs>();
            __Type.GetMethod("Init").Invoke(null, null);

            __Type = App.Handlers.AssemblyHandler.GetTypeFromBaseInDomainHierarchy<PageIDs>();
            __Type.GetMethod("Init").Invoke(null, null);




            cSysDatabaseContext __DatabaseContext = DataService.GetDatabaseContext<cSysDatabaseContext>();


            __DatabaseContext.Perform(() => { LanguageDataLoader.Init(); });
            __DatabaseContext.Perform(() => { GlobalParamsDataLoader.Init(); });
            __DatabaseContext.Perform(() => { PageDataLoader.Init(); });
            __DatabaseContext.Perform(() => { RoleDataLoader.Init(); });
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

