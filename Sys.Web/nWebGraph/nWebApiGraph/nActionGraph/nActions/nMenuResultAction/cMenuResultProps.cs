using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sys.Web.nUtils.nValueTypes;

using Newtonsoft.Json.Linq;

namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nMenuResultAction
{
    public class cMenuResultProps : cBaseProps
    {
        public virtual List<cMenuItem> MenuItems { get; set; }

		public cMenuResultProps()
		{
			MenuItems = new List<cMenuItem>();
		}
	}
}
