using Bootstrapper.Boundary.nCore.nObjectLifeTime;

using System;

namespace Bootstrapper.Core.nAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class Register : Attribute
    {
        public Type BindFrom { get; private set; }
        public bool BindThisAllBaseTypes { get; private set; }
        public bool CreateInstance { get; private set; }
        public bool RegisterInstance { get; private set; }
        public bool CallInit { get; private set; }
        public LifeTime LifetimeManager { get; private set; }

        public Register(Type _BindFrom = null, bool _BindThisAllBaseTypes = false, bool _CreateInstance = false, bool _RegisterInstance = false, bool _CallInit = false, LifeTime _LifetimeManager = LifeTime.ContainerControlledLifetimeManager)
        {
            BindFrom = _BindFrom;
            BindThisAllBaseTypes = _BindThisAllBaseTypes;
            CreateInstance = _CreateInstance;
            RegisterInstance = _RegisterInstance;
            CallInit = _CallInit;
            LifetimeManager = _LifetimeManager;
        }
    }
}
