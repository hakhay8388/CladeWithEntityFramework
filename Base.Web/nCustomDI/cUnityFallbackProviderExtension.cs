using Bootstrapper.Boundary.nCore.nObjectLifeTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.Builder;
using Unity.Extension;
using Unity.Lifetime;

namespace Base.Web.nCustomDI
{
    public class cUnityFallbackProviderExtension : UnityContainerExtension
    {
        #region Const

        ///Used for Resolving the Default Container inside the UnityFallbackProviderStrategy class
        public const string FALLBACK_PROVIDER_NAME = "UnityFallbackProvider";

        #endregion

        #region Vars

        // The default Service Provider so I can Register it to the IUnityContainer
        private IServiceProvider m_DefaultServiceProvider;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of the UnityFallbackProviderExtension class
        /// </summary>
        /// <param name="defaultServiceProvider">The default Provider used to fall back to</param>
        public cUnityFallbackProviderExtension(IServiceProvider _DefaultServiceProvider)
        {
            m_DefaultServiceProvider = _DefaultServiceProvider;
        }

        #endregion

        #region Overrides of UnityContainerExtension

        /// <summary>
        /// Initializes the container with this extension's functionality.
        /// </summary>
        /// <remarks>
        /// When overridden in a derived class, this method will modify the given
        /// <see cref="T:Microsoft.Practices.Unity.ExtensionContext" /> by adding strategies, policies, etc. to
        /// install it's functions into the container.</remarks>
        protected override void Initialize()
        {
            // Register the default IServiceProvider with a name.
            // Now the UnityFallbackProviderStrategy can Resolve the default Provider if needed
            Context.Container.RegisterInstance(typeof(IServiceProvider), FALLBACK_PROVIDER_NAME, m_DefaultServiceProvider, (IInstanceLifetimeManager)LifeTimeEnumConverter.GetLifetimeManager(LifeTime.ContainerControlledLifetimeManager)) ;

            // Create the UnityFallbackProviderStrategy with our UnityContainer
            var __Strategy = new cUnityFallbackProviderStrategy(Context.Container);

            // Adding the UnityFallbackProviderStrategy to be executed with the PreCreation LifeCycleHook
            // PreCreation because if it isnt registerd with the IUnityContainer there will be an Exception
            // Now if the IUnityContainer "magically" gets a Instance of a Type it will accept it and move on
            Context.Strategies.Add(__Strategy, UnityBuildStage.PreCreation);
        }

        #endregion
    }
}
