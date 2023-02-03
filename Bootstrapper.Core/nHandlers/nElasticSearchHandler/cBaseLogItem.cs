using Newtonsoft.Json;
using System;

namespace Bootstrapper.Core.nHandlers.nElasticSearchHandler
{
    public class cBaseLogItem
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
