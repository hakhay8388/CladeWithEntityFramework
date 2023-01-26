using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nApplication.nCoreLoggers;
using Bootstrapper.Core.nAttributes;
using Bootstrapper.Core.nCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrapper.Core.nApplication.nFactories.nHookedObjectFactory.nPropertyHookedObjectFactory
{
    public class cPropertyHookedObjectFactory : cCoreObject
    {
        private MethodInfo PropertyGetterEventDistributor_Method { get; set; }
        private MethodInfo ConstructorEventDistributor_Method { get; set; }
        private Dictionary<Type, Type> TypeToDynamicTypeMap { get; set; }
        private ModuleBuilder ModuleBuilder { get; set; }

        public cPropertyHookedObjectFactory(cApp _App)
            : base(_App)
        {
            TypeToDynamicTypeMap = new Dictionary<Type, Type>();
            ModuleBuilder = GetModuleBuilder();
            PropertyGetterEventDistributor_Method = this.GetType().GetMethod("PropertyGetterEventDistributor");
            ConstructorEventDistributor_Method = this.GetType().GetMethod("ConstructorEventDistributor");
        }

        public void PropertyGetterEventDistributor(object _Instance, string _PropertyName, object _PropertyInner)
        {
            cPropertyHookController __PropertyHookController = (cPropertyHookController)_Instance;
            __PropertyHookController.HookedFuction(_Instance, _PropertyName, _PropertyInner);
        }

        public void ConstructorEventDistributor(object instance)
        {
        }

        public object GetInstance(Type _Type, params object[] _Params)
        {
            if (typeof(cPropertyHookController).IsAssignableFrom(_Type))
            {
                lock (this)
                {
                    Type __DynamicType = GetMappedDynamicType(_Type);
                    if (__DynamicType == null)
                    {
                        TypeBuilder __TypeBuilder = GetTypeBuilder(_Type);
                        __DynamicType = __TypeBuilder.CreateType();
                        TypeToDynamicTypeMap.Add(_Type, __DynamicType);
                    }
                    cPropertyHookController __Instance = (cPropertyHookController)__DynamicType.CreateInstance(_Params);
                    __Instance.App = App;
                    return __Instance;
                }
            }
            throw new Exception("Dynamic proxy Error!");
        }

        public T GetInstance<T>(params object[] _Params) where T : cPropertyHookController
        {
            return (T)GetInstance(typeof(T), _Params);
        }


        private Type GetMappedDynamicType(Type _Type)
        {
            return TypeToDynamicTypeMap.ContainsKey(_Type) ? TypeToDynamicTypeMap[_Type] : null;
        }

        private ModuleBuilder GetModuleBuilder()
        {
            AssemblyName __AssemblyName = new AssemblyName("PropertyHookHandlerGenerator");
            AppDomain __Domain = AppDomain.CurrentDomain;
            AssemblyBuilder __AssemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(__AssemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder __ModuleBuilder = __AssemblyBuilder.DefineDynamicModule(__AssemblyName.Name);
            return __ModuleBuilder;
        }

        private TypeBuilder GetTypeBuilder(Type _Type)
        {
            TypeBuilder __TypeBuilder = null;
            string __DynamicTypeName = Assembly.CreateQualifiedName(_Type.AssemblyQualifiedName, _Type.Name + "__Proxy__");
            __TypeBuilder = ModuleBuilder.DefineType(__DynamicTypeName, TypeAttributes.Public | TypeAttributes.Class, _Type);

            foreach (PropertyInfo __PropertyInfo in _Type.GetProperties())
            {
                //if (!__PropertyInfo.PropertyType.IsPrimitiveWithString())
                //{
                OverrideProperty(__PropertyInfo, __TypeBuilder);
                //}
            }
            return __TypeBuilder;
        }

        private void OverrideProperty(PropertyInfo _PropertyInfo, TypeBuilder _TypeBuilder)
        {
            PropertyBuilder __PropertyBuilder = _TypeBuilder.DefineProperty(_PropertyInfo.Name, System.Reflection.PropertyAttributes.HasDefault, _PropertyInfo.PropertyType, null);

            MethodAttributes __MethodAttributes = MethodAttributes.Virtual
                                                | MethodAttributes.Public
                                                | MethodAttributes.HideBySig
                                                | MethodAttributes.SpecialName;

            FieldBuilder __ValueField = _TypeBuilder.DefineField("_" + _PropertyInfo.Name, _PropertyInfo.PropertyType, FieldAttributes.Private);

            SetGetMethod(_TypeBuilder, __PropertyBuilder, _PropertyInfo, __MethodAttributes, __ValueField);
            SetSetMethod(_TypeBuilder, __PropertyBuilder, _PropertyInfo, __MethodAttributes, __ValueField);
        }

        private void SetGetMethod(TypeBuilder _TypeBuilder,
                                    PropertyBuilder _PropertyBuilder,
                                    PropertyInfo _PropertyInfo,
                                    MethodAttributes _MethodAttributes,
                                    FieldBuilder _ValueField)
        {
            MethodBuilder __MethodBuilder = _TypeBuilder.DefineMethod("get_" + _PropertyInfo.Name, _MethodAttributes, _PropertyInfo.PropertyType, Type.EmptyTypes);
            ILGenerator __Generator = __MethodBuilder.GetILGenerator();

            __Generator.Emit(OpCodes.Ldarg_0);
            __Generator.Emit(OpCodes.Ldarg_0);
            __Generator.Emit(OpCodes.Ldstr, _PropertyInfo.Name);
            __Generator.Emit(OpCodes.Ldarg_0);
            __Generator.Emit(OpCodes.Ldfld, _ValueField);
            __Generator.Emit(OpCodes.Call, PropertyGetterEventDistributor_Method);

            __Generator.Emit(OpCodes.Ldarg_0);
            __Generator.Emit(OpCodes.Ldfld, _ValueField);
            __Generator.Emit(OpCodes.Ret);

            _PropertyBuilder.SetGetMethod(__MethodBuilder);
        }
        private void SetSetMethod(TypeBuilder _TypeBuilder,
                            PropertyBuilder _PropertyBuilder,
                            PropertyInfo _PropertyInfo,
                            MethodAttributes _MethodAttributes,
                            FieldBuilder _ValueField)
        {
            MethodBuilder __MethodBuilder = _TypeBuilder.DefineMethod("set_" + _PropertyInfo.Name, _MethodAttributes, typeof(void), new Type[] { _PropertyInfo.PropertyType });
            ILGenerator __Generator = __MethodBuilder.GetILGenerator();

            __Generator.Emit(OpCodes.Ldarg_0);
            __Generator.Emit(OpCodes.Ldarg_1);
            __Generator.Emit(OpCodes.Stfld, _ValueField);
            __Generator.Emit(OpCodes.Ret);

            _PropertyBuilder.SetSetMethod(__MethodBuilder);
        }
    }
}

