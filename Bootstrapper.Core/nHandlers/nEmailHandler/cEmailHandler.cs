using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nApplication.nCoreLoggers;
//using Bootstrapper.Core.nApplication.nCoreLoggers.nMethodCallLogger;
using Bootstrapper.Core.nAttributes;
using Bootstrapper.Core.nCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrapper.Core.nHandlers.nEmailHandler
{
    public class cEmailHandler : cCoreObject
    {

        public cEmailConfiguration EmailConfiguration { get; private set; }

        public cEmailHandler(nApplication.cApp _App)
            : base(_App)
        {

        }

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cEmailHandler>(this);
        }

        private bool RemoteServerCertificateValidationCallback(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        public void ReloadConfig()
        {
            IEmailConfiguration __EmailConfiguration = App.Factories.ObjectFactory.ResolveInstance<IEmailConfiguration>();
            if (__EmailConfiguration != null)
            {
                EmailConfiguration = __EmailConfiguration.GetEmailConfiguration();
            }
        }

        public bool SendMail(string _MessageTo, string _Subject, string _Body, List<string> _Attachments = null, string _ReplyTo = "")
        {
            if (EmailConfiguration == null)
            {
                IEmailConfiguration __EmailConfiguration = App.Factories.ObjectFactory.ResolveInstance<IEmailConfiguration>();
                if (__EmailConfiguration != null)
                {
                    EmailConfiguration = __EmailConfiguration.GetEmailConfiguration();
                }
                else
                {
                    return false;
                }
            }

            SmtpClient __SmtpClient = new SmtpClient(EmailConfiguration.SmtpHost, EmailConfiguration.SmtpPort);
            __SmtpClient.Credentials = new System.Net.NetworkCredential(EmailConfiguration.SmtpUsername, EmailConfiguration.SmtpPassword);
            __SmtpClient.EnableSsl = false;
            MailMessage message = GetMessage(EmailConfiguration.EmailDisplayName, _MessageTo, _Subject, _Body, _Attachments, _ReplyTo);
            if (EmailConfiguration.SmtpSslEnabled)
            {
                __SmtpClient.EnableSsl = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; 
            }
            if (message == null)
            {
                return false;
            }

            try
            {
                __SmtpClient.Send(message);
                return true;
            }
            catch (Exception _Ex)
            {
				App.Loggers.CoreLogger.LogError(_Ex);
				throw (_Ex); 
            }

        }
        private MailMessage GetMessage(string _EmailDisplayName, string _MessageTo, string _Subject, string _Body, List<string> _Attachments, string _ReplyTo)
        {

            if (EmailConfiguration == null)
            {
                IEmailConfiguration __EmailConfiguration = App.Factories.ObjectFactory.ResolveInstance<IEmailConfiguration>();
                if (__EmailConfiguration != null)
                {
                    EmailConfiguration = __EmailConfiguration.GetEmailConfiguration();
                }
                else
                {
                    return null;
                }
            }


            if (!_MessageTo.IsNullOrEmpty())
            {
                MailMessage __MailMessage = new MailMessage();
                __MailMessage.Subject = _Subject;
                __MailMessage.IsBodyHtml = true;

                __MailMessage.From = new MailAddress(EmailConfiguration.MailFrom, _EmailDisplayName);
                string __unSubscribeUrl = "";
                if (!__unSubscribeUrl.IsNullOrEmpty())
                {
                    __MailMessage.Headers.Add("List-Unsubscribe", String.Format(
    CultureInfo.InvariantCulture, "<{0}>", __unSubscribeUrl));
                }

                __MailMessage.To.Add(_MessageTo);
                if (_Attachments != null && _Attachments.Count > 0)
                {
                    for (int i = 0; i < _Attachments.Count; i++)
                    {
                        __MailMessage.Attachments.Add(new Attachment(_Attachments[i]));
                    }

                }
                if (!_ReplyTo.IsNullOrEmpty())
                {
                    __MailMessage.ReplyToList.Add(new MailAddress(_ReplyTo));
                }
                AlternateView __plainView = AlternateView.CreateAlternateViewFromString(_Body, null, "text/plain");
                AlternateView __htmlView = AlternateView.CreateAlternateViewFromString(_Body, null, "text/html");
                __MailMessage.AlternateViews.Add(__plainView);
                __MailMessage.AlternateViews.Add(__htmlView);
                __MailMessage.Body = "";
                return __MailMessage;
            }
            return null;
        }

    }
}
