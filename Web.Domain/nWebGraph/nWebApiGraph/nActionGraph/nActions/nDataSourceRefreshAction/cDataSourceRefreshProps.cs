using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.nUtils.nValueTypes;

using Data.Domain.nDefaultValueTypes;
using Newtonsoft.Json.Linq;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nDataSourceRefreshAction
{
    public class cDataSourceRefreshProps : cBaseProps
    {
        public virtual DataSourceIDs DataSource { get; set; }
    }
}
