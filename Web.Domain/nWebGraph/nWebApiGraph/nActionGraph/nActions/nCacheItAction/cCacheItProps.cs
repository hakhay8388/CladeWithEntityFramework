using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.nUtils.nValueTypes;
using Newtonsoft.Json.Linq;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nCacheItAction
{
    public class cCacheItProps : cBaseProps
    {
        public virtual JArray CacheActionList { get; set; }
    }
}
