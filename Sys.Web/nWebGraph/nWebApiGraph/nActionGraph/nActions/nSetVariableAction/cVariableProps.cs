using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sys.Web.nUtils.nValueTypes;

using Newtonsoft.Json.Linq;

namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSetVariableAction
{
    public class cVariableProps : cBaseProps
    {
        public virtual string ObjectTypeName { get; set; }
        public virtual string Name { get; set; }
        public virtual object Value { get; set; }
        public virtual bool ForceUpdate { get; set; }
    }
}
