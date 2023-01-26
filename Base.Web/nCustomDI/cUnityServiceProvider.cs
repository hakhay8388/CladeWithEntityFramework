using Bootstrapper.Core.nApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace Base.Web.nCustomDI
{
    public class cUnityServiceProvider : IServiceProvider
    {
        private IUnityContainer m_Montainer;

        public IUnityContainer UnityContainer => m_Montainer;

        public cUnityServiceProvider(IUnityContainer _IUnityContainer)
        {
            m_Montainer = _IUnityContainer;
        }

        #region Implementation of IServiceProvider

        /// <summary>Gets the service object of the specified type.</summary>
        /// <returns>A service object of type <paramref name="_ServiceType" />.-or- null if there is no service object of type <paramref name="_ServiceType" />.</returns>
        /// <param name="_ServiceType">An object that specifies the type of service object to get. </param>
        public object GetService(Type _ServiceType)
        {
            //Delegates the GetService to the Containers Resolve method
            try
            {
                return m_Montainer.Resolve(_ServiceType);
            }
            catch(Exception _Ex)
            {
				cApp.App.Loggers.CoreLogger.LogError(_Ex);
				IServiceProvider __IServiceProvider = m_Montainer.Resolve<IServiceProvider>();
                object __Result = __IServiceProvider.GetService(_ServiceType);
                return __Result;
            }
        }

        #endregion
    }
}
