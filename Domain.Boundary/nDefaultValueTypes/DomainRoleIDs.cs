using Bootstrapper.Boundary.nValueTypes.nConstType;
using Sys.Boundary.nDefaultValueTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Domain.Boundary.nDefaultValueTypes
{
    public class DomainRoleIDs : RoleIDs
    {
        public static RoleIDs TestUserRole { get; set; }
        public static void Init()
        {
            TestUserRole = new RoleIDs(GetVariableName(() => User), GetVariableName(() => TestUserRole), 1);
        }
        public DomainRoleIDs(string _MainCode, string _Code, int _ID) 
            : base(_MainCode, _Code, _ID)
        {
        }
    }
}
