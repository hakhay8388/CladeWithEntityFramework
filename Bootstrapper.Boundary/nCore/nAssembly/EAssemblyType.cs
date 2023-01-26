using Bootstrapper.Boundary.nValueTypes.nConstType;
using System.Collections.Generic;
using System.Data;

namespace Bootstrapper.Boundary.nCore.nAssembly
{
    public class AssemblyType : cBaseConstType<AssemblyType>
    {
        public static AssemblyType None = new AssemblyType(GetVariableName(() => None), 1);
        public static AssemblyType Concrete = new AssemblyType(GetVariableName(() => Concrete), 2);
        public static AssemblyType Abstract = new AssemblyType(GetVariableName(() => Abstract), 3);
        public static AssemblyType Interface = new AssemblyType(GetVariableName(() => Interface), 4);
        public static AssemblyType Enum = new AssemblyType(GetVariableName(() => Enum), 5);

        static List<AssemblyType> TypeList { get; set; }
        public AssemblyType(string _Name, int _Value)
            : base(_Name, _Name, _Value)
        {
            TypeList = TypeList ?? new List<AssemblyType>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static AssemblyType GetByID(int _ID, AssemblyType _ETypeSearch)
        {
            return GetByID(TypeList, _ID, _ETypeSearch);
        }
        public static AssemblyType GetByName(string _Name, AssemblyType _ETypeSearch)
        {
            return GetByName(TypeList, _Name, _ETypeSearch);
        }
    }
}
