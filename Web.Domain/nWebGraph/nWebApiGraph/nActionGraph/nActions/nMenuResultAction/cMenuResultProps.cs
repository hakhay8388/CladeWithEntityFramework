using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.nUtils.nValueTypes;

using Newtonsoft.Json.Linq;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nMenuResultAction
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
