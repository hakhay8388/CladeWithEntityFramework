using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sys.Web.Controllers;
using Sys.Web.nUtils.nValueTypes;
using Sys.Web.nWebGraph.nSessionManager;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph;

using Newtonsoft.Json.Linq;

namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nShowMessageAction
{
    public class cMessageWithRequestObjectProps<T> : cBaseMessageProps<T>
    {
        public cMessageWithRequestObjectProps()
            :base()
        {
        }
    }
}
