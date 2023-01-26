using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bootstrapper.Core.nHandlers.nLanguageHandler
{
    public class cLanguageItem : cCoreObject
    {
        public cLanguageItemObject Language { get; private set; }
        public JObject LanguageObject { get; set; }


        private string LanguageFileFullPath { get; set; }
        public cLanguageItem(cApp _App, cLanguageItemObject _Language, string _LanguageFileFullPath)
            : base(_App)
        {
            Language = _Language;
            LanguageFileFullPath = _LanguageFileFullPath;
            LoadLanguage();
        }

        private void LoadLanguage()
        {
            if (File.Exists(LanguageFileFullPath))
            {
                var __LanguageJSON = App.Handlers.FileHandler.ReadString(LanguageFileFullPath);
                LanguageObject = JObject.Parse(__LanguageJSON);
				/*foreach (var __Test in LanguageObject)
				{
					Console.WriteLine(__Test.Key);
					Console.WriteLine(__Test.Value["message"]);
				}*/
			}
            else
            {
                throw new Exception("Dil dosyası bulunamadı!");
            }
        }

		public string GetCheckSum()
		{
			if (File.Exists(LanguageFileFullPath))
			{
				return App.Handlers.FileHandler.GetFileChecksum(LanguageFileFullPath);
			}
			else
			{
				throw new Exception("Dil dosyası bulunamadı!");
			}
		}


		public string GetWordValue(string _Word, params object[] _Parameters)
        {
            if (LanguageObject.ContainsKey(_Word))
            {
                if (_Parameters != null && _Parameters.Length > 0)
                {
                    return string.Format((string)LanguageObject[_Word]["message"], _Parameters);
                }
                else
                {
                    return (string)LanguageObject[_Word]["message"];

                }

            }
            else
            {
                return "(" + _Word + ") Kelimesi karşılığı (" + Language + ") dilinde bulunamadı!";
            }
        }

    }
}
