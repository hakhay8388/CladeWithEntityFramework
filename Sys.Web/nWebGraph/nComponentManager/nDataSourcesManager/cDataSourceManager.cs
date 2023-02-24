using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using Sys.Boundary.nDefaultValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sys.Web.nWebGraph.nComponentManager.nDataSourcesManager;

namespace Sys.Web.nWebGraph.nComponentManager.nDataSourcesManager
{
    public class cDataSourceManager : cCoreObject
    {
        List<IDataSource> DataSourceList { get; set; }
        cBaseWebGraph WebGraph { get; set; }

        public cDataSourceManager(cApp _App, cBaseWebGraph _WebGraph)
                : base(_App)
        {
            DataSourceList = new List<IDataSource>();
            WebGraph = _WebGraph;
        }
      

		public override void Init()
		{
            //DataSourceList = WebGraph.ListenerGraph.GetListenerByBaseType<IDataSource>();

            List<Type> __DataSources = App.Handlers.AssemblyHandler.GetTypesFromBaseInterface<IDataSource>();
			__DataSources.ForEach(__Type =>
			{
				IDataSource __DataSource = (IDataSource)App.Factories.ObjectFactory.ResolveInstance(__Type);
				__DataSource.Init();
				DataSourceList.Add(__DataSource);
			});
		}


		public void FirtsRequestInit()
        {
            foreach (var __Item in DataSourceList)
            {
                __Item.SynchronizeColumnNames();
            }
        }


        public IDataSource GetDataSourceByID(DataSourceIDs _DataSourceID)
        {
            return DataSourceList.Find(__Item => __Item.DataSourceID.ID == _DataSourceID.ID);
        }

        public IDataSource GetDataSourceByDataSourceClientComponentName(string _ClientComponentName)
        {
            return DataSourceList.Find(__Item => __Item.DataSourceID.ClientComponentName == _ClientComponentName);
        }
    }
}
