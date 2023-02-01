using Bootstrapper.Core.nApplication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSetServerDateTimeAction;
using Web.Domain.nWebGraph;
using Data.Domain.nDatabaseService;
using Microsoft.AspNetCore.SignalR;
using Web.Domain.Controllers;
using Web.Domain.nWebGraph.nSessionManager;
using Newtonsoft.Json;
using System.Dynamic;

namespace Web.Domain.Controllers
{
    
    [Route("[controller]")]
    public class WebGraphController : cBaseController
    {


        public WebGraphController(cApp _App, cWebGraph _WebGraph, cDataService _DataService, IHubContext<SignalRHub> _SignalHub)
            : base(_App, _WebGraph, _DataService, _SignalHub)
        {
        }


        [HttpPost("[action]")]
        public JsonResult WebApi()
        {
            Stream ___Request = Request.Body;
            if (___Request.CanSeek)
            {
                ___Request.Seek(0, System.IO.SeekOrigin.Begin);
            }
            string __JSON = new StreamReader(___Request).ReadToEnd();

            //dynamic x = JsonConvert.DeserializeObject(__JSON);
            CommandJson = JObject.Parse(__JSON);

            WebGraph.CommandGraph.InterpreterCommand(this);
            try
            {
                WebGraph.ActionGraph.SetServerDateTimeAction.Action(this, new cSetServerDateTimeProps() { ServerDate = App.Handlers.DateTimeHandler.Now });

                WebGraph.NotificationManager.StartNotificationBroadCaster(this);

                SignalSessions.ForEach(__Item =>
                {
                    if (__Item.Session == null)
                    {
                        WebGraph.SessionManager(CurrentContext).GetSessionList().ForEach(__SessionItems =>
                        {
                            //SignalHub.Clients.All.SendAsync("CommandChannel", __Item.ActionJson.ToString());
                            if (__SessionItems.IsLogined)
                            {
                                foreach (string __SignalRID in __SessionItems.SignalRIDList)
                                {
                                    SignalHub.Clients.Client(__SignalRID).SendAsync("CommandChannel", __Item.ActionJson.ToString());
                                }
                            }
                        });
                    }
                    else
                    {
                        if (__Item.Session.IsLogined)
                        {
                            foreach (string __SignalRID in __Item.Session.SignalRIDList)
                            {
                                SignalHub.Clients.Client(__SignalRID).SendAsync("CommandChannel", __Item.ActionJson.ToString());
                            }
                        }
                    }
                });
            }
            catch (Exception _Ex)
            {
                WebGraph.ErrorMessageManager.ErrorAction(_Ex, this, this.GetWordValue("Error"), this.GetWordValue("UnknownError"));
            }

            return Json(ActionJson);
        }
    }
}