using Bootstrapper.Boundary.nValueTypes.nConstType;
using Sys.Boundary.nDefaultValueTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Boundary.nDefaultValueTypes
{
    public class DomainPageIDs : PageIDs
    {
        public static PageIDs TestPage { get; set; }
        public static PageIDs TeamMemberHayriPage { get; set; }


        public static void Init()
        {
            TestPage = new PageIDs(GetVariableName(() => TestPage), "TestPage", "testpage", "TTestPage", 1000, new List<RoleIDs>() { RoleIDs.Admin });

            TeamMemberHayriPage = new PageIDs(GetVariableName(() => TeamMemberHayriPage), "TeamMemberHayriPage", "teammemberhayripage", "TTeamMemberHayriPage", 1001, new List<RoleIDs>() { RoleIDs.Admin });
        }

        public DomainPageIDs(string _Code, string _Name, string _Url, string _Component, int _ID, List<RoleIDs> _MainRoles, string[] _SubParamName = null) 
            : base(_Code, _Name, _Url, _Component, _ID, _MainRoles, _SubParamName)
        {
        }
    }
}
