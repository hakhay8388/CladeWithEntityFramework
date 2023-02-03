using Bootstrapper.Boundary.nCore.nLoggerType;
using System;
using System.IO;

namespace Bootstrapper.Core.nApplication.nCoreLoggers.nCoreLogger
{
    public class cCoreLogger : cBaseLogger
    {
        public cCoreLogger(cApp _App)
            : base(_App, ELoggerType.CoreLogger)
        {
        }
        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cCoreLogger>(this);
            base.Init();
        }

        protected override string LogPath()
        {
            return App.Configuration.GeneralLogPath;
        }

		protected override bool IsEnabled()
		{
			return App.Configuration.ApplicationSettings.LogGeneralEnabled;
		}
	}
}
