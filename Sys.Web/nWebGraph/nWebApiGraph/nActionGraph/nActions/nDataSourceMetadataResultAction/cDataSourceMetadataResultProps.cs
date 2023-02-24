using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sys.Web.nUtils.nValueTypes;

using Newtonsoft.Json.Linq;

namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nDataSourceMetadataResultAction
{
    public class cDataSourceMetadataResultProps : cBaseProps
    {
        public virtual IList ResultList { get; set; }
    }
}
