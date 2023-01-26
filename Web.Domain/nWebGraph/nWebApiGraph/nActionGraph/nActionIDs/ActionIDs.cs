using Bootstrapper.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs
{
    public class ActionIDs : cBaseConstType<ActionIDs>
    {

        public static List<ActionIDs> TypeList { get; set; }

        public static ActionIDs SuccessResult = new ActionIDs(GetVariableName(() => SuccessResult), 1, "", true);
        public static ActionIDs CacheIt = new ActionIDs(GetVariableName(() => CacheIt), 2, "", true);

		public static ActionIDs CommandList = new ActionIDs(GetVariableName(() => CommandList), 10, "", true);
        public static ActionIDs ActionList = new ActionIDs(GetVariableName(() => ActionList), 11, "", true);
		public static ActionIDs Language = new ActionIDs(GetVariableName(() => Language), 12, "", true);
		public static ActionIDs ErrorAction = new ActionIDs(GetVariableName(() => ErrorAction), 13, "", true);
        public static ActionIDs NoPermission = new ActionIDs(GetVariableName(() => NoPermission), 14, "", true);
        public static ActionIDs BadCommand = new ActionIDs(GetVariableName(() => BadCommand), 15, "", true);
        public static ActionIDs SpamFilter = new ActionIDs(GetVariableName(() => SpamFilter), 16, "", true);
        public static ActionIDs SetState = new ActionIDs(GetVariableName(() => SetState), 17, "", true);
        public static ActionIDs SetVariable = new ActionIDs(GetVariableName(() => SetVariable), 18, "", true);
        public static ActionIDs LogInOut = new ActionIDs(GetVariableName(() => LogInOut), 19, "", true);
        
        
        public static ActionIDs ShowMessage = new ActionIDs(GetVariableName(() => ShowMessage), 20, "", true);
        public static ActionIDs ShowMessageAndRunCommand = new ActionIDs(GetVariableName(() => ShowMessageAndRunCommand), 21, "", true);
        public static ActionIDs HotSpotMessage = new ActionIDs(GetVariableName(() => HotSpotMessage), 22, "", true);
        public static ActionIDs HotSpotMessageAndRunCommand = new ActionIDs(GetVariableName(() => HotSpotMessageAndRunCommand), 23, "", true);
        public static ActionIDs GoPage = new ActionIDs(GetVariableName(() => GoPage), 24, "", true);
        public static ActionIDs ResultList = new ActionIDs(GetVariableName(() => ResultList), 25, "", true);
        public static ActionIDs SetUserOnClient = new ActionIDs(GetVariableName(() => SetUserOnClient), 26, "", true);
        public static ActionIDs SetServerDateTime = new ActionIDs(GetVariableName(() => SetServerDateTime), 27, "", true);
        public static ActionIDs ResultItem = new ActionIDs(GetVariableName(() => ResultItem), 28, "", true);
        public static ActionIDs SetGlobalParamList = new ActionIDs(GetVariableName(() => SetGlobalParamList), 29, "", true);
        public static ActionIDs ProgressStatus = new ActionIDs(GetVariableName(() => ProgressStatus), 30, "", true);
        public static ActionIDs Notification = new ActionIDs(GetVariableName(() => Notification), 31, "", true);
        public static ActionIDs AsyncLoad = new ActionIDs(GetVariableName(() => AsyncLoad), 33, "", true);
		public static ActionIDs DataSourceRefresh = new ActionIDs(GetVariableName(() => DataSourceRefresh), 34, "", true);
		public static ActionIDs ForceUpdate = new ActionIDs(GetVariableName(() => ForceUpdate), 35, "", true);
		public static ActionIDs DoReconnectSignalRequest = new ActionIDs(GetVariableName(() => DoReconnectSignalRequest), 37, "", true);
		public static ActionIDs DebugAlert= new ActionIDs(GetVariableName(() => DebugAlert), 38, "", true);
		public static ActionIDs DoCheckLoginRequest = new ActionIDs(GetVariableName(() => DoCheckLoginRequest), 39, "", true);
		public static ActionIDs ValidationResult = new ActionIDs(GetVariableName(() => ValidationResult), 40, "", true);
        public static ActionIDs PageResult = new ActionIDs(GetVariableName(() => PageResult), 41, "", true);
        public static ActionIDs MenuResult = new ActionIDs(GetVariableName(() => MenuResult), 41, "", true);


        public static ActionIDs ModalOpen = new ActionIDs(GetVariableName(() => ModalOpen), 10009, "", true);
        public static ActionIDs SetClientLanguage = new ActionIDs(GetVariableName(() => SetClientLanguage), 20003, "", true);
        public static ActionIDs ForceLogout = new ActionIDs(GetVariableName(() => ForceLogout), 20004, "", true);


        public bool Enabled { get; set; }
        public string Info { get; set; }


        public ActionIDs(string _Name, int _ID, string _Info, bool _Enabled)
            : base(_Name, _Name, _ID)
        {
            TypeList = TypeList ?? new List<ActionIDs>();
            Enabled = _Enabled;
            Info = _Info;
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static ActionIDs GetByID(int _ID, ActionIDs _DefaultCommandID)
        {
            return GetByID(TypeList, _ID, _DefaultCommandID);
        }
        public static ActionIDs GetByName(string _Name, ActionIDs _DefaultCommandID)
        {
            return GetByName(TypeList, _Name, _DefaultCommandID);
        }
    }
}
