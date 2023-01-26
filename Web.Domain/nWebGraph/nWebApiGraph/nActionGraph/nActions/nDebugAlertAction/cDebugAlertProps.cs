using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.nUtils.nValueTypes;
using Newtonsoft.Json.Linq;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nDebugAlertAction
{
    public class cDebugAlertProps : cBaseProps
    {
		public virtual string Header { get; set; }
		public virtual string Message { get; set; }
    }
}
