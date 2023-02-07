using Sys.Web.nUtils.nValueTypes;
using Sys.Boundary.nDefaultValueTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Bootstrapper.Boundary.nValueTypes.nConstType;

namespace Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommandIDs
{
    public class ECommandType : cBaseConstType<ECommandType>
    {

        public static List<ECommandType> TypeList { get; set; }


        public static ECommandType FirstInit = new ECommandType(GetVariableName(() => FirstInit), 1, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Unlogined, RoleIDs.Developer, RoleIDs.User });
        public static ECommandType GetCommandList = new ECommandType(GetVariableName(() => GetCommandList), 2, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Unlogined, RoleIDs.Developer, RoleIDs.User });
        public static ECommandType GetActionList = new ECommandType(GetVariableName(() => GetActionList), 3, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Unlogined, RoleIDs.Developer, RoleIDs.User });
        public static ECommandType SetLanguage = new ECommandType(GetVariableName(() => SetLanguage), 4, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Unlogined, RoleIDs.Developer, RoleIDs.User });

        public static ECommandType MessageResult = new ECommandType(GetVariableName(() => MessageResult), 5, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Unlogined, RoleIDs.Developer, RoleIDs.User });
        public static ECommandType GetEnumVariableList = new ECommandType(GetVariableName(() => GetEnumVariableList), 6, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Unlogined, RoleIDs.Developer, RoleIDs.User }, true);
        public static ECommandType GetServerDateTime = new ECommandType(GetVariableName(() => GetServerDateTime), 7, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Unlogined, RoleIDs.Developer, RoleIDs.User });


        public static ECommandType Login = new ECommandType(GetVariableName(() => Login), 10, "", true, new List<RoleIDs>() { RoleIDs.Unlogined }, _DoFlowCheck: true);
        public static ECommandType Logout = new ECommandType(GetVariableName(() => Logout), 11, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Developer, RoleIDs.User });
        public static ECommandType CheckLogin = new ECommandType(GetVariableName(() => CheckLogin), 12, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Unlogined, RoleIDs.Developer, RoleIDs.User }, _DoFlowCheck: true);


        public static ECommandType GetMenuList = new ECommandType(GetVariableName(() => GetMenuList), 13, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.User, RoleIDs.Unlogined, RoleIDs.Developer });
        public static ECommandType GetPageList = new ECommandType(GetVariableName(() => GetPageList), 14, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.User, RoleIDs.Unlogined, RoleIDs.Developer });

        public static ECommandType GetNotifications = new ECommandType(GetVariableName(() => GetNotifications), 20, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.User, RoleIDs.Developer });
        public static ECommandType ReadNotification = new ECommandType(GetVariableName(() => ReadNotification), 21, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.User, RoleIDs.Developer });


        /// <summary>
        /// // DATA SOURCE
        /// </summary>
        public static ECommandType DataSource_Read = new ECommandType(GetVariableName(() => DataSource_Read), 100, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.User, RoleIDs.Developer });
        public static ECommandType DataSource_Create = new ECommandType(GetVariableName(() => DataSource_Create), 101, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.User, RoleIDs.Developer });
        public static ECommandType DataSource_Update = new ECommandType(GetVariableName(() => DataSource_Update), 102, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.User, RoleIDs.Developer });
        public static ECommandType DataSource_Delete = new ECommandType(GetVariableName(() => DataSource_Delete), 103, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.User, RoleIDs.Developer });
        public static ECommandType DataSource_GetMetaData = new ECommandType(GetVariableName(() => DataSource_GetMetaData), 104, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.User, RoleIDs.Developer });
        public static ECommandType DataSource_GetSettings = new ECommandType(GetVariableName(() => DataSource_GetSettings), 105, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.User, RoleIDs.Developer });
        /// <summary>
        /// //
        /// </summary>


        public bool Enabled { get; set; }
        public string Info { get; set; }
        public bool CacheIt { get; set; }
        public bool DoFlowCheck { get; set; }
        public List<RoleIDs> MainRoles { get; set; }

        public ECommandType(string _Name, int _ID, string _Info, bool _Enabled, List<RoleIDs> _MainRoles, bool _CacheIt = false, bool _DoFlowCheck = false)
            : base(_Name, _Name, _ID)
        {
            TypeList = TypeList ?? new List<ECommandType>();
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
        public static ECommandType GetByID(int _ID, ECommandType _DefaultCommandID)
        {
            return GetByID(TypeList, _ID, _DefaultCommandID);
        }
        public static ECommandType GetByName(string _Name, ECommandType _DefaultCommandID)
        {
            return GetByName(TypeList, _Name, _DefaultCommandID);
        }
    }
}
