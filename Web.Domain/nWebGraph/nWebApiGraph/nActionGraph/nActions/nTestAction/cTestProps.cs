using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nTestAction
{
    public class cTestProps : cBaseProps
    {
        public virtual object Page { get; set; }
        public virtual string Params { get; set; }
    }
}
