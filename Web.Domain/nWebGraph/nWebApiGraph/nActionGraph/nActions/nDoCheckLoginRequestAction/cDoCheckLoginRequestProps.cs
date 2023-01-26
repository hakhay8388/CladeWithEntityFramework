using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.nUtils.nValueTypes;

using Newtonsoft.Json.Linq;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nDoCheckLoginRequestAction
{
    public class cDoCheckLoginRequestProps : cBaseProps
    {
		public virtual bool IsLogined { get; set; }
	}
}
