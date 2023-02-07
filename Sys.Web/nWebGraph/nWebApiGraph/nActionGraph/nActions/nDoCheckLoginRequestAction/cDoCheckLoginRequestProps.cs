using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sys.Web.nUtils.nValueTypes;

using Newtonsoft.Json.Linq;

namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nDoCheckLoginRequestAction
{
    public class cDoCheckLoginRequestProps : cBaseProps
    {
		public virtual bool IsLogined { get; set; }
	}
}
