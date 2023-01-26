using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nApplication.nCoreLoggers;
//using Bootstrapper.Core.nApplication.nCoreLoggers.nMethodCallLogger;
using Bootstrapper.Core.nAttributes;
using Bootstrapper.Core.nCore;
using Bootstrapper.Core.nExceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrapper.Core.nHandlers.nAssemblyHandler
{
	public class cAssemblyHandler : cCoreObject
	{
		public cAssemblyHandler(nApplication.cApp _App)
			: base(_App)
		{
		}

		public override void Init()
		{
			App.Factories.ObjectFactory.RegisterInstance<cAssemblyHandler>(this);
		}


		public bool AssemblyIsLoaded(string _FilePath)
		{
			return AppDomain.CurrentDomain.GetAssemblies().ToList().Exists(__Item => !__Item.IsDynamic && __Item.Location.ToLower() == _FilePath.ToLower());
		}

		public void LoadFromAssemblyPath(string _FilePath)
		{
			if (!AssemblyIsLoaded(_FilePath))
			{
				AssemblyLoadContext.Default.LoadFromAssemblyPath(_FilePath);
			}
		}

		public bool IsInApplicationDomain(List<string> _ApplicationDomain, string _Name)
		{
			if (!_Name.StartsWith("System.") && !_Name.StartsWith("Microsoft."))
			{
				foreach (string __Tags in _ApplicationDomain)
				{
					if (_Name.ToLower().StartsWith(__Tags.ToLower() + "."))
					{
						return true;
					}
				}
			}
			return false;
		}

		public List<Assembly> GetLoadedApplicationAssemblies(List<string> _ApplicationAssemblyList)
		{
			List<Assembly> __List = new List<Assembly>();

			foreach (Assembly __Assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				try
				{
					if (!__Assembly.IsDynamic || Path.GetExtension(__Assembly.Location).ToLower() == "exe")
					{
						if (IsInApplicationDomain(_ApplicationAssemblyList, Path.GetFileName(__Assembly.Location)))
						{
							if (__List.Find(__Item => __Item.FullName.Equals(__Assembly.FullName)) == null)
							{
								__List.Add(__Assembly);
							}
						}
					}
				}
				catch (Exception _Ex)
				{
					//App.Loggers.CoreLogger.LogError(_Ex);
				}
			}
			return __List;
		}

		public List<Type> GetTypes<TAttribute>(Assembly _Assembly)
		{
			return GetTypes(_Assembly, typeof(TAttribute));
		}
		public List<Type> GetTypes(Assembly _Assembly, Type _Attribute = null)
		{
			Type[] __Array = _Assembly.GetTypes();
			IEnumerable<Type> __Result = __Array.Where((_Type) =>
			{
				return _Attribute == null || _Type.GetCustomAttribute(_Attribute) != null;
			});
			List<Type> __ResultList = __Result.ToList();
			return __ResultList;
		}

		public List<Type> GetLoadedApplicationTypes(List<string> _ApplicationAssemblyList)
		{
			List<Type> __Result = new List<Type>();
			List<Assembly> __AssemblyList = GetLoadedApplicationAssemblies(_ApplicationAssemblyList);
			foreach (Assembly __Assembly in __AssemblyList)
			{
				__Result = __Result.Union(GetTypes(__Assembly)).ToList();
			}
			return __Result;
		}

		public List<Type> GetLoadedApplicationTypesByCustomAttribute<TCustomAttribute>(List<string> _ApplicationAssemblyList)
		{
			return GetLoadedApplicationTypes(_ApplicationAssemblyList).UseIsNotNullWhere(__Item => __Item.GetCustomAttribute(typeof(TCustomAttribute)) != null).ToList();
		}

		public List<Type> GetLoadedApplicationTypesByCustomAttribute<TCustomAttribute>(string _DomainName)
		{
			List<string> __DomainList = new List<string>();
			__DomainList.Add(_DomainName);
			return GetLoadedApplicationTypes(__DomainList).UseIsNotNullWhere(__Item => __Item.GetCustomAttribute(typeof(TCustomAttribute)) != null);
		}

		public bool IsSubType(Type _BaseType, Type _Extended)
		{
			if (_BaseType.IsAssignableFrom(_Extended))
			{
				return true;
			}
			return false;
		}

		public Type FindFirstType(string _TypeFullName)
		{
			List<Type> __Types = FindType(_TypeFullName);
			if (__Types.Count == 1) return __Types[0];
			throw new cCoreException(App, "Type Bulunamadı..?");
		}

		private List<Type> FindType(string _TypeFullName)
		{
			List<Type> __Result = new List<Type>();
			List<Assembly> __Assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
			__Assemblies.ForEach((__Assembly) =>
			{
				Type __Type = __Assembly.GetType(_TypeFullName, false);
				if (__Type != null) __Result.Add(__Type);
			});
			return __Result;

		}

		public List<Type> GetTypesFromBaseType<TType>(string _StartWithName = null, string _EndWithName = null)
		{
			return GetTypesFromBaseType(typeof(TType), _StartWithName, _EndWithName);
		}

		public List<Type> GetTypesFromBaseType(Type _BaseType, string _StartWithName = null, string _EndWithName = null)
		{
			List<Type> __AllTypes = GetLoadedApplicationTypes(App.Configuration.ApplicationSettings.DomainNames);
			return __AllTypes.Where(__Item => __Item.IsClass && !__Item.IsAbstract && !__Item.IsInterface && __Item.IsSubclassOf(_BaseType)
			&& (_StartWithName == null || (_StartWithName != null && __Item.Name.StartsWith(_StartWithName)))
            && (_EndWithName == null || (_EndWithName != null && __Item.Name.EndsWith(_EndWithName)))
			).ToList();

		}

		public List<Type> GetTypesFromBaseInterface<TType>()
		{
			return GetTypesFromBaseInterface(typeof(TType));
		}

		public List<Type> GetTypesFromBaseInterface(Type _BaseType)
		{
			List<Type> __AllTypes = GetLoadedApplicationTypes(App.Configuration.ApplicationSettings.DomainNames);
			return __AllTypes.Where(__Item => __Item.IsClass && !__Item.IsAbstract && !__Item.IsInterface && _BaseType.IsAssignableFrom(__Item)).ToList();
		
		}

        public Type GetTypeFromBaseInDomainHierarchy<TType>()
        {
            return GetTypeFromBaseInDomainHierarchy(typeof(TType));
        }

        public Type GetTypeFromBaseInDomainHierarchy(Type _BaseType)
        {
			for (int i = App.Configuration.ApplicationSettings.DomainNames.Count - 1; i > -1; i--)
			{
				List<Type> __AllTypes = GetLoadedApplicationTypes(new List<string>() { App.Configuration.ApplicationSettings.DomainNames[i] });
				__AllTypes = __AllTypes.Where(__Item => __Item.IsClass && !__Item.IsAbstract && !__Item.IsInterface && _BaseType.IsAssignableFrom(__Item)).ToList();
				if (__AllTypes.Count > 0)
				{
					return __AllTypes.FirstOrDefault();
				}
			}
			return null;
        }
    }
}
