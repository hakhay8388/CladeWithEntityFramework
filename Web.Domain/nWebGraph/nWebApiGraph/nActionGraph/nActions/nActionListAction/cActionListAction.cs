using Newtonsoft.Json.Linq;
using Web.Domain.nWebGraph.nSessionManager;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommandIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Web.Domain.Controllers;
using Bootstrapper.Core.nApplication;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nActionListAction
{
    public class cActionListAction : cBaseAction, IActionWithoutProps
    {
        public cActionListAction(cApp _App, cWebGraph _WebGraph)
          : base(_App, _WebGraph, ActionIDs.ActionList)
        {
        }


        public override void Action(IController _Controller, List<cSession> _SignalSessions = null, bool _InstantSend = false)
        {
            JObject __JsonObject = new JObject();

            List<object> __Result = new List<object>();
            for (int i = 0; i < CommandIDs.TypeList.Count; i++)
            {
                CommandIDs __CommandID = CommandIDs.TypeList[i];
                Type __CommandDataClass = Type.GetType("Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.n" + __CommandID.Name + "Command.c" + __CommandID.Name + "CommandData");

                FieldInfo[] __FieldInfos = __CommandDataClass.GetFields();

                List<string> __Parameters = new List<string>();
                foreach (FieldInfo __FieldInfo in __FieldInfos)
                {
                    __Parameters.Add(__FieldInfo.Name);
                    __Parameters.Add(__FieldInfo.FieldType.Name);
                }

                __Result.Add(new { Name = __CommandID.Name, ID = __CommandID.ID, Info = __CommandID.Info, Enabled = __CommandID.Enabled, Parameters = __Parameters });
            }

            __JsonObject["ActionList"] = JArray.FromObject(__Result);
            base.Action(_Controller, __JsonObject, _SignalSessions, _InstantSend);
        }
    }
}
