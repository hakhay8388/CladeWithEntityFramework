using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_ReadCommand
{
    public class cDataSource_ReadCommandData
    {
        public String ClientComponentName;
        public int PageSize;
        public int Page;
        public string OrderByField;
        public string OrderDirection;
        public string Search;
        public object Options;
    }
}
