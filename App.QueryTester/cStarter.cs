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


using Sys.Boundary.nDefaultValueTypes;
using System.Xml.Linq;
using Domain.Data.nDatabaseService;
using Base.Data.nDatabaseService;
using Sys.Data.nDatabaseService.nParams;
using Web.Domain.nWebGraph;
using Sys.Web.nWebGraph;

namespace App.QueryTester
{
    public class cStarter : cCoreObject, IStarter
    {
        public IDataService DataService { get; set; }
        public cBaseWebGraph WebGraph { get; set; }
        public cStarter(cApp _App, IDataService _DataService, cBaseWebGraph _WebGraph)
            : base(_App)
        {
            DataService = _DataService;
            WebGraph = _WebGraph;
        }

        public void Start(cApp _App)
        {
            DataService.Migrate();
            WebGraph.ComponentManager.Load();

            WebGraph.ComponentManager.Load();
            if (App.Configuration.LoadSysDefaultDataOnStart) DataService.LoadSysDefaultData();
            if (App.Configuration.LoadDomainDefaultDataOnStart) DataService.LoadDomainDefaultData();
            if (App.Configuration.LoadBatchJobOnStart) DataService.LoadBatchJob();
            if (App.Configuration.LoadGlobalParamsOnStart) Params.GlobalParams.LoadGlobalParams();
            Console.WriteLine("Test");
        }
    }
}
