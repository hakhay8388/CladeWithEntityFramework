using Bootstrapper.Core.nApplication.nCoreLoggers.nBatchJobLogger;
using Bootstrapper.Core.nApplication.nCoreLoggers.nCoreLogger;
using Bootstrapper.Core.nApplication.nCoreLoggers.nRequestPerformanceLogger;
using Bootstrapper.Core.nApplication.nCoreLoggers.nSqlLogger;
using Bootstrapper.Core.nAttributes;
using Bootstrapper.Core.nCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrapper.Core.nApplication.nCoreLoggers
{
    public class cLoggers :cCoreObject
    {
        public cCoreLogger CoreLogger {get;set;}

        public cCoreSqlLogger SqlLogger { get; set; }

        public cSqlGlobalInfoLogger SqlGlobalInfoLogger { get; set; }

        public cCoreRequestPerformanceLogger RequestPerformanceLogger { get; set; }
        
        public cCoreBatchJobLogger BatchJobLogger { get; set; }


        public cLoggers(cApp _App)
            :base(_App)
        {
            CoreLogger = new cCoreLogger(_App);
            SqlLogger = new cCoreSqlLogger(_App);
            SqlGlobalInfoLogger = new cSqlGlobalInfoLogger(_App);
            RequestPerformanceLogger = new cCoreRequestPerformanceLogger(_App);


        }

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cLoggers>(this);

            CoreLogger.Init();
            SqlLogger.Init();
            SqlGlobalInfoLogger.Init();
            RequestPerformanceLogger.Init();

        }
    }
}
