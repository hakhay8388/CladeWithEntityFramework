using Base.FileData.nConfiguration;
using Base.FileData.nFileDataService;
using Bootstrapper.Boundary.nCore.nBootType;
using System;
using System.Collections.Generic;
using System.IO;

namespace Base.Data.nConfiguration
{
    public class cDataConfiguration : cFileDataConfiguration
    {
        public cDataConfiguration(EBootType _BootType)
            :base(_BootType)
        {
            LoadSysDefaultDataOnStart = true;
            LoadGlobalParamsOnStart = true;
        }

        
        public override void Init()
        {
            base.Init();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

    }
}
