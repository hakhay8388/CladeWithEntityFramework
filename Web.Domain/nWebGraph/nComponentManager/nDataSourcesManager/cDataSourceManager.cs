using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using Data.Domain.nDefaultValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.nWebGraph.nComponentManager.nDataSourcesManager;

namespace Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager
{
    public class cDataSourceManager : cCoreObject
    {
        List<IDataSource> DataSourceList { get; set; }

        public cDataSourceManager(cApp _App)
                : base(_App)
        {
            DataSourceList = new List<IDataSource>();
        }
      

		public override void Init()
		{

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
