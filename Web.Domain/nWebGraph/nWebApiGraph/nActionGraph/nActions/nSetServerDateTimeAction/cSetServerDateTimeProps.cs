using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.nUtils.nValueTypes;

using Newtonsoft.Json.Linq;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSetServerDateTimeAction
{
    public class cSetServerDateTimeProps : cBaseProps
    {
        public virtual DateTime ServerDate { get; set; }
    }
}
