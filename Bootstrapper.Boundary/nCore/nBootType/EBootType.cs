using Bootstrapper.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrapper.Boundary.nCore.nBootType
{
    public class EBootType : cBaseConstType<EBootType>
    {
        public static EBootType None = new EBootType(GetVariableName(() => None), 1);
        public static EBootType Web = new EBootType(GetVariableName(() => Web),2);
        public static EBootType Batch = new EBootType(GetVariableName(() => Batch),3);
        public static EBootType Console = new EBootType(GetVariableName(() => Console),4);
        public static EBootType Desktop = new EBootType(GetVariableName(() => Desktop),5);

        static List<EBootType> TypeList { get; set; }
        public EBootType(string _Name, int _Value)
            : base(_Name, _Name, _Value)
        {
            TypeList = TypeList ?? new List<EBootType>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static EBootType GetByID(int _ID, EBootType _DefaultBootType)
        {
            return GetByID(TypeList, _ID, _DefaultBootType);
        }
        public static EBootType GetByName(string _Name, EBootType _DefaultBootType)
        {
            return GetByName(TypeList, _Name, _DefaultBootType);
        }
    }
}
