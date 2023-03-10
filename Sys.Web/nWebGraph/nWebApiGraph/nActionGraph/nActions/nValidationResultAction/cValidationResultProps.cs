using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sys.Web.nUtils.nValueTypes;

using Newtonsoft.Json.Linq;

namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nValidationResultAction
{
    public class cValidationResultProps : cBaseProps
    {
        public virtual List<cValidationItem> ValidationItems { get; set; }

		public cValidationResultProps()
		{
			ValidationItems = new List<cValidationItem>();
		}
	}
}
