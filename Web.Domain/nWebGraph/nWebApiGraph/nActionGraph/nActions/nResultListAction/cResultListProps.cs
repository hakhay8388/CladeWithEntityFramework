using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.nUtils.nValueTypes;

using Newtonsoft.Json.Linq;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nResultListAction
{
    public class cResultListProps : cBaseProps
    {
        public virtual int Total { get; set; }
        public virtual int Page { get; set; }
        public virtual IList ResultList { get; set; }

        /*public override JObject SerializeObject()
        {
            JArray __List = JArray.FromObject(ResultList);
            JObject __JObject =  new JObject();
            __JObject["ResultList"] = __List;
            return __JObject;
        }*/
    }
}
