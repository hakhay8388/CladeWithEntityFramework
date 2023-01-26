using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph
{
    public class cListenerEvent
    {
        public cBaseCommand SenderCommand { get; private set; }
        public bool IsPropogationStoped { get; private set; }
        public Dictionary<string, object> Values { get; private set; }

        public cListenerEvent(cBaseCommand _SenderCommand)
        {
            SenderCommand = _SenderCommand;
            IsPropogationStoped = false;
            Values = new Dictionary<string, object>();
        }

        public void StopPropogation()
        {
            IsPropogationStoped = true;
        }
    }
}
