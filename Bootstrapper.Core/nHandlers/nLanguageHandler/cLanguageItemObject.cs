using System;
using System.Collections.Generic;
using System.Text;

namespace Bootstrapper.Core.nHandlers.nLanguageHandler
{
	public class cLanguageItemObject
	{
		public string Name { get; set; }

		public string Code { get; set; }

		public string IconCode { get; set; }
		public List<string> HrefLangs { get; set; }

	}
}
