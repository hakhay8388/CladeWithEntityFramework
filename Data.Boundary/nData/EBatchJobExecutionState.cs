using Bootstrapper.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Boundary.nData
{
    public enum EBatchJobExecutionStateEnums
    {
        NotRunning = 1,
        Running = 2,
        Success = 3,
        Error = 4,
    }

    public class EBatchJobExecutionState : cBaseConstType<EBatchJobExecutionState>
    {
        public static EBatchJobExecutionState NotRunning = new EBatchJobExecutionState(GetVariableName(() => NotRunning), (int)EBatchJobExecutionStateEnums.NotRunning, "NotRunning");
        public static EBatchJobExecutionState Running = new EBatchJobExecutionState(GetVariableName(() => Running), (int)EBatchJobExecutionStateEnums.Running, "Running");
        public static EBatchJobExecutionState Success = new EBatchJobExecutionState(GetVariableName(() => Success), (int)EBatchJobExecutionStateEnums.Success, "Success");
        public static EBatchJobExecutionState Error = new EBatchJobExecutionState(GetVariableName(() => Error), (int)EBatchJobExecutionStateEnums.Error, "Error");

        public static List<EBatchJobExecutionState> TypeList { get; set; }

        public EBatchJobExecutionState(string _Code, int _ID, string _Name)
            : base(_Name, _Code, _ID)
        {
            TypeList = TypeList ?? new List<EBatchJobExecutionState>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static EBatchJobExecutionState GetByID(int _ID, EBatchJobExecutionState _DefaultData)
        {
            return GetByID(TypeList, _ID, _DefaultData);
        }
        public static EBatchJobExecutionState GetByName(string _Name, EBatchJobExecutionState _DefaultData)
        {
            return GetByName(TypeList, _Name, _DefaultData);
        }
    }
}
