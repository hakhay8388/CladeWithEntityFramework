using System;
using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// Eğer bir Extensition yazıyorsan namespace belirtmeden yazmazlısın.
/// </summary>
public static class TypeExtensitons
{

    public static object CreateInstance(this Type _Type, params object[] _Params)
    {
        List<Type> __ConstorType = new List<Type>();
        foreach (object __Param in _Params)
        {
            __ConstorType.Add(__Param.GetType());
        }

        return _Type.GetConstructor(__ConstorType.ToArray()).Invoke(_Params);
    }

    public static bool IsPrimitiveWithString(this Type _Type)
    {
        return _Type.IsPrimitive || _Type == typeof(string) || _Type == typeof(decimal);
    }

    public static void SetPropertyValue(this Type _Type, object _Object, string _PropertyName, object _Value, bool _ReturnExceptionIfNotFoundMethod = true)
    {
        PropertyInfo __PropertyInfo = _Type.SearchProperty(_PropertyName);
        if (__PropertyInfo != null)
        {
            MethodInfo __MethodInfo = __PropertyInfo.GetSetMethod(true);
            __MethodInfo.Invoke(_Object, new object[] { _Value });
        }
        else if (_ReturnExceptionIfNotFoundMethod)
        {
             new Exception(_Type.Name + "." + _PropertyName + " property değişkeni bulunamadı!"); 
        }
    }

}

