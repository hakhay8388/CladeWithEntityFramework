using System;
using System.Collections.Generic;
using System.Text;

namespace Bootstrapper.Core.nApplication.nCoreLoggers.nRequestPerformanceLogger
{
    public class cCoreRequestPerformanceLogger : cBaseLogger
    {
        public cCoreRequestPerformanceLogger(cApp _App)
            : base(_App)
        {
        }
        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cCoreRequestPerformanceLogger>(this);
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
