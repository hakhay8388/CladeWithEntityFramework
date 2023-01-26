
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections;
using System.Dynamic;
using System.ComponentModel;
using Base.Data.nDatabaseService.nDatabase;
using Bootstrapper.Core.nApplication;
using Newtonsoft.Json;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions
{
    public abstract class cBaseProps : IActionProps
    {
		public cBaseProps()
		{
			foreach (PropertyDescriptor __Property in TypeDescriptor.GetProperties(this.GetType()))
			{
				if (typeof(cBaseEntityType).IsAssignableFrom(__Property.PropertyType))
				{
					throw new Exception("Serialize edilecek objenin icine entity yollanamaz..!\nSerialize yavasliyor..!");
				}
			}
		}


		public List<dynamic> ConverToDynamicList(IList _List, Type _GenericType)
        {
            List<dynamic> __Result = new List<dynamic>();

            for (int i = 0; i < _List.Count; i++)
            {
                ExpandoObject __Dynamic = new ExpandoObject();
                IDictionary<string, object> __UnderlyingObject = __Dynamic;

                List<PropertyInfo> __PropertList = _GenericType.GetProperties().ToList();
                foreach (PropertyInfo __PropertyInfo in __PropertList)
                {
                    bool? __IsVirtual = __PropertyInfo.IsVirtual();
                    if (__IsVirtual != null && __IsVirtual.Value)
                    {
                        if (typeof(IList).IsAssignableFrom(__PropertyInfo.PropertyType))
                        {
                            Object __Object = __PropertyInfo.GetGetMethod().Invoke(this, new object[] { });
                            Type __Type = __Object.GetType();

                            if (__Type.IsGenericType && __Type.GetGenericTypeDefinition() == typeof(List<>))
                            {
                                Type __ItemType = __Type.GetGenericArguments()[0];
                                ConverToDynamicList((IList)__Object, __ItemType);
                            }
                            else
                            {
                                __UnderlyingObject.Add(__PropertyInfo.Name, __Object);
                            }
                        }
                        else
                        {
                            __UnderlyingObject.Add(__PropertyInfo.Name, __PropertyInfo.GetGetMethod().Invoke(_List[i], new object[] { }));
                        }
                    }
                }
            }
            return __Result;
        }

        
        public virtual JObject SerializeObject()
        {

			foreach (PropertyDescriptor __Property in TypeDescriptor.GetProperties(this.GetType()))
			{
				if (typeof(cBaseEntityType).IsAssignableFrom(__Property.PropertyType))
				{
					throw new Exception("Serialize edilecek objenin icine entity yollanamaz..!\nSerialize yavasliyor..!");
				}
			}


			JObject __JObject = JObject.FromObject(this, new JsonSerializer()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

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
                    catch(Exception _Ex)
                    {
						cApp.App.Loggers.CoreLogger.LogError(_Ex);
						// şimdilik buraya düşen varmı diye kontrol için konuldu
						// daha sonra kaldırılacak
						throw _Ex;
                    }
                }
            }
            return __JObject;
        }
    }
}
