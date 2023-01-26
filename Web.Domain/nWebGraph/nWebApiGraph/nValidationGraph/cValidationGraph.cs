using Web.Domain.nUtils;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootstrapper.Core.nCore;
using Bootstrapper.Core.nApplication;

namespace Web.Domain.nWebGraph.nWebApiGraph.nValidationGraph
{
    public class cValidationGraph : cCoreObject
    {
        static List<cBaseValidation> ListenerList = null;

        public cValidationGraph(cApp _App)
            : base(_App)
        {
            ListenerList = new List<cBaseValidation>();
        }

        public override void Init()
        {
            List<Type> __Listeners = App.Handlers.AssemblyHandler.GetTypesFromBaseType<cBaseValidation>();
            __Listeners.ForEach(__Type =>
            {
                cBaseValidation __Listener = (cBaseValidation)App.Factories.ObjectFactory.ResolveInstance(__Type);
                __Listener.Init();
                ListenerList.Add(__Listener);
            });
        }

        public object GetValidationByReceiverInterface(Type _ReceiverInterface)
        {
            cBaseValidation __BaseValidation = ListenerList.Where(__Item => _ReceiverInterface.IsAssignableFrom(__Item.GetInterfaceType())).FirstOrDefault();
            if (__BaseValidation == null)
            {
                return null;
            }
            else
            {
                object __Result = __BaseValidation.CastObject();
                return __Result;
            }
        }
    }
}
