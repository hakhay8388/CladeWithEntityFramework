using Bootstrapper.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Boundary.nData
{
    public enum EBatchJobStateEnums
    {
        Started = 1,
        Stopped = 2,
    }

    public class EBatchJobState : cBaseConstType<EBatchJobState>
    {
        public static EBatchJobState Started = new EBatchJobState(GetVariableName(() => Started), (int)EBatchJobStateEnums.Started, "Started");
        public static EBatchJobState Stopped = new EBatchJobState(GetVariableName(() => Stopped), (int)EBatchJobStateEnums.Stopped, "Stopped");

        public static List<EBatchJobState> TypeList { get; set; }

        public EBatchJobState(string _Code, int _ID, string _Name)
            : base(_Name, _Code, _ID)
        {
            TypeList = TypeList ?? new List<EBatchJobState>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static EBatchJobState GetByID(int _ID, EBatchJobState _DefaultData)
        {
            return GetByID(TypeList, _ID, _DefaultData);
        }
        public static EBatchJobState GetByName(string _Name, EBatchJobState _DefaultData)
        {
            return GetByName(TypeList, _Name, _DefaultData);
        }
    }
}
