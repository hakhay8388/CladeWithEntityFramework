using Bootstrapper.Core.nApplication;
using System;
using System.Collections.Generic;

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

    public static object CreateInstance(this Type _Type, cApp _App)
    {
        return Activator.CreateInstance(_Type);
    } 
}

