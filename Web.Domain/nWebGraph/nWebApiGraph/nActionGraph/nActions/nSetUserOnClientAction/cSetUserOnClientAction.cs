using Bootstrapper.Core.nApplication;
using Data.Domain.nDatabaseService.nSystemEntities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.Controllers;
using Web.Domain.nWebGraph.nSessionManager;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nLogInOutAction;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSetUserOnClientAction
{
    public class cSetUserOnClientAction : cBaseAction, IActionWithoutProps
    {
        public cSetUserOnClientAction(cApp _App, cWebGraph _WebGraph)
           : base(_App, _WebGraph, ActionIDs.SetUserOnClient)
        {

        }

        public override void Action(IController _Controller = null, List<cSession> _SignalSessions = null, bool _InstantSend = false)
        {
            if (_Controller.ClientSession.IsLogined)
            {
                _Controller.ClientSession.User.Load(__Item => __Item.Roles);
                _Controller.ClientSession.User.Load(__Item => __Item.UserDetail);
            }

            cSetUserOnClientProps __SetUserOnClientProps = new cSetUserOnClientProps();
            __SetUserOnClientProps.User = _Controller.ClientSession.User != null ? _Controller.ClientSession.User : null;

            JObject __JsonObject = __SetUserOnClientProps.SerializeObject();
            if (__JsonObject["User"].HasValues)
            {
                __JsonObject["User"]["Password"] = null;
            }

            base.Action(_Controller, __JsonObject, _SignalSessions, _InstantSend);
        }

        public void Action(IController _Controller, cUserEntity _UserEntity, List<cSession> _SignalSessions)
        {
            cSetUserOnClientProps __SetUserOnClientProps = new cSetUserOnClientProps();

            _UserEntity.Load(__Item => __Item.Roles);
            _UserEntity.Load(__Item => __Item.UserDetail);

            __SetUserOnClientProps.User = _Controller.ClientSession.User != null ? _Controller.ClientSession.User : null;

            JObject __JsonObject = __SetUserOnClientProps.SerializeObject();
            if (__JsonObject["User"].HasValues)
            {
                __JsonObject["User"]["Password"] = null;
            }

            base.Action(_Controller, __JsonObject, _SignalSessions, true);

            
            
            /*


            cSetUserOnClientProps __LogInOutProps = new cSetUserOnClientProps();

            __LogInOutProps.User = _UserEntity.ToDynamic();

            JObject __JsonObject = __LogInOutProps.SerializeObject();
            if (__JsonObject["User"].HasValues)
            {
                SetJObjectContent(_Controller, __JsonObject);
                __JsonObject["User"]["Password"] = null;
                __JsonObject["User"]["PaymentCardDetailEntity"] = null;
                __JsonObject["User"]["PaymentIyzicoInstructionEntity"] = null;
                __JsonObject["User"]["UserPasswordChange"] = null;
                __JsonObject["User"]["UserPasswordChangeRequest"] = null;

                __JsonObject["User"]["Roles"] = JArray.FromObject(_UserEntity.Actor.GetValue().Roles.ToDynamicObjectList());
                __JsonObject["User"]["UserDetail"] = JObject.FromObject(_UserEntity.UserDetail.ToDynamic());
            }

            base.Action(_Controller, __JsonObject, _SignalSessions, true);*/
        }

    }
}
