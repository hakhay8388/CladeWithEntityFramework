using Bootstrapper.Core.nApplication;
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

namespace Bootstrapper.Core.nHandlers.nLanguageHandler
{
    public class cLanguageHandler : cCoreObject
    {
		public List<cLanguageItemObject> LanguageNameList { get; private set; }
		public Dictionary<string, cLanguageItem> LanguageList { get; private set; }
		public string LanguageListFileName { get; private set; }
		public string LanguageListFullPath { get { return GetLanguageItemFullPath(LanguageListFileName); } }
        public cLanguageHandler(cApp _App)
            :base(_App)
        {
			LanguageNameList = new List<cLanguageItemObject>();
			LanguageList = new Dictionary<string, cLanguageItem>();
			LanguageListFileName = "Languages.json";
		}

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cLanguageHandler>(this);
			LoadLanguages();
		}

		public string GetLanguageItemFullPath(string _Item)
		{
			return Path.Combine(App.Configuration.LanguagePath, _Item);
		}

		public cLanguageItem GetLanguageByCode(string _LanguageCode)
		{
			if (LanguageList.ContainsKey(_LanguageCode))
			{
				return LanguageList[_LanguageCode];
			}
			return null;
		}

		public void LoadLanguages()
		{
			if (File.Exists(LanguageListFullPath))
			{
				 var __LanguageJSON = App.Handlers.FileHandler.ReadString(LanguageListFullPath);
				 JObject __LanguageJObject = JObject.Parse(__LanguageJSON);
				LanguageNameList = JsonConvert.DeserializeObject<List<cLanguageItemObject>>(__LanguageJObject["Languages"].ToString());
				LoadLanguagesInner();
			}
			else
			{
				throw new Exception("Dil listesi dosyası bulunamadı!");
			}
		}

		public void LoadLanguagesInner()
		{
			for (int i = 0; i < LanguageNameList.Count;i++)
			{
				LanguageList.Add(LanguageNameList[i].Code, new cLanguageItem(App, LanguageNameList[i], GetLanguageItemFullPath(LanguageNameList[i].Code + ".json")));				
			}
		}


		public string GetWordValue(string _Language, string _Word, params object[] _Parameters)
		{
			if (LanguageList.ContainsKey(_Language))
			{
				return LanguageList[_Language].GetWordValue(_Word, _Parameters);
			}
			else
			{
				throw new Exception("(" + _Language + ") Dili, liste içinde bulunamadı!");
			}
		}

        public void Reload()
		{
			LanguageNameList = new List<cLanguageItemObject>();
			LanguageList = new Dictionary<string, cLanguageItem>();
			LoadLanguages();
		}
    }
}
