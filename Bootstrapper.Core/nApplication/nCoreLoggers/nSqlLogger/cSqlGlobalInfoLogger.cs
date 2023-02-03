﻿using Bootstrapper.Boundary.nCore.nLoggerType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bootstrapper.Core.nApplication.nCoreLoggers.nSqlLogger
{
    public class cSqlGlobalInfoLogger : cBaseLogger
    {
        public cSqlGlobalInfoLogger(cApp _App)
            : base(_App, ELoggerType.SqlGlobalInfoLogger)
        {
        }
        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cSqlGlobalInfoLogger>(this);
            base.Init();
        }

        protected override string LogPath()
        {
            return App.Configuration.SqlGlobalInfoLogPath;
        }


		protected override bool IsEnabled()
		{
			return App.Configuration.ApplicationSettings.LogSqlGlobalInfoEnabled;
		}
	}
}
