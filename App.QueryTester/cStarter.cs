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
using Data.Domain.nDatabaseService;


using Data.Domain.nDefaultValueTypes;
using System.Xml.Linq;
using Data.Domain.nDatabaseService.nEntities;

namespace App.QueryTester
{
    public class cStarter : cCoreObject, IStarter
    {
        public cDataService DataService { get; set; }
        public cStarter(cApp _App, cDataService _DataService)
            : base(_App)
        {
            DataService = _DataService;
        }

        public void Start(cApp _App)
        {
            DataService.Migrate();
            DataService.ComponentLoad();
            if (App.Configuration.LoadDefaultDataOnStart) DataService.LoadDefaultData();
            if (App.Configuration.LoadBatchJobOnStart) DataService.LoadBatchJob();
            if (App.Configuration.LoadGlobalParamsOnStart) DataService.GlobalParams.LoadGlobalParams();

            cDatabaseContext __DatabaseContext = DataService.GetDatabaseContext();

            Console.WriteLine("Test");
        }
    }
}
