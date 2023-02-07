using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sys.Web.nUtils.nValueTypes;

using Newtonsoft.Json.Linq;

namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSetClientLanguageAction
{
    public class cSetClientLanguageProps : cBaseProps
    {
        public virtual string LanguageCode { get; set; }
        public virtual JObject Language { get; set; }
    }
}
