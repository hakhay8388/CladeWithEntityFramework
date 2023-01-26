using Web.Domain.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nProgressStatusAction
{
	public class cProgressItem
	{
		public DateTime CreateTime { get; set; }

		public cSession Session { get; set; }
		public cProgressItem(cSession _Session)
		{
			Session = _Session;
		}

		public void RefreshValue()
		{
			CreateTime = DateTime.Now;
		}
	}
}
