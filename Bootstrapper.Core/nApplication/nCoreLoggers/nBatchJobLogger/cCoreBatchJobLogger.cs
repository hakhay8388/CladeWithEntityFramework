using System;
using System.Collections.Generic;
using System.Text;

namespace Bootstrapper.Core.nApplication.nCoreLoggers.nBatchJobLogger
{
    public class cCoreBatchJobLogger : cBaseLogger
    {
        public cCoreBatchJobLogger(cApp _App)
            : base(_App)
        {
        }
        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cCoreBatchJobLogger>(this);
        }

		protected override bool IsEnabled()
		{
            return App.Configuration.ApplicationSettings.LogBatchJobEnabled;
		}

		protected override string LogPath()
        {
            return App.Configuration.BatchJobLogPath;
        }

    }
}
