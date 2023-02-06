using Base.Data.nConfiguration;
using Base.FileData.nConfiguration;
using Base.FileData.nFileDataService;
using Bootstrapper.Boundary.nCore.nBootType;
using System;
using System.Collections.Generic;
using System.IO;

namespace Sys.Data.nConfiguration
{
    public class cSysDataConfiguration : cDataConfiguration
    {
        public cSysDataConfiguration(EBootType _BootType)
            :base(_BootType)
        {
            LoadSysDefaultDataOnStart = true;
            LoadGlobalParamsOnStart = true;
        }

        public override void Init()
        {
            base.Init();
        }

    }
}
