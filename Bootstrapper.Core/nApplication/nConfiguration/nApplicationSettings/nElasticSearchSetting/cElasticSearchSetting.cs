using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrapper.Core.nApplication.nConfiguration.nApplicationSettings.nElasticSearchSetting
{
    public class cElasticSearchSetting
    {
        public bool LogToElastic { get; set; }
        public string ElasticUrl { get; set; }
        public string ElasticIndexPrefix { get; set; }

    }
}
