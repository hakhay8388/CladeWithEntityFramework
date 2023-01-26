using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bootstrapper.Core.nHandlers.nDefaultDataHandler
{
	public class cDefaultDataItem : cCoreObject
	{
		public string ItemCode{ get; set; }
	public JObject ItemJObject { get; set; }
		private string ItemFileFullPath { get; set; }
		public cDefaultDataItem(nApplication.cApp _App, string _ItemCode, string _ItemFileFullPath)
			: base(_App)
		{
			ItemFileFullPath = _ItemFileFullPath;
			ItemCode = _ItemCode;
			LoadItem();
		}

		private void LoadItem()
		{
			if (File.Exists(ItemFileFullPath))
			{
				var __ItemJSON = App.Handlers.FileHandler.ReadString(ItemFileFullPath);
				ItemJObject = JObject.Parse(__ItemJSON);
			}
			else
			{
				throw new Exception("DefaultDataItem dosyası bulunamadı!");
			}
		}

		public void LoadDataFromJson(string _ItemJObjectName, Action<JObject> _Function)
		{
			if (ItemJObject.ContainsKey(_ItemJObjectName))
			{
				JArray __ObjecList = (JArray)ItemJObject[_ItemJObjectName];
				for (int i = 0; i < __ObjecList.Count;i++)
                {
					_Function((JObject)__ObjecList[i]);
				}
			}
			else
			{
				throw new Exception("DefaultDataItem " + _ItemJObjectName + " değişkeni bulunamadı!");
			}
		}

	}
}
