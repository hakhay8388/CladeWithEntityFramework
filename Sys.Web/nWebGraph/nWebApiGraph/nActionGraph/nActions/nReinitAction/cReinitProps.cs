using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sys.Web.nUtils.nValueTypes;

using Newtonsoft.Json.Linq;

namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nLogInOutAction
{
    public class cReinitProps : cBaseProps
    {
        public virtual bool LoginState { get; set; }
        public virtual string SessionID { get; set; }
    }
}
