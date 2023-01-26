using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.nUtils.nValueTypes;
using Newtonsoft.Json.Linq;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nProgressStatusAction
{
    public class cProgressStatusProps : cBaseProps
    {
        public virtual string ProgressProcessName { get; set; }
        public virtual int ProgressPercentage { get; set; }
        public virtual string CompletedOperation { get; set; }

    }
}
