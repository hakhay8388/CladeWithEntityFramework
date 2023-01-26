using Bootstrapper.Boundary.nValueTypes.nConstType;
using Data.Boundary.nData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Core.BatchJobService.nDefaultValueTypes
{
    public class BatchJobIDs : cBaseConstType<BatchJobIDs>
    {
        public static List<BatchJobIDs> TypeList { get; set; }

        public static BatchJobIDs TestService = new BatchJobIDs(GetVariableName(() => TestService), "TestService", 1, 10000, EBatchJobState.Started, true, true, false, 3);
        public static BatchJobIDs OldBatchJobExcutionsDelete = new BatchJobIDs(GetVariableName(() => OldBatchJobExcutionsDelete), "OldBatchJobExcutionsDelete", 2, 86400000, EBatchJobState.Started, true, true, false, 3);
        
        ///////////////////// 

        public int TimePeriodMilisecond { get; set; }
        public EBatchJobState State { get; set; }
        public bool AutoExecution { get; set; }
        public bool ExecuteFirstWithoutWait { get; set; }
        public bool StopAfterFirstExecution { get; set; }
        public virtual int MaxRetryCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Code"></param>
        /// <param name="_Name"></param>
        /// <param name="_ID"></param>
        /// <param name="_TimePeriodMilisecond"></param>
        /// <param name="_State"></param>
        /// <param name="_AutoExecution">Son execute edilen job parametreleriyle alıp tekrar ekleyip run eder.</param>
        /// <param name="_ExecuteFirstWithoutWait">Bir job eklendiği anda bekleme zamanını beklemeden run eder ve sonra beklemeye başlar</param>
        /// <param name="_StopAfterFirstExecution">Herhangi bir job execute edildikten sonra servisi durdurur.tekrar çalıştırmak için start edilmesi lazım.</param>
        /// <param name="_MaxRetryCount">Maksimum Yeniden Deneme Sayısı</param>
        public BatchJobIDs(string _Code, string _Name, int _ID, int _TimePeriodMilisecond, EBatchJobState _State, bool _AutoExecution, bool _ExecuteFirstWithoutWait, bool _StopAfterFirstExecution, int _MaxRetryCount)
            : base(_Name, _Code, _ID)
        {
            TimePeriodMilisecond = _TimePeriodMilisecond;
            State = _State;
            AutoExecution = _AutoExecution;
            ExecuteFirstWithoutWait = _ExecuteFirstWithoutWait;
            StopAfterFirstExecution = _StopAfterFirstExecution;
            MaxRetryCount = _MaxRetryCount;

            TypeList = TypeList ?? new List<BatchJobIDs>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static BatchJobIDs GetByID(int _ID, BatchJobIDs _DefaultID)
        {
            return GetByID(TypeList, _ID, _DefaultID);
        }
        public static BatchJobIDs GetByName(string _Name, BatchJobIDs _DefaultID)
        {
            return GetByName(TypeList, _Name, _DefaultID);
        }

        public static BatchJobIDs GetByCode(string _Code, BatchJobIDs _DefaultID)
        {
            return GetByCode(TypeList, _Code, _DefaultID);
        }
    }
}
