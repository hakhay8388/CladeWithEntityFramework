using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;
using Unity.Builder;
using Unity.Strategies;

namespace Base.Web.nCustomDI
{
    public class cUnityFallbackProviderStrategy : BuilderStrategy
    {
        private IUnityContainer m_Container;

        public cUnityFallbackProviderStrategy(IUnityContainer _Container)
        {
            m_Container = _Container;
        }

       #region Overrides of BuilderStrategy

        /// <summary>
        /// Called during the chain of responsibility for a build operation. The
        /// PreBuildUp method is called when the chain is being executed in the
        /// forward direction.
        /// </summary>
        /// <param name="_Context">Context of the build operation.</param>
        public override void PreBuildUp(ref BuilderContext _Context)
        {
//            NamedTypeBuildKey key = context.OriginalBuildKey;
            

            // Checking if the Type we are resolving is registered with the Container
            if (!m_Container.IsRegistered(_Context.RegistrationType))
            {
                // If not we first get our default IServiceProvider and then try to resolve the type with it
                // Then we save the Type in the Existing Property of IBuilderContext to tell Unity
                // that it doesnt need to resolve the Type
                _Context.Existing = m_Container.Resolve<IServiceProvider>(cUnityFallbackProviderExtension.FALLBACK_PROVIDER_NAME).GetService(_Context.RegistrationType);
            }

            // Otherwise we do the default stuff
            base.PreBuildUp(ref _Context);
        }
        #endregion
    }
}
