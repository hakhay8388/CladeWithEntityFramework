using Bootstrapper.Boundary.nCore.nLoggerType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bootstrapper.Core.nApplication.nCoreLoggers.nSqlLogger
{
    public class cCoreSqlLogger : cBaseLogger
    {
        public cCoreSqlLogger(cApp _App)
            : base(_App, ELoggerType.CoreSqlLogger)
        {
        }
        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cCoreSqlLogger>(this);
            base.Init();
        }

        protected override string LogPath()
        {
            return App.Configuration.ExecutedSqlLogPath;
        }


		protected override bool IsEnabled()
		{
			return App.Configuration.ApplicationSettings.LogExecutedSqlEnabled;
		}
	}
}
