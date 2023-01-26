using Bootstrapper.Boundary.nCore.nObjectLifeTime;
using Bootstrapper.Core.nApplication.nBootstrapper;
using Bootstrapper.Core.nApplication.nCoreLoggers;
using Bootstrapper.Core.nAttributes;
using Bootstrapper.Core.nCore;
using Bootstrapper.Core.nHandlers.nAssemblyHandler;
using Bootstrapper.Core.nHandlers.nReflectionHandler;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;
using Unity.Registration;

namespace Bootstrapper.Core.nApplication.nFactories.nObjectFactory
{
    public class cObjectFactory : cCoreObject
    {
        public IUnityContainer DependencyContainer { get; set; }
        public cObjectFactory(cApp _App)
            :base(_App)
        {
            DependencyContainer = new UnityContainer().EnableDiagnostic();
        }

        public override void Init()
        {
           RegisterInstance<cObjectFactory>(this);
        }

        public Type GetRegisteredTypeFromMappedType(Type _Type)
        {
            foreach (IContainerRegistration __Registration in DependencyContainer.Registrations)
            {
                if (__Registration.MappedToType == _Type)
                {
                    return __Registration.RegisteredType;
                }
            }
            return null;
        }
        public Type GetMappedTypeFromRegisteredType(Type _Type)
        {
            foreach (IContainerRegistration __Registration in DependencyContainer.Registrations)
            {
                if (__Registration.RegisteredType == _Type)
                {
                    return __Registration.MappedToType;
                }
            }
            return null;
        }
        public bool IsRegistered(Type _Type)
        {
            return DependencyContainer.IsRegistered(_Type);
        }
        public bool IsRegistered<T>()
        {
            return DependencyContainer.IsRegistered<T>();
        }
        public T ResolveInstance<T>()
        {
            return (T)ResolveInstance(typeof(T));
        }
        public object ResolveInstance(Type _Type)
        {
            return ResolveInstance(new Dictionary<object, object>(), _Type);
        }
        private object ResolveInstance(Dictionary<object, object> _ParentList, Type _Type)
        {
            object __MappedInstance = null;
            try
            {
                App.Loggers.CoreLogger.DebugLog("ResolveInstance<T>() started : " + _Type.Name);
                object __Instance = DependencyContainer.Resolve(_Type);
                 __MappedInstance = __Instance;
                 
                _ParentList.Add(__Instance, __MappedInstance);
                //ResolveInnerObject(_ParentList, __Instance);
                App.Loggers.CoreLogger.DebugLog("ResolveInstance<T>() ended : " + _Type.Name);
            }
            catch (Exception _Ex)
            {
				List<string> __List = new List<string>();
				__List.Add(string.Format("ResolveInstance<T>() failed : {0}", _Ex.Message));
				App.Loggers.CoreLogger.LogError(__List, _Ex, null);
				throw _Ex;
            }
            return __MappedInstance;
        }

        public void RegisterInstance<T>(T _Instance)
        {
            RegisterInstance(typeof(T), _Instance); ;
        }
        public object RegisterInstance(Type _Type, object _Instance)
        {
            //App.Loggers.CoreLogger.DebugLog("Instance Bind : " + _Type.Name + " -> " + _Instance.GetType().Name);

            DependencyContainer.RegisterInstance(_Type, _Instance);
            return _Instance;
        }
        public void RegisterType<T>(LifeTime _LifetimeManager)
        {
            App.Loggers.CoreLogger.DebugLog("Type Bind : " + typeof(T).Name + " -> " + typeof(T).Name + "  ,  Life : " + _LifetimeManager.ToString());

            DependencyContainer.RegisterType<T>(LifeTimeEnumConverter.GetLifetimeManager(_LifetimeManager));
        }
        public void RegisterType<TFrom, TTo>(LifeTime _LifetimeManager)
        {
            RegisterType(typeof(TFrom), typeof(TTo), _LifetimeManager);
        }

        public void RegisterType(Type _FromType, Type _ToType, LifeTime _LifetimeManager)
        {
            App.Loggers.CoreLogger.DebugLog("Type  Bind : " + _FromType.Name + " -> " + _ToType.Name + "  ,  Life : " + _LifetimeManager.ToString());
            DependencyContainer.RegisterType(_FromType, _ToType, LifeTimeEnumConverter.GetLifetimeManager(_LifetimeManager));
        }

        public void RegisterFactory<TFrom>(Func<TFrom> _Function, LifeTime _LifetimeManager)
        {
            DependencyContainer.RegisterFactory<TFrom>((IUnityContainer _UnityContainer) =>
            {
                return (TFrom)_Function();
            }, LifeTimeEnumConverter.GetLifetimeManagerForFactory(_LifetimeManager));
        }

        public IUnityContainer CreateChildContainer()
        {
            return DependencyContainer.CreateChildContainer();
        }
        public void RemoveAll()
        {
            foreach (ContainerRegistration __Registration in DependencyContainer.Registrations)
            {
                if (__Registration.LifetimeManager != null)
                {
                    __Registration.LifetimeManager.RemoveValue();
                }
            }
        }
        
        public void ResolveInnerObject(object _Object)
        {
            ResolveInnerObject(new Dictionary<object, object>(), _Object);
        }

        private void ResolveInnerObject(Dictionary<object, object> _ParentList, object _Object)
        {
            //cReflectionHandler __ReflectionUtils = new cReflectionHandler();
            bool __FirstCreate = false;
            PropertyInfo[] _PropertyInfo = _Object.GetType().GetAllProperties();
            foreach (PropertyInfo __PropertyInfo in _PropertyInfo)
            {
                if (App.Handlers.AssemblyHandler.IsInApplicationDomain(App.Configuration.ApplicationSettings.DomainNames, __PropertyInfo.PropertyType.Namespace.Substring(0, __PropertyInfo.PropertyType.Namespace.IndexOf(".") + 1)))
                {
                    if ((__PropertyInfo.GetMethod != null && __PropertyInfo.GetValue(_Object) == null) && __PropertyInfo.SetMethod != null)
                    {
                        object __Instance = null;

                        for (int i = _ParentList.Count - 1; i > -1; i--)
                        {
                            var __Item = _ParentList.ElementAt(i);
                            if (__Item.Key.GetType() == __PropertyInfo.PropertyType)
                            {
                                __Instance = __Item.Value;
                                break;
                            }
                        }

                        if (__Instance == null)
                        {
                            __Instance = ResolveInstance(_ParentList, __PropertyInfo.PropertyType);
                        }


                        if (__Instance != null)
                        {
                            __PropertyInfo.SetValue(_Object, __Instance);
                            __FirstCreate = true;
                        }
                    }
                }
            }

            if (__FirstCreate)
            {
                MethodInfo __MethodInfo = _Object.GetType().SearchMethod("Init");
                if (__MethodInfo != null)
                {
                    __MethodInfo.Invoke(_Object, new object[] { });
                }
            }
        }
    }
}
