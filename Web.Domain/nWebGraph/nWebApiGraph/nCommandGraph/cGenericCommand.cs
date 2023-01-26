using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Web.Domain.nWebGraph.nSessionManager;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nShowMessageAction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommandIDs;
using Web.Domain.nUtils.nValueTypes;
using Web.Domain.Controllers;
using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nUtils.nImpersonatedUserUtils;

namespace Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph
{
    public class cGenericCommand : cBaseCommand
    {
        List<cRecieverItem> ReceiverList = new List<cRecieverItem>();
        Type CommandDataClass = null;
        Type CommandReceiverClass = null;

        public cGenericCommand(cApp _App, cWebGraph _WebGraph, CommandIDs _Command)
                : base(_App, _WebGraph, _Command)
        {
            try
            {
                CommandDataClass = Type.GetType("Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.n" + CommandID.Name + "Command.c" + CommandID.Name + "CommandData");
                CommandReceiverClass = Type.GetType("Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.n" + CommandID.Name + "Command.I" + CommandID.Name + "Receiver");
            }
            catch (Exception _Ex)
            {
				App.Loggers.CoreLogger.LogError(_Ex);
				Console.WriteLine(CommandID.Name + " Komutunun Data Class'ı yada Receiver Interfaci Oluşturulmamış..!");
                Console.WriteLine(_Ex.StackTrace);
            }
        }

        public override void Interpret(IController _Controller, JToken _JToken)
        {
            try
            {
                JsonSerializerSettings __Settings = new JsonSerializerSettings
                {
                    Converters = new List<JsonConverter> { new cBadDateFixingConverter() },
                    DateParseHandling = DateParseHandling.None
                };

                Object __Data = JsonConvert.DeserializeObject(_JToken.ToString(), CommandDataClass, __Settings);

                ReceiverList = ReceiverList.OrderBy(__Item => __Item.Order).ToList();
                cListenerEvent __Event = new cListenerEvent(this);

                Type __ValidationGraphType = WebGraph.ValidationGraph.GetType();


                MethodInfo __Info = __ValidationGraphType.GetMethod("GetValidationByReceiverInterface");
                object __Validator = __Info.Invoke(WebGraph.ValidationGraph, new object[] { CommandReceiverClass });

                if (__Validator != null)
                {
                    Type __ValidatorType = __Validator.GetType();
                    MethodInfo __Receiver = __ValidatorType.GetMethod("Receive" + CommandID.Name + "Data", 0, new Type[] { typeof(cListenerEvent), typeof(IController), __Data.GetType() });
                    __Receiver.Invoke(__Validator, new object[] { __Event, _Controller, __Data });
                }
                

                /*ConstructorInfo __Info = __GenericType.GetConstructor(new Type[] { typeof(cQueryFilterOperand<TOwnerEntity, TEntity>), typeof(IQuery) });
                object __Result = __Info.Invoke(new object[] { this, _Query });
                Query.Filters.Add((cBaseCompareOperator<TOwnerEntity, TEntity>)__Result);
                return Filter;*/

                if (!__Event.IsPropogationStoped)
                {
                    for (int i = 0; i < ReceiverList.Count; i++)
                    {
                        Object __ReceiverObject = ReceiverList[i].Receiver;
                        try
                        {
                            MethodInfo __Receiver = CommandReceiverClass.GetMethod("Receive" + CommandID.Name + "Data", 0, new Type[] { typeof(cListenerEvent), typeof(IController), __Data.GetType() });
                            __Receiver.Invoke(__ReceiverObject, new object[] { __Event, _Controller, __Data });
                            if (__Event.IsPropogationStoped)
                            {
                                break;
                            }
                        }
                        catch (Exception _Ex)
                        {
                            Console.WriteLine(_Ex.StackTrace);
                            Console.WriteLine("I" + CommandID.Name + "Receiver Class'ının içinde Receive" + CommandID.Name + "Data methodunda sıkıntı var..!");

							WebGraph.ErrorMessageManager.ErrorAction(_Ex, _Controller, "Hata", "I" + CommandID.Name + "Receiver Class'ının içinde Receive" + CommandID.Name + "Data methodunda sıkıntı var..!" );
                        }
                    }
                }
            }
            catch (Exception _Ex)
            {
				//Console.WriteLine(_Ex.StackTrace);
				WebGraph.ErrorMessageManager.ErrorAction(_Ex, _Controller, "Hata", "Gelen veri çözümlenemiyor");
            }
        }


        public void Connect(Object _Receiver, int _Order = 0)
        {
            ReceiverList.Add(new cRecieverItem(_Receiver, _Order));
        }

        public void Disconnect(Object _Receiver)
        {
            cRecieverItem __RecieverItem = ReceiverList.Where(__Item => __Item.Receiver == _Receiver).ToList().FirstOrDefault();
            if (__RecieverItem != null)
            {
                ReceiverList.Remove(__RecieverItem);
            }            
        }
    }
}
