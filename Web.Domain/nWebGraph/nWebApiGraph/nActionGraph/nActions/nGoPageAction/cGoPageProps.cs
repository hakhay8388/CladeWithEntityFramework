using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.nUtils.nValueTypes;

using Newtonsoft.Json.Linq;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nGoPageAction
{
    public class cGoPageProps : cBaseProps
    {
        public virtual object Page { get; set; }
        public virtual string Params { get; set; }
    }
}
