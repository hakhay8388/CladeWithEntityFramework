using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;

namespace Bootstrapper.Core.nHandlers.nDateTimeHandler
{
    public class cDateTimeHandler : cCoreObject
    {
        public cDateTimeHandler(nApplication.cApp _App)
            : base(_App)
        {
        }

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cDateTimeHandler>(this);
		}

		public DateTime Now
		{
			get
			{
				return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTimeKind.Unspecified);
			}
		}

        public long ToTimestamp(DateTime _Date)
        {
            var date = new DateTime(1970, 1, 1, 0, 0, 0);
            var unixTimestamp = System.Convert.ToInt64((_Date - date).TotalSeconds);

            return unixTimestamp;
        }

        public string GetNow()
        {
            return DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }

        public long GetTimestampNow()
        {
            return ToTimestamp(DateTime.Now);
        }

        public DateTime ToDateTime(long _Timestamp)
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0);
            return dateTime.AddSeconds(_Timestamp);
        }

        public String GetDatetimeDiff(DateTime _BigDate, DateTime _SmallDate)
        {
            TimeSpan __TimeSpan = _BigDate.Subtract(_SmallDate);
            return __TimeSpan.ToString(@"hh\:mm\:ss");
        }
      

        public long GetDatetimeDiffInSeccond(DateTime _BigDate, DateTime _SmallDate)
        {
            TimeSpan __TimeSpan = _BigDate.Subtract(_SmallDate);
            return (long)__TimeSpan.TotalSeconds;
        }
    }
}
