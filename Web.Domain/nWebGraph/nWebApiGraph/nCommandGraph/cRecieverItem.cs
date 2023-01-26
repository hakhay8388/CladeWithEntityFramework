using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph
{
    public class cRecieverItem
    {
        public Object Receiver { get; set; }
        public int Order { get; set; }
        public cRecieverItem(Object _Receiver, int _Order)
        {
            Receiver = _Receiver;
            Order = _Order;
        }
    }
}
