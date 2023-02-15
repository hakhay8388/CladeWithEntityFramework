using Bootstrapper.Boundary.nValueTypes.nConstType;
using Sys.Boundary.nDefaultValueTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Boundary.nDefaultValueTypes
{
    public class DomainDataSourceIDs : DataSourceIDs
    {


        public DomainDataSourceIDs(string _Code, string _ClientComponentName, string _Name, int _ID, bool _IsPublic = false) 
            : base(_Code, _ClientComponentName, _Name, _ID, _IsPublic)
        {
        }
    }
}
