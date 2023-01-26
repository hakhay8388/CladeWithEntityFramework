using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Base.Data.nDatabaseService.nDatabase;
using Bootstrapper.Core.nApplication;
using Newtonsoft.Json.Linq;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nResultItemAction
{
    public class cResultItemProps : cBaseProps
    {
        public virtual object Item { get; set; }

        private List<string> ItemList { get; set; }
        public cResultItemProps()
        {
            ItemList = new List<string>();
        }

        public void AddItemsToObject<TEntity>(Expression<Func<TEntity, object>> _PropertyExpression) where TEntity : cBaseEntityType
        {
            string __PropName = cApp.App.Handlers.LambdaHandler.GetParamPropName<TEntity>(_PropertyExpression);
            ItemList.Add(__PropName);
        }

        /*public override JObject SerializeObject()
        {
            JObject __JObject = base.SerializeObject();

            if (ItemList.Count > 0)
            {
                List<PropertyInfo> __PropertList = Item.GetType().GetProperties().ToList();

                foreach (string __Item in ItemList)
                {
                    PropertyInfo __PropertyInfo = __PropertList.Where(__PropItem => __PropItem.Name == __Item).ToList().FirstOrDefault();

                    if (__PropertyInfo != null && typeof(IMappedEntity).IsAssignableFrom(__PropertyInfo.PropertyType))
                    {
                        IMappedEntity __MappedEntity = (IMappedEntity)__PropertyInfo.GetValue(Item);
                        if (__MappedEntity != null)
                        {
                            //IList __TempList = __MappedEntity.ToIList();
                            //JArray __JArray = JArray.FromObject(__TempList);
                            __JObject["Item"][__Item] = __MappedEntity.ToJArray();
                        }
                    }
                }
                /*
                            foreach (PropertyInfo __PropertyInfo in __PropertList)
                            {
                                if (ItemList.Where(__Item => __Item == __PropertyInfo.Name).ToList().Count > 0)
                                {
                                    if (typeof(IMappedEntity).IsAssignableFrom(__PropertyInfo.PropertyType))
                                    {
                                        IMappedEntity __MappedEntity = (IMappedEntity)__PropertyInfo.GetValue(Item);
                                        if (__MappedEntity != null)
                                        {
                                            __JObject[Item][__PropertyInfo.Name] = JArray.FromObject(__MappedEntity.ToIList());
                                        }                        
                                    }
                                }
                            }
                */
           /*
            }
            return __JObject;
        }*/
    }
}
