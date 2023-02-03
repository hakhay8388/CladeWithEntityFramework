using System;
using System.Collections.Generic;
using System.Text;

using System.Net.Http;
using Newtonsoft.Json;

namespace Bootstrapper.Core.nHandlers.nElasticSearchHandler
{
    public class JsonContent : StringContent
    {
        public JsonContent(object _Obj) :
            base(JsonConvert.SerializeObject(_Obj), Encoding.UTF8, "application/json")
        {
        }
    }
}
