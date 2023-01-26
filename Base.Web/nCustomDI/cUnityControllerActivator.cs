using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace Base.Web.nCustomDI
{
    public class cUnityControllerActivator : IControllerActivator
    {
        private IUnityContainer m_UnityContainer;

        public cUnityControllerActivator(IUnityContainer _Container)
        {
            m_UnityContainer = _Container;
        }

        public object Create(ControllerContext _Context)
        {
            return m_UnityContainer.Resolve(_Context.ActionDescriptor.ControllerTypeInfo.AsType());
        }


        public void Release(ControllerContext _Context, object _Controller)
        {
            //ignored
        }
    }
}
