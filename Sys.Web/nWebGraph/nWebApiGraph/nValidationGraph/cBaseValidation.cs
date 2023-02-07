using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using Sys.Data.nDatabaseService;

using Base.Data.nDatabaseService;

namespace Sys.Web.nWebGraph.nWebApiGraph.nValidationGraph
{
    public abstract class cBaseValidation : cCoreObject
    {
        public cBaseWebGraph WebGraph { get; set; }

        public IDataService DataService { get; set; }

        public cBaseValidation(cApp _App, cBaseWebGraph _WebGraph, IDataService _DataService)
            : base(_App)
        {
            WebGraph = _WebGraph;
            DataService = _DataService;
        }

        public object CastObject()
        {
            return this;
        }
        public Type GetInterfaceType()
        {
            Type __Type = this.GetType();
            Type[] __Interfaces = __Type.GetInterfaces();
            if (__Interfaces.Length != 2)
            {
                var __Exception = new Exception("Validatorlerde 1 den fazla interface tanımlanamaz...!");
                App.Loggers.CoreLogger.LogError(__Exception);
                throw __Exception;
            }
            return __Interfaces.Where(__Item => __Item != typeof(ICommandReceiver)).FirstOrDefault();
        }

    }
}
