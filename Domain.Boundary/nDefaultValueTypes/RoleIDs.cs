using Bootstrapper.Boundary.nValueTypes.nConstType;
using Sys.Boundary.nDefaultValueTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Boundary.nDefaultValueTypes
{
    public class DomainRoleIDs : RoleIDs
    {
        public DomainRoleIDs(string _MainCode, string _Code, int _ID) 
            : base(_MainCode, _Code, _ID)
        {
        }
    }
}
