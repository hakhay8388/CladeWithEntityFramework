using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

public static class ReflectionExtensition 
{
    public static BindingFlags CreateFlagsFor(Type _Type, bool _IgnoreFlags = false)
    {
        BindingFlags __Flags = BindingFlags.Default;
        if (_Type == null)
        {
            return __Flags;
        }
        __Flags = BindingFlags.Public | BindingFlags.Instance;
        if (_IgnoreFlags)
        {
            __Flags = BindingFlags.Public |
                                 BindingFlags.NonPublic |
                                 BindingFlags.Static |
                                 BindingFlags.Instance |
                                 BindingFlags.DeclaredOnly;
        }
        return __Flags;
    }
    public static FieldInfo[] GetAllFields(this Type _Type, BindingFlags _BindingFlags = BindingFlags.Default)
    {
        BindingFlags __Flags = _BindingFlags == BindingFlags.Default ? CreateFlagsFor(_Type, true) : _BindingFlags;
        if (__Flags != BindingFlags.Default)
        {
            return _Type.GetFields(__Flags).Union(GetAllFields(_Type.BaseType)).ToArray();
        }
        else
        {
            return Enumerable.Empty<FieldInfo>().ToArray();
        }
    }

    public static FieldInfo GetField(this Type _Type, string _FieldName, bool _IgnoreFlags)
    {
        BindingFlags __Flags = CreateFlagsFor(_Type, true);
        if (__Flags != BindingFlags.Default)
        {
            return _Type.GetField(_FieldName, __Flags);
        }
        else
        {
            return null;
        }        
    }

    public static FieldInfo SearchField(this Type _Type, string _Name)
    {
        BindingFlags __Flags = CreateFlagsFor(_Type, true);
        if (__Flags != BindingFlags.Default)
        {
            return _Type.GetField(_Name, __Flags) ?? SearchField(_Type.BaseType, _Name);
        }
        else
        {
            return null;
        }
    }

    public static MethodInfo[] GetAllMethods(this Type _Type, BindingFlags _BindingFlags = BindingFlags.Default)
    {
        BindingFlags __Flags = _BindingFlags == BindingFlags.Default ? CreateFlagsFor(_Type, true) : _BindingFlags;
        if (__Flags != BindingFlags.Default)
        {
            return _Type.GetMethods(__Flags).Union(GetAllMethods(_Type.BaseType)).ToArray();
        }
        else
        {
            return Enumerable.Empty<MethodInfo>().ToArray();
        }
    }

    public static MethodInfo GetMethod(this Type _Type, string _FieldName, bool _IgnoreFlags)
    {
        BindingFlags __Flags = CreateFlagsFor(_Type, true);
        if (__Flags != BindingFlags.Default)
        {
            return _Type.GetMethod(_FieldName, __Flags);
        }
        else
        {
            return null;
        }        
    }


    public static MethodInfo SearchMethod(this Type _Type, string _Name)
    {
        BindingFlags __Flags = CreateFlagsFor(_Type, true);
        if (__Flags != BindingFlags.Default)
        {
            return _Type.GetMethod(_Name, __Flags) ?? SearchMethod(_Type.BaseType, _Name);
        }
        else
        {
            return null;
        }
    }
    
    public static PropertyInfo[] GetAllProperties(this Type _Type, BindingFlags _BindingFlags = BindingFlags.Default)
    {
        BindingFlags __Flags = _BindingFlags == BindingFlags.Default ? CreateFlagsFor(_Type, true) : _BindingFlags;
        if (__Flags != BindingFlags.Default)
        {
            return _Type.GetProperties(__Flags).Union(GetAllProperties(_Type.BaseType)).ToArray();
        }
        else
        {
            return Enumerable.Empty<PropertyInfo>().ToArray();
        }
    }

    public static PropertyInfo GetProperty(this Type _Type, string _PropertyName, bool _IgnoreFlags)
    {
        BindingFlags __Flags = CreateFlagsFor(_Type, true);
        if (__Flags != BindingFlags.Default)
        {
            return _Type.GetProperty(_PropertyName, __Flags);
        }
        else
        {
            return null;
        }
    }

    public static PropertyInfo SearchProperty(this Type _Type, string _Name)
    {
        BindingFlags __Flags = CreateFlagsFor(_Type, true);
        if (__Flags != BindingFlags.Default)
        {
            return _Type.GetProperty(_Name, __Flags) ?? SearchProperty(_Type.BaseType, _Name);
        }
        else
        {
            return null;
        }
    }

    public static ConstructorInfo[] GetAllConstructors(this Type _Type, BindingFlags _BindingFlags = BindingFlags.Default)
    {
        BindingFlags __Flags = _BindingFlags == BindingFlags.Default ? CreateFlagsFor(_Type, true) : _BindingFlags;
        if (__Flags != BindingFlags.Default)
        {
            return _Type.GetConstructors(__Flags).ToArray();
        }
        else
        {
            return Enumerable.Empty<ConstructorInfo>().ToArray();
        }
    }


    public static object GetStaticPropertyValue(this Type _Type, string _PropertyName)
    {
        PropertyInfo __PropertyInfo = SearchProperty(_Type, _PropertyName);
        if (__PropertyInfo == null)
        {
            throw new Exception(_Type.Name + " Class'i " + _PropertyName  + " adinda bir degiskeni yok!");
        }
        return __PropertyInfo.GetValue(null);
    }

}
