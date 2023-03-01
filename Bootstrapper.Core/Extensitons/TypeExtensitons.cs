using AutoMapper;
using Bootstrapper.Core.nApplication;
using System;
using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// Eğer bir Extensition yazıyorsan namespace belirtmeden yazmazlısın.
/// </summary>
public static class TypeExtensitons
{
    public static T ResolveInstance<T>(this Type _Type, cApp _App)
    {
        if (!typeof(T).IsAssignableFrom(_Type)) throw new Exception("TypeExtensitons -> ResolveInstance");
        if (_App.Bootstrapper != null)
        {
            //if (_App.Factories.ObjectFactory.IsRegistered(_Type))
            //{
                return (T)_App.Factories.ObjectFactory.ResolveInstance(_Type);
            //}
        }
        throw new Exception("TypeExtensitons -> ResolveInstance");
    }

    public static void CopyProperties<T>(this T _Source, T _Destination)
    {
        PropertyInfo[] __Properties = typeof(T).GetProperties();

        foreach (PropertyInfo __Property in __Properties)
        {
            if (__Property.CanWrite)
            {
                object __Value = __Property.GetValue(_Source);
                __Property.SetValue(_Destination, __Value);
            }
        }
    }

}

