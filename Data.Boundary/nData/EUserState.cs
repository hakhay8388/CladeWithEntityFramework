using Bootstrapper.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Boundary.nData
{
    public enum EUserStateEnums
    {
        Confirmed = 1,
        Canceled = 2,
        Suspended = 3,
        Waiting = 4,
    }

    public class EUserState : cBaseConstType<EUserState>
    {
        public static EUserState Confirmed = new EUserState(GetVariableName(() => Confirmed), (int)EUserStateEnums.Confirmed, "Confirmed");
        public static EUserState Canceled = new EUserState(GetVariableName(() => Canceled), (int)EUserStateEnums.Canceled, "Canceled");
        public static EUserState Suspended = new EUserState(GetVariableName(() => Suspended), (int)EUserStateEnums.Suspended, "Suspended");
        public static EUserState Waiting = new EUserState(GetVariableName(() => Waiting), (int)EUserStateEnums.Waiting, "Waiting");

        public static List<EUserState> TypeList { get; set; }

        public EUserState(string _Code, int _ID, string _Name)
            : base(_Name, _Code, _ID)
        {
            TypeList = TypeList ?? new List<EUserState>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static EUserState GetByID(int _ID, EUserState _DefaultData)
        {
            return GetByID(TypeList, _ID, _DefaultData);
        }
        public static EUserState GetByName(string _Name, EUserState _DefaultData)
        {
            return GetByName(TypeList, _Name, _DefaultData);
        }
    }
}
