using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.nUtils.nValueTypes;

using Newtonsoft.Json.Linq;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nPageResultAction
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
