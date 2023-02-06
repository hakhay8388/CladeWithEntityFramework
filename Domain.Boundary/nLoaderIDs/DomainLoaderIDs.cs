using Bootstrapper.Boundary.nValueTypes.nConstType;
using Sys.Boundary.nLoaderIDs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Boundary.nLoaderIDs
{
    public class DomainLoaderIDs
    {
		public static LoaderIDs TestDataLoader = new LoaderIDs(nameof(TestDataLoader), 1);
    }
}
