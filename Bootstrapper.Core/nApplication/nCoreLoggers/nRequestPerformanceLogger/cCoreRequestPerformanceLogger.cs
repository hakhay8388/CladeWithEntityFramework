using Bootstrapper.Boundary.nCore.nLoggerType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bootstrapper.Core.nApplication.nCoreLoggers.nRequestPerformanceLogger
{
    public class cCoreRequestPerformanceLogger : cBaseLogger
    {
        public cCoreRequestPerformanceLogger(cApp _App)
            : base(_App, ELoggerType.RequestPerformanceLogger)
        {
        }
        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cCoreRequestPerformanceLogger>(this);
            base.Init();
        }

        protected override string LogPath()
        {
            return App.Configuration.RequestPerformanceLogPath;
        }

		protected override bool IsEnabled()
		{
			return App.Configuration.ApplicationSettings.RequestPerformanceLogPath;
		}
	}
}
