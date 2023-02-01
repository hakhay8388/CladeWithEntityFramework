using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using Newtonsoft.Json;

namespace Web.Domain.nWebGraph.nNotificationManager.nNotifications
{
    public class cBaseNotificationProps
    {
        public virtual string SerializeObject()
        {
            JObject __JObject = JObject.FromObject(this);

            List<PropertyInfo> __PropertList = this.GetType().GetProperties().ToList();
            foreach (PropertyInfo __PropertyInfo in __PropertList)
            {
                bool? __IsVirtual = __PropertyInfo.IsVirtual();
                if (!(__IsVirtual != null && __IsVirtual.Value))
                {
                    try
                    {
                        __JObject.Remove(__PropertyInfo.Name);
                    }
                    catch (Exception _Ex)
                    {
						// şimdilik buraya düşen varmı diye kontrol için konuldu
						// daha sonra kaldırılacak
						throw _Ex;
                    }
                }
            }
            string __Result = JsonConvert.SerializeObject(__JObject);
            __Result = __Result.Replace("{", "{{").Replace("}", "}}");
            return __Result;
        }

        public static TNotificationProps GetPropFromString<TNotificationProps>(string _SerializedObject)
        {
            _SerializedObject = _SerializedObject.Replace("{{", "{").Replace("}}", "}");
            return (TNotificationProps)JsonConvert.DeserializeObject<TNotificationProps>(_SerializedObject);
        }
    }
}
