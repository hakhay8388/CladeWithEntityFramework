using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Eğer bir Extensition yazıyorsan namespace belirtmeden yazmazlısın.
/// </summary>
public static class DataRowExtensitons
{
    /// <summary>
    /// Verdiğin sınıfıntan bir instance oluşturur ve tablo içeriğindeki değişkenleri sınıftaki değişkenlere yerleştirip döndürür.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_Row"></param>
    /// <returns></returns>
    public static T Fill<T>(this DataRow _Row)
    {
        Type __Type = typeof(T);
        T __TempInstance = (T)Activator.CreateInstance(typeof(T));
        foreach (PropertyInfo __PropertyInfo in __Type.GetProperties())
        {
            if (_Row.Table.Columns.Contains(__PropertyInfo.Name))
            {
                if (_Row[__PropertyInfo.Name] != DBNull.Value)
                {
                    __PropertyInfo.GetSetMethod().Invoke(__TempInstance, new object[] { Convert.ChangeType(_Row[__PropertyInfo.Name], __PropertyInfo.PropertyType) });
                }
            }
        }
        return __TempInstance;
    }

    /// <summary>
    /// Verdiğin sınıf instance içeriğindeki değişkenleri DataRow kolonundaki bilgiler ile doldurur.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_Row"></param>
    /// <returns></returns>
    public static void Fill(this DataRow _Row, object _Entity)
    {
        Type __Type = _Entity.GetType();
        if (__Type.FullName.Contains("__Proxy__"))
        {
            __Type = __Type.BaseType;
        }

        foreach (PropertyInfo __PropertyInfo in __Type.GetProperties())
        {
            if (_Row.Table.Columns.Contains(__PropertyInfo.Name))
            {
                if (_Row[__PropertyInfo.Name] != DBNull.Value && __PropertyInfo.PropertyType != __Type && !__PropertyInfo.PropertyType.IsAssignableFrom(__Type))
                {
                      __PropertyInfo.GetSetMethod().Invoke(_Entity, new object[] { Convert.ChangeType(_Row[__PropertyInfo.Name], __PropertyInfo.PropertyType) });
                }
            }
        }
    }

}

