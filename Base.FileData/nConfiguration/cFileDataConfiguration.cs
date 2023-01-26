using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bootstrapper.Boundary.nCore.nBootType;
using Bootstrapper.Core.nApplication.nConfiguration;

namespace Base.FileData.nConfiguration
{
    public class cFileDataConfiguration : cConfiguration
    {
        public string FileDataPath { get; set; }
        public cFileDataConfiguration(EBootType _BootType)
            :base(_BootType)
        {
        }

        public override void Init()
        {
            base.Init();
            FileDataPath = GetVariableName(() => FileDataPath);
            FileDataPath = Path.Combine(HomePath, FileDataPath);

            App.Handlers.FileHandler.MakeDirectory(App.Cfg<cFileDataConfiguration>().FileDataPath, true);
        }
    }
}
