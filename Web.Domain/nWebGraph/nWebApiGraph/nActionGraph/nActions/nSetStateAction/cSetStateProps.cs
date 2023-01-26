using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.nUtils.nValueTypes;

using Newtonsoft.Json.Linq;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSetStateAction
{
    public class cSetStateProps : cBaseProps
    {
        public virtual string ObjectTypeName { get; set; }
        public virtual string Name { get; set; }
        public virtual object Value { get; set; }
    }
}
