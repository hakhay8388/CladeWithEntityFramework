using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ObjectExtensitions
{
    public static bool IsDBNull(this object value)
    {
        return (value == null || value.Equals(DBNull.Value) || (string.IsNullOrEmpty(value.ToString())));
    }
    public static dynamic ToDynamic(this object value)
    {
        IDictionary<string, object> expando = new ExpandoObject();
        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
        {
            expando.Add(property.Name, property.GetValue(value));
        }
        return expando as ExpandoObject;
    }
    public static bool In<T>(this T item, params T[] items)
    {
        //if (items == null) throw new CoreException(RS.Message.E1036_InItemsCanNotBeNull);
        return items.Contains(item);
    }
}

