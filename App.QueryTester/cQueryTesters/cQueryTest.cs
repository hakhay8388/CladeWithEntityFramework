using Base.Data.nDatabaseService;
using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using Domain.Data.nDatabaseService;
using Sys.Data.nDatabaseService;
using Sys.Data.nDatabaseService.nSystemEntities;
using Sys.Data.nDataService.nDataManagers;

namespace App.QueryTester.cQueryTesters
{
    public class cQueryTest : cCoreObject
    {
        public IDataService DataService { get; set; }

        public cMenuDataManager MenuDataManager { get; set; }

        public cQueryTest(cApp _App
            , IDataService _DataService
            , cMenuDataManager _MenuDataManager
        )
            : base(_App)
        {
            DataService = _DataService;
            MenuDataManager = _MenuDataManager;
        }

        public void Start()
        {
            //Test0001();
            Test0002();
        }

        public void Test0001()
        {
            cUserEntity __UserEntity = cUserEntity.Get(__Item => __Item.Email == "user@user.com").FirstOrDefault();

            __UserEntity.Load(__Item => __Item.Roles);

            List<cMenuEntity> __MenuList = MenuDataManager.GetMenuByUser(__UserEntity, "LeftMenu", null);
        }

        public void Test0002()
        {
            for (int i = 0; i < 1000; i++)
            {
                cSysDatabaseContext __DatabaseContext = DataService.GetDatabaseContext<cSysDatabaseContext>();
                __DatabaseContext.Perform(() =>
                {
                    cUserEntity __UserEntity = cUserEntity.GetEntityByID(1);
                    __UserEntity = __UserEntity.LockAndRefresh();

                    try
                    {
                        int __Count = Convert.ToInt32(__UserEntity.Surname);
                        __UserEntity.Surname = (__Count + 1).ToString();
                    }
                    catch(Exception _Ex)
                    {
                        __UserEntity.Surname = "0";
                    }
                    Console.WriteLine("Surname : " + __UserEntity.Surname);
                    
                    __UserEntity.Save();

                    Thread.Sleep(5000);
                });
            }
        }

        /*
        private async Task Test()
        {
            try
            {
                cSysDatabaseContext __DatabaseContext = DataService.GetDatabaseContext<cSysDatabaseContext>();
                __DatabaseContext.Perform(() =>
                {
                    cUserEntity __cUserEntity = cUserEntity.GetEntityByID(1);
                    __cUserEntity.LockAndRefresh();

                    Task.Delay(10000);

                    __cUserEntity.Surname = "DEnemeeeeee";
                    __cUserEntity.Save();




                    Console.WriteLine("Thread ID_5: " + Thread.CurrentThread.ManagedThreadId);
                });
            }
            catch (Exception ex)
            {

            }
        }*/

    }
}
