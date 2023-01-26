using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.nUtils.nValueTypes;

using Newtonsoft.Json.Linq;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nValidationResultAction
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
