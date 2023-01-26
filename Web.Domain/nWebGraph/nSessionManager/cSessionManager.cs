using Microsoft.AspNetCore.Http;
using Web.Domain.nWebGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Data.Domain.nDefaultValueTypes;
using Base.Data.nConfiguration;
using Bootstrapper.Core.nCore;
using Bootstrapper.Core.nApplication;
using Web.Domain.Controllers;

using Data.Domain.nDataService.nDataManagers;
using Data.Domain.nDatabaseService.nSystemEntities;

namespace Web.Domain.nWebGraph.nSessionManager
{
    public class cSessionManager : cCoreObject
    {
		public static string CookieSessionName = "GenericWebScaffoldSessionID";

		public string CurrentContext { get; set; }
        private List<cSession> SessionItems = null;

        public cSessionDataManager SessionDataManager { get; set; }
		public cWebGraph WebGraph { get; set; }


		public cSessionManager(cApp _App, cWebGraph _WebGraph, cSessionDataManager _SessionDataManager)
            : base(_App)
        {
            SessionItems = new List<cSession>();
			WebGraph = _WebGraph;
			SessionDataManager = _SessionDataManager;
        }

        public List<cSession> GetSessionItems()
        {
            return SessionItems;
        }

        public List<cSession> GetSessionByUserID(long _UserID)
        {
            lock (SessionItems)
            {
                return SessionItems.Where(__Item => __Item.User != null && __Item.User.ID == _UserID).ToList();
            }
        }

        public List<cSession> GetSessionByUserID(List<long> _UserIDList)
        {
            lock (SessionItems)
            {
                return SessionItems.Where(__Item => __Item.User != null && _UserIDList.Contains(__Item.User.ID)).ToList();
            }
        }


        public List<cSession> GetSessionList()
        {
            lock (SessionItems)
            {
                return SessionItems.CloneOnlyList().ToList();
            }
        }
        public List<long> GetAllOnlineUsers()
        {
            lock (SessionItems)
            {
                return SessionItems.Where(__Item => __Item.User != null).Select(x => x.User.ID).ToList();
            }
        }
        public List<cSession> GetSessionByUserIDs(List<long> _UserID)
        {
            lock (SessionItems)
            {
                List<cSession> __ResultList = SessionItems.Where((__Item) =>
                {
                    if (__Item.User != null)
                    {
                        int __Index = _UserID.IndexOf(__Item.User.ID);
                        return __Item.User != null && __Index > -1;
                    }
                    return false;
                }).ToList();
                return __ResultList;
            }
        }
      /*  public List<cSession> GetSessionByRole(RoleIDs _RoleValue)
        {
            List<cSession> __AllSession = GetSessionList();
            List<cSession> __RoleSession = new List<cSession>();
            for (int i = 0; i < __AllSession.Count; i++)
            {
                if (__AllSession[i].User != null && __AllSession[i].User.Roles.GetValue().Code == _RoleValue.Code)
                {
                    __RoleSession.Add(__AllSession[i]);
                }
            }
            return __RoleSession;
        }*/
        public List<cSession> GetSessionByEmail(string _Email)
        {
            lock (SessionItems)
            {
                return SessionItems.Where(__Item => __Item.User != null && __Item.User.Email == _Email).ToList();
            }
        }

        public List<cSession> GetSessionBySeesionID(string _SessionID)
        {
            lock (SessionItems)
            {
                return SessionItems.Where(__Item => !__Item.SessionID.IsNullOrEmpty() && __Item.SessionID == _SessionID).ToList();
            }
        }

        public cSession CreateSession(IController _Controller)
        {
            lock (SessionItems)
            {
                ClearUnusedSession(_Controller);

                string __SessionID = _Controller.CurrentContext.Request.Cookies[CookieSessionName];
                if (string.IsNullOrEmpty(__SessionID))
                {
                    CookieOptions __Options = new CookieOptions();
                    __Options.Expires = DateTime.Now.AddDays(365);

                    __SessionID = _Controller.CurrentContext.Session.Id;
                    _Controller.CurrentContext.Response.Cookies.Append(CookieSessionName, __SessionID, __Options);
                }

                cSession __CurrentSession = GetSessionByID(__SessionID);
                if (__CurrentSession == null)
                {
                    __CurrentSession = new cSession(App, _Controller, __SessionID);
                }

                cUserEntity __UserEntity = SessionDataManager.GetUserBySessionID(__CurrentSession.SessionID);
                if (__UserEntity != null)
                {
                    __CurrentSession.SetUser(__UserEntity);
                }
                else
                {
                    /*__UserEntity = SessionDataManager.GetUserBySessionIDFromTemp(__CurrentSession.SessionID);
                    if (__UserEntity != null)
                    {
                        __CurrentSession.SetUser(__UserEntity);
                    }*/
                }

                __CurrentSession.RefreshValue(_Controller);
                SessionItems.Remove(__CurrentSession);
                SessionItems.Insert(0, __CurrentSession);

                return __CurrentSession;
            }
        }
       

        public void Logout(IController _Controller)
        {
            lock (SessionItems)
            {
                _Controller.CurrentContext.Response.Cookies.Append(CookieSessionName, "");
                SessionItems.Remove(_Controller.ClientSession);
                _Controller.ClientSession.Logout();
            }
        }
        public void ForceLogout(List<cSession> _ClientSession)
        {
            lock (SessionItems)
            {
                for (int i = 0; i < _ClientSession.Count; i++)
                {
                    SessionItems.Remove(_ClientSession[i]);
                    SessionDataManager.DeleteSession(_ClientSession[i].SessionID);
                }

            }
        }

        public cSession GetSessionByID(string _SessionID)
        {
            lock (SessionItems)
            {
                return SessionItems.Find(__Item => __Item.SessionID == _SessionID);
            }
        }

        private void ClearUnusedSession(IController _Controller)
        {
            lock (SessionItems)
            {
				
				List<cSession> __Sessions = SessionItems.Where(__Item => __Item.CreateTime.AddMinutes(30) < DateTime.Now).ToList();
				if (__Sessions.Count > 0)
				{
					WebGraph.ActionGraph.DoReconnectSignalRequestAction.Action(_Controller, __Sessions, true);
				}
				SessionItems.RemoveAll(__Item => __Item.CreateTime.AddMinutes(30) < DateTime.Now);
            }
        }
    }
}
