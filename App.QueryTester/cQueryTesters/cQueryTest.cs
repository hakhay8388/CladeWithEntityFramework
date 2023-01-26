using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using Data.Domain.nDatabaseService;
using Data.Domain.nDatabaseService.nSystemEntities;
using Data.Domain.nDataService.nDataManagers;

namespace App.QueryTester.cQueryTesters
{
    public class cQueryTest : cCoreObject
    {
        public cDataService DataService { get; set; }

        public cMenuDataManager MenuDataManager { get; set; }

        public cQueryTest(cApp _App
            , cDataService _DataService
            , cMenuDataManager _MenuDataManager
        )
            : base(_App)
        {
            DataService = _DataService;
            MenuDataManager = _MenuDataManager;
        }

        public void Start()
        {
            Test0001();
        }

        public void Test0001()
        {
            cUserEntity __UserEntity = cUserEntity.Get(__Item => __Item.Email == "user@user.com").FirstOrDefault();

            __UserEntity.Load(__Item => __Item.Roles);

            List<cMenuEntity> __MenuList = MenuDataManager.GetMenuByUser(__UserEntity, "LeftMenu", null);
        }

    }
}
