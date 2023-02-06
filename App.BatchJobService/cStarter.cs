using Bootstrapper.Core.nApplication.nStarter;
using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Sys.Data.nDatabaseService;


using Sys.Boundary.nDefaultValueTypes;
using Domain.Data.nDatabaseService;
using Base.Data.nDatabaseService;

namespace Web.Domain
{
    public class cStarter : cCoreObject, IStarter
    {
        public IDataService DataService { get; set; }
        public cStarter(cApp _App, IDataService _DataService)
            : base(_App)
        {
            DataService = _DataService;
        }

        public void Start(cApp _App)
        {
            DataService.Migrate();
            DataService.ComponentLoad();
            if (App.Configuration.LoadSysDefaultDataOnStart) DataService.LoadSysDefaultData();
            if (App.Configuration.LoadDomainDefaultDataOnStart) DataService.LoadDomainDefaultData();
            if (App.Configuration.LoadBatchJobOnStart) DataService.LoadBatchJob();
            if (App.Configuration.LoadGlobalParamsOnStart) Params.GlobalParams.LoadGlobalParams();


            //cDomainDatabaseContext __DatabaseContext = DataService.CastAndGetDatabaseContext<cDomainDatabaseContext>();

            /*__DatabaseContext.Perform(() =>
            {
                cUserEntity __UserEntity = new cUserEntity
                {
                    Name = "Hayri",
                    Surname = "Eryürek",
                    Email = "hayhay8388@hotmail.com",
                    Language = "tr",
                    Password = "1",
                    State = UserStateIDs.Active.ID,
                    UserDetail = new cUserDetailEntity()
                    {
                        Telephone = "1111",
                        DateOfBirth = DateTime.Now,
                        Gender = GenderIDs.Man.ID
                    }
                   ,
                    Roles = new List<cRoleEntity>()
                    {

                    }

                };
                cUserEntity.Add(__UserEntity);
                __DatabaseContext.SaveChanges();

            });*/

            Console.WriteLine("Test");
        }
    }
}
