using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sys.Web.nUtils.nValueTypes;
using Newtonsoft.Json.Linq;

namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nDebugAlertAction
{
    public class cDebugAlertProps : cBaseProps
    {
		public virtual string Header { get; set; }
		public virtual string Message { get; set; }
    }
}
