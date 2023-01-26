using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.Controllers;
using Web.Domain.nUtils.nValueTypes;
using Web.Domain.nWebGraph.nSessionManager;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nAttributes;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph;

using Newtonsoft.Json.Linq;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nShowMessageAction
{
    public class cMessageWithRequestObjectProps<T> : cBaseMessageProps<T>
    {
        public cMessageWithRequestObjectProps()
            :base()
        {
        }
    }
}
