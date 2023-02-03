using Newtonsoft.Json;
using System;

namespace Bootstrapper.Core.nHandlers.nElasticSearchHandler
{
    public class cLogItem : cBaseLogItem
    {
        public DateTime LogTime { get; set; }
        public string LogValue { get; set; }
    }
}
