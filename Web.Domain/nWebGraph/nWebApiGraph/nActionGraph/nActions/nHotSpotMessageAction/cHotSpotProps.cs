using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.nUtils.nValueTypes;
using Newtonsoft.Json.Linq;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nHotSpotMessageAction
{
    public class cHotSpotProps : cBaseProps
    {
        public virtual string Header { get; set; }
        public virtual EColorTypes ColorType { get; set; }
        public virtual string Message { get; set; }
        public virtual int DurationMS { get; set; }
        public virtual int WaitTime { get; set; }

    }
}
