using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;

namespace Bootstrapper.Core.nHandlers.nEmailHandler
{
    public interface IEmailConfiguration
    {
        cEmailConfiguration GetEmailConfiguration();
    }
}
