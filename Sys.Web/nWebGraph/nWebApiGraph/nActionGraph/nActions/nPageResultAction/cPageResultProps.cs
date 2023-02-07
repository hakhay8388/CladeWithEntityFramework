using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sys.Web.nUtils.nValueTypes;

using Newtonsoft.Json.Linq;

namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nPageResultAction
{
    public class cPageResultProps : cBaseProps
    {
        public virtual List<cPageItem> PagesItems { get; set; }

		public cPageResultProps()
		{
			PagesItems = new List<cPageItem>();
		}
	}
}
