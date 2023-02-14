using Bootstrapper.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs
{
    public class EActionType : cBaseConstType<EActionType>
    {

        public static List<EActionType> TypeList { get; set; }

        public static EActionType SuccessResult = new EActionType(GetVariableName(() => SuccessResult), 1, "", true);
        public static EActionType CacheIt = new EActionType(GetVariableName(() => CacheIt), 2, "", true);

		public static EActionType CommandList = new EActionType(GetVariableName(() => CommandList), 10, "", true);
        public static EActionType ActionList = new EActionType(GetVariableName(() => ActionList), 11, "", true);
		public static EActionType Language = new EActionType(GetVariableName(() => Language), 12, "", true);
		public static EActionType ErrorAction = new EActionType(GetVariableName(() => ErrorAction), 13, "", true);
        public static EActionType NoPermission = new EActionType(GetVariableName(() => NoPermission), 14, "", true);
        public static EActionType BadCommand = new EActionType(GetVariableName(() => BadCommand), 15, "", true);
        public static EActionType SpamFilter = new EActionType(GetVariableName(() => SpamFilter), 16, "", true);
        public static EActionType SetState = new EActionType(GetVariableName(() => SetState), 17, "", true);
        public static EActionType SetVariable = new EActionType(GetVariableName(() => SetVariable), 18, "", true);
        public static EActionType LogInOut = new EActionType(GetVariableName(() => LogInOut), 19, "", true);
        
        
        public static EActionType ShowMessage = new EActionType(GetVariableName(() => ShowMessage), 20, "", true);
        public static EActionType ShowMessageAndRunCommand = new EActionType(GetVariableName(() => ShowMessageAndRunCommand), 21, "", true);
        public static EActionType HotSpotMessage = new EActionType(GetVariableName(() => HotSpotMessage), 22, "", true);
        public static EActionType HotSpotMessageAndRunCommand = new EActionType(GetVariableName(() => HotSpotMessageAndRunCommand), 23, "", true);
        public static EActionType GoPage = new EActionType(GetVariableName(() => GoPage), 24, "", true);
        public static EActionType ResultList = new EActionType(GetVariableName(() => ResultList), 25, "", true);
        public static EActionType SetUserOnClient = new EActionType(GetVariableName(() => SetUserOnClient), 26, "", true);
        public static EActionType SetServerDateTime = new EActionType(GetVariableName(() => SetServerDateTime), 27, "", true);
        public static EActionType ResultItem = new EActionType(GetVariableName(() => ResultItem), 28, "", true);
        public static EActionType SetGlobalParamList = new EActionType(GetVariableName(() => SetGlobalParamList), 29, "", true);
        public static EActionType ProgressStatus = new EActionType(GetVariableName(() => ProgressStatus), 30, "", true);
        public static EActionType Notification = new EActionType(GetVariableName(() => Notification), 31, "", true);
        public static EActionType AsyncLoad = new EActionType(GetVariableName(() => AsyncLoad), 33, "", true);
		public static EActionType DataSourceRefresh = new EActionType(GetVariableName(() => DataSourceRefresh), 34, "", true);
		public static EActionType ForceUpdate = new EActionType(GetVariableName(() => ForceUpdate), 35, "", true);
		public static EActionType DoReconnectSignalRequest = new EActionType(GetVariableName(() => DoReconnectSignalRequest), 37, "", true);
		public static EActionType DebugAlert= new EActionType(GetVariableName(() => DebugAlert), 38, "", true);
		public static EActionType DoCheckLoginRequest = new EActionType(GetVariableName(() => DoCheckLoginRequest), 39, "", true);
		public static EActionType ValidationResult = new EActionType(GetVariableName(() => ValidationResult), 40, "", true);
        public static EActionType PageResult = new EActionType(GetVariableName(() => PageResult), 41, "", true);
        public static EActionType MenuResult = new EActionType(GetVariableName(() => MenuResult), 42, "", true);


        public static EActionType ModalOpen = new EActionType(GetVariableName(() => ModalOpen), 100, "", true);
        public static EActionType SetClientLanguage = new EActionType(GetVariableName(() => SetClientLanguage), 101, "", true);
        public static EActionType ForceLogout = new EActionType(GetVariableName(() => ForceLogout), 102, "", true);


        public bool Enabled { get; set; }
        public string Info { get; set; }


        public EActionType(string _Name, int _ID, string _Info, bool _Enabled)
            : base(_Name, _Name, _ID)
        {
            TypeList = TypeList ?? new List<EActionType>();
            Enabled = _Enabled;
            Info = _Info;
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static EActionType GetByID(int _ID, EActionType _DefaultCommandID)
        {
            return GetByID(TypeList, _ID, _DefaultCommandID);
        }
        public static EActionType GetByName(string _Name, EActionType _DefaultCommandID)
        {
            return GetByName(TypeList, _Name, _DefaultCommandID);
        }
    }
}
