using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sys.Web.Controllers
{
    public interface IGlobalSignalReceiver
    {
        void OnConnected(IController _Controller);

        void OnDisconnected(IController _Controller);
    }
}
