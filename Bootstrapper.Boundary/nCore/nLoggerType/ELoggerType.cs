using Bootstrapper.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrapper.Boundary.nCore.nLoggerType
{
    public class ELoggerType : cBaseConstType<ELoggerType>
    {
        public static ELoggerType BatchLogger = new ELoggerType(GetVariableName(() => BatchLogger), 1);
        public static ELoggerType CoreLogger = new ELoggerType(GetVariableName(() => CoreLogger),2);
        public static ELoggerType RequestPerformanceLogger = new ELoggerType(GetVariableName(() => RequestPerformanceLogger),3);
        public static ELoggerType CoreSqlLogger = new ELoggerType(GetVariableName(() => CoreSqlLogger),4);
        public static ELoggerType SqlGlobalInfoLogger = new ELoggerType(GetVariableName(() => SqlGlobalInfoLogger),5);

        static List<ELoggerType> TypeList { get; set; }
        public ELoggerType(string _Name, int _Value)
            : base(_Name, _Name, _Value)
        {
            TypeList = TypeList ?? new List<ELoggerType>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static ELoggerType GetByID(int _ID, ELoggerType _DefaultBootType)
        {
            return GetByID(TypeList, _ID, _DefaultBootType);
        }
        public static ELoggerType GetByName(string _Name, ELoggerType _DefaultBootType)
        {
            return GetByName(TypeList, _Name, _DefaultBootType);
        }
    }
}
