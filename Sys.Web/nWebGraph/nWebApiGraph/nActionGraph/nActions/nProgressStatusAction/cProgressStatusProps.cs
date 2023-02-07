using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sys.Web.nUtils.nValueTypes;
using Newtonsoft.Json.Linq;

namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nProgressStatusAction
{
    public class cProgressStatusProps : cBaseProps
    {
        public virtual string ProgressProcessName { get; set; }
        public virtual int ProgressPercentage { get; set; }
        public virtual string CompletedOperation { get; set; }

    }
}
