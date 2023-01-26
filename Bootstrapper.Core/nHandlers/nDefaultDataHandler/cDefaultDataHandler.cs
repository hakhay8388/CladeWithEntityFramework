using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nApplication.nCoreLoggers;
//using Bootstrapper.Core.nApplication.nCoreLoggers.nMethodCallLogger;
using Bootstrapper.Core.nAttributes;
using Bootstrapper.Core.nCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrapper.Core.nHandlers.nDefaultDataHandler
{
    public class cDefaultDataHandler : cCoreObject
    {
        public Dictionary<string, cDefaultDataItem> DefaultDataList { get; private set; }
        public cDefaultDataHandler(nApplication.cApp _App)
            : base(_App)
        {
            DefaultDataList = new Dictionary<string, cDefaultDataItem>();
        }

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cDefaultDataHandler>(this);
        }

        public string GetDataItemFullPath(string _ItemCode)
        {
            return Path.Combine(App.Configuration.DefaultDataPath, _ItemCode + ".json");
        }

        public cDefaultDataItem GetItemByCode(string _ItemCode)
        {
            if (DefaultDataList.ContainsKey(_ItemCode))
            {
                return DefaultDataList[_ItemCode];
            }
            return null;
        }

        public string GetCheckSum(string _ItemCode)
        {
            if (File.Exists(GetDataItemFullPath(_ItemCode)))
            {
                return App.Handlers.FileHandler.GetFileChecksum(GetDataItemFullPath(_ItemCode));
            }
            else
            {
                throw new Exception(_ItemCode + " dosyası bulunamadı!");
            }
        }


        public cDefaultDataItem LoadItem(string _ItemCode)
        {
            if (File.Exists(GetDataItemFullPath(_ItemCode)))
            {
                cDefaultDataItem __DefaultDataItem = new cDefaultDataItem(App, _ItemCode, GetDataItemFullPath(_ItemCode));
                if (!DefaultDataList.ContainsKey(_ItemCode))
                {
                    DefaultDataList.Add(_ItemCode, __DefaultDataItem);
                }
                return __DefaultDataItem;
            }
            else
            {
                throw new Exception(_ItemCode + " dosyası bulunamadı!");
            }
        }
    }
}
