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
            Console.WriteLine("Thread ID_1: " + Thread.CurrentThread.ManagedThreadId);
            MainAsync();
            Task.WaitAll();
            Console.WriteLine("Thread ID_2: " + Thread.CurrentThread.ManagedThreadId);
        }

        private async Task MainAsync()
        {
            Test();
            Test();
            Test();
        }

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


                    /*__DatabaseContext.Perform(async () =>
                    {
                        Console.WriteLine("Thread ID_4: " + Thread.CurrentThread.ManagedThreadId);
                        cPaymentEntity __PaymentEntity = cPaymentEntity.Add(new cPaymentEntity()
                        {
                            Price = 250,
                            Name = "Test250",
                        });
                        __PaymentEntity.Save();
                    });*/

                    Console.WriteLine("Thread ID_5: " + Thread.CurrentThread.ManagedThreadId);
                });
            }
            catch (Exception ex)
            {

            }
        }

      
    }
}
