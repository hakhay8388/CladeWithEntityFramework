using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions
{
    public interface IActionProps
    {
        JObject SerializeObject();
    }
}

