using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sys.Web.nUtils.nValueTypes;

using Newtonsoft.Json.Linq;

namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nPageResultAction
{
    public class cPageItem
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string OriginalCode { get; set; }

        public string[] SubParamName { get; set; }

        public string Component { get; set; }

        public bool IsMainPage { get; set; }
    }
}
