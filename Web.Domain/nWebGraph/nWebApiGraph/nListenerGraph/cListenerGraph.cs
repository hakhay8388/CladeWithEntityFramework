using Web.Domain.nUtils;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootstrapper.Core.nCore;
using Bootstrapper.Core.nApplication;

namespace Web.Domain.nWebGraph.nWebApiGraph.nListenerGraph
{
    public class cListenerGraph : cCoreObject
    {
        static List<cBaseListener> ListenerList = null;

        public cListenerGraph(cApp _App)
            : base(_App)
        {
            ListenerList = new List<cBaseListener>();
        }

        public void Init()
        {
            List<Type> __Listeners = App.Handlers.AssemblyHandler.GetTypesFromBaseType<cBaseListener>();
            __Listeners.ForEach(__Type =>
            {
                cBaseListener __Listener = (cBaseListener)App.Factories.ObjectFactory.ResolveInstance(__Type);
                __Listener.Init();
                ListenerList.Add(__Listener);
            });
        }
        public TListenerType GetListenerByType<TListenerType>()
            where TListenerType : cBaseListener
        {
            TListenerType __Listener = (TListenerType)ListenerList.Where(__Item => typeof(TListenerType).IsAssignableFrom(__Item.GetType())).FirstOrDefault();
            return __Listener;
        }
    }
}
