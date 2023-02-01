using Web.Domain.nUtils.nValueTypes;
using Data.Domain.nDefaultValueTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Bootstrapper.Boundary.nValueTypes.nConstType;

namespace Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommandIDs
{
    public class CommandIDs : cBaseConstType<CommandIDs>
    {

        public static List<CommandIDs> TypeList { get; set; }


        public static CommandIDs FirstInit = new CommandIDs(GetVariableName(() => FirstInit), 1, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Admin, RoleIDs.Unlogined, RoleIDs.Developer, RoleIDs.User });
        public static CommandIDs GetCommandList = new CommandIDs(GetVariableName(() => GetCommandList), 2, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Unlogined, RoleIDs.Developer, RoleIDs.User });
        public static CommandIDs GetActionList = new CommandIDs(GetVariableName(() => GetActionList), 3, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Unlogined, RoleIDs.Developer, RoleIDs.User });
        public static CommandIDs SetLanguage = new CommandIDs(GetVariableName(() => SetLanguage), 4, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Unlogined, RoleIDs.Developer, RoleIDs.User });

        public static CommandIDs MessageResult = new CommandIDs(GetVariableName(() => MessageResult), 5, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Unlogined, RoleIDs.Developer, RoleIDs.User });
        public static CommandIDs GetEnumVariableList = new CommandIDs(GetVariableName(() => GetEnumVariableList), 6, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Unlogined, RoleIDs.Developer, RoleIDs.User }, true);
        public static CommandIDs GetServerDateTime = new CommandIDs(GetVariableName(() => GetServerDateTime), 7, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Unlogined, RoleIDs.Developer, RoleIDs.User });


        public static CommandIDs Login = new CommandIDs(GetVariableName(() => Login), 10, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Unlogined, RoleIDs.Developer, RoleIDs.User }, _DoFlowCheck: true);
        public static CommandIDs Logout = new CommandIDs(GetVariableName(() => Logout), 11, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Unlogined, RoleIDs.Developer, RoleIDs.User });
        public static CommandIDs CheckLogin = new CommandIDs(GetVariableName(() => CheckLogin), 12, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Unlogined, RoleIDs.Developer, RoleIDs.User }, _DoFlowCheck: true);


        public static CommandIDs GetMenuList = new CommandIDs(GetVariableName(() => GetMenuList), 13, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.User, RoleIDs.Unlogined, RoleIDs.Developer });
        public static CommandIDs GetPageList = new CommandIDs(GetVariableName(() => GetPageList), 14, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.User, RoleIDs.Unlogined, RoleIDs.Developer });

        public static CommandIDs GetNotifications = new CommandIDs(GetVariableName(() => GetNotifications), 20, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.User, RoleIDs.Developer });
        public static CommandIDs ReadNotification = new CommandIDs(GetVariableName(() => ReadNotification), 21, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.User, RoleIDs.Developer });


        /// <summary>
        /// // DATA SOURCE
        /// </summary>
        public static CommandIDs DataSource_Read = new CommandIDs(GetVariableName(() => DataSource_Read), 20000, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.User, RoleIDs.Unlogined, RoleIDs.Developer });
        public static CommandIDs DataSource_Create = new CommandIDs(GetVariableName(() => DataSource_Create), 20001, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.User, RoleIDs.Unlogined, RoleIDs.Developer });
        public static CommandIDs DataSource_Update = new CommandIDs(GetVariableName(() => DataSource_Update), 20002, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.User, RoleIDs.Unlogined, RoleIDs.Developer });
        public static CommandIDs DataSource_Delete = new CommandIDs(GetVariableName(() => DataSource_Delete), 20003, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.User, RoleIDs.Unlogined, RoleIDs.Developer });
        public static CommandIDs DataSource_GetMetaData = new CommandIDs(GetVariableName(() => DataSource_GetMetaData), 20004, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.User, RoleIDs.Unlogined, RoleIDs.Developer });
        public static CommandIDs DataSource_GetSettings = new CommandIDs(GetVariableName(() => DataSource_GetSettings), 20005, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.User, RoleIDs.Unlogined, RoleIDs.Developer });
        /// <summary>
        /// //
        /// </summary>


        public bool Enabled { get; set; }
        public string Info { get; set; }
        public bool CacheIt { get; set; }
        public bool DoFlowCheck { get; set; }
        public List<RoleIDs> MainRoles { get; set; }

        public CommandIDs(string _Name, int _ID, string _Info, bool _Enabled, List<RoleIDs> _MainRoles, bool _CacheIt = false, bool _DoFlowCheck = false)
            : base(_Name, _Name, _ID)
        {
            TypeList = TypeList ?? new List<CommandIDs>();
            Info = _Info;
            Enabled = _Enabled;
            CacheIt = _CacheIt;
            DoFlowCheck = _DoFlowCheck;
            MainRoles = _MainRoles;

            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static CommandIDs GetByID(int _ID, CommandIDs _DefaultCommandID)
        {
            return GetByID(TypeList, _ID, _DefaultCommandID);
        }
        public static CommandIDs GetByName(string _Name, CommandIDs _DefaultCommandID)
        {
            return GetByName(TypeList, _Name, _DefaultCommandID);
        }
    }
}
