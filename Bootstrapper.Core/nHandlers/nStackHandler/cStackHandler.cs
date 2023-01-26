using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Linq;

namespace Bootstrapper.Core.nHandlers.nStackHandler
{
    public class cStackHandler : cCoreObject
    {
        public cStackHandler(nApplication.cApp _App)
            : base(_App)
        {
        }

		public override void Init()
		{
			App.Factories.ObjectFactory.RegisterInstance<cStackHandler>(this);
		}

		public bool SearchMethodMustCount(MethodInfo _MethodInfo, int _MustCount)
		{
			return SearchMethodMustCount(_MethodInfo.Name, _MustCount);
		}

		public bool SearchMethodMustCount(string _MethodName, int _MustCount)
		{
			StackTrace __StackTrace = new StackTrace(1, true);
			StackFrame[] __StackFrames = __StackTrace.GetFrames();
			return __StackFrames.Where(__Item => __Item.GetMethod().Name == _MethodName).ToList().Count == _MustCount;
		}

		public bool SearchMethodValidMaxCount(MethodInfo _MethodInfo, int _MaxValid)
		{
			return SearchMethodValidMaxCount(_MethodInfo.Name, _MaxValid);
		}

		public bool SearchMethodValidMaxCount(string _MethodName, int _MaxValid)
		{
			StackTrace __StackTrace = new StackTrace(1, true);
			StackFrame[] __StackFrames = __StackTrace.GetFrames();
			return __StackFrames.Where(__Item => __Item.GetMethod().Name == _MethodName).ToList().Count <= _MaxValid;
		}

		public List<MethodBase> GetMethods(string _MethodName, int _SkipFrame)
		{
			StackTrace __StackTrace = new StackTrace(_SkipFrame, true);
			StackFrame[] __StackFrames = __StackTrace.GetFrames();
			List<StackFrame> __StackFrameList = __StackFrames.Where(__Item => __Item.GetMethod().Name == _MethodName).ToList();
			return __StackFrameList.Select<StackFrame, MethodBase>(__Item => __Item.GetMethod()).ToList();
		}
	}
}
