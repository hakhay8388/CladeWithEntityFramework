using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sys.Web.nUtils.nValueTypes;

using Sys.Boundary.nDefaultValueTypes;
using Newtonsoft.Json.Linq;

namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nDataSourceRefreshAction
{
    public class cDataSourceRefreshProps : cBaseProps
    {
        public virtual DataSourceIDs DataSource { get; set; }
    }
}
