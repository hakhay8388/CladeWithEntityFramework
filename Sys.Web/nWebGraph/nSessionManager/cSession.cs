using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using Sys.Web.Controllers;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommandIDs;
using Sys.Boundary.nDefaultValueTypes;

using System;
using System.Collections.Generic;
using Sys.Data.nDatabaseService.nSystemEntities;

namespace Sys.Web.nWebGraph.nSessionManager
{
    public class cSession : cCoreObject
    {
        public DateTime CreateTime { get; set; }
        public string SessionID { get; set; }
        public string IpAddress { get; set; }

        public List<string> SignalRIDList { get; set; }

        public cUserEntity User { get; private set; }
        public Dictionary<int, object> MapedValues { get; set; }


        public cSession(cApp _App, IController _BaseController, string _SessionID)
            : base(_App)
        {
            SessionID = _SessionID;
            IpAddress = _BaseController.CurrentContext.Connection.RemoteIpAddress.ToString();
            SignalRIDList = new List<string>();
            MapedValues = new Dictionary<int, object>();
        }

       

        string m_Language { get; set; }
        public string Language
        {
            get
            {
                if (IsLogined)
                {
                    return User.Language;
                }
                if (string.IsNullOrEmpty(m_Language))
                {
                    m_Language = App.Handlers.LanguageHandler.LanguageNameList[0].Code;
                }
                return m_Language;
            }
            set
            {
                m_Language = value;
            }
        }
        public void RefreshValue(IController _BaseController)
        {
            CreateTime = DateTime.Now;
        }

        public void SetUser(cUserEntity _User)
        {
            _User.LoadInnerDatas();
            User = _User;
        }

        public bool IsLogined
        {
            get
            {
                return User != null;
            }
        }

        public void Logout()
        {
            SessionID = "";
            //SignalRIDList.Clear();
            User = null;
        }

        public bool IsUsableForMe(ECommandType _CommandID)
        {
            if (!IsLogined && _CommandID != null && _CommandID.MainRoles.Exists(__Item => __Item.Code == RoleIDs.Unlogined.Code))
            {
                return true;
            }
            else if (IsLogined && _CommandID != null)
            {
                List<cRoleEntity> __ActorRoles = User.Roles.ToList();
                for (int i = 0; i < _CommandID.MainRoles.Count; i++)
                {
                    if (__ActorRoles.Exists(__Item => __Item.Code == _CommandID.MainRoles[i].Code))
                    {
                        return true;
                    }
                }
                return false;
            }
            return false;
        }
    }
}
