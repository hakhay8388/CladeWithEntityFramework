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
    public class cEmailConfiguration : cCoreObject
    {
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public string EmailDisplayName { get; set; }
        public bool SmtpSslEnabled { get; set; }
        public string MailFrom { get; set; }
        public cEmailConfiguration(nApplication.cApp _App, string _SmtpHost, int _SmtpPort, string _SmtpUsername, string _SmtpPassword, string _EmailDisplayName, bool _SmtpSslEnabled, string _MailFrom)
            : base(_App)
        {
            SmtpHost = _SmtpHost;
            SmtpPort = _SmtpPort;
            SmtpUsername = _SmtpUsername;
            SmtpPassword = _SmtpPassword;
            EmailDisplayName = _EmailDisplayName;
            SmtpSslEnabled = _SmtpSslEnabled;
            MailFrom = _MailFrom;
        }


    }
}
