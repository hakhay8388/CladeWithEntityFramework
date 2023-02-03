using Bootstrapper.Boundary.nCore.nLoggerType;
using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nApplication.nCoreLoggers;
//using Bootstrapper.Core.nApplication.nCoreLoggers.nMethodCallLogger;
using Bootstrapper.Core.nAttributes;
using Bootstrapper.Core.nCore;
using Bootstrapper.Core.nExceptions;
using Nest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrapper.Core.nHandlers.nElasticSearchHandler
{
	public class cElasticSearchHandler : cCoreObject
	{
        public cElasticSearchHandler(nApplication.cApp _App)
			: base(_App)
		{
		}

		public override void Init()
		{
			App.Factories.ObjectFactory.RegisterInstance<cElasticSearchHandler>(this);
        }

        public ElasticClient CreateClient(string _Index)
        {
            string __Name = (App.Configuration.ApplicationSettings.ElasticSearchSetting.ElasticIndexPrefix + "_" + _Index).ToLower();
            new HttpClient().PutAsync(App.Configuration.ApplicationSettings.ElasticSearchSetting.ElasticUrl + __Name, new JsonContent(new { }));
            ElasticClient __Client = new ElasticClient(
               new ConnectionSettings(new Uri(App.Configuration.ApplicationSettings.ElasticSearchSetting.ElasticUrl))
               .DefaultIndex(__Name)
           );
            return __Client;
        }

        public IndexResponse Write(ElasticClient _Client, string _Value)
        {
            IndexResponse __ContentResponse = _Client.IndexDocument(new cLogItem() { LogValue = _Value, LogTime = DateTime.Now });
            return __ContentResponse;
        }

        public IndexResponse Write<TObject>(ElasticClient _Client, TObject _Value)
            where TObject : cBaseLogItem
        {
            IndexResponse __ContentResponse = _Client.IndexDocument(_Value);
            return __ContentResponse;
        }
    }
}
