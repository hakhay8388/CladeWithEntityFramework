using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.nUtils.nValueTypes;

using Newtonsoft.Json.Linq;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nMenuResultAction
{
    public class cMenuItem
    {
        public string Icon { get; set; }
        public string Name { get; set; }
        public bool MainMenu { get; set; }
        public object SubMenu { get; set; }
        public string Url { get; set; }

        public bool Active { get; set; }
    }
}
