using Base.Data.nConfiguration;
using Base.FileData.nConfiguration;
using Base.FileData.nFileDataService;
using Bootstrapper.Boundary.nCore.nBootType;
using System;
using System.Collections.Generic;
using System.IO;

namespace Data.Domain.nConfiguration
{
    public class cDomainDataConfiguration : cDataConfiguration
    {
        public cDomainDataConfiguration(EBootType _BootType)
            :base(_BootType)
        {
            LoadDefaultDataOnStart = true;
            LoadGlobalParamsOnStart = true;
        }

        public override void Init()
        {
            base.Init();
        }

    }
}
