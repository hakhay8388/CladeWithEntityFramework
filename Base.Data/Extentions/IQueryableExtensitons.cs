using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Eğer bir Extensition yazıyorsan namespace belirtmeden yazmazlısın.
/// </summary>
public static class IQueryableExtensitons
{
   
    public static DataTable ToDataTable<TEntity>(this IQueryable<TEntity> _Query)
    {
        DataTable __DataTable = new DataTable(typeof(TEntity).Name);

        //Get all the properties
        PropertyInfo[] __Props = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (PropertyInfo prop in __Props)
        {
            //Setting column names as Property names
            __DataTable.Columns.Add(prop.Name);
        }
        List<TEntity> __List = _Query.ToList();
        foreach (TEntity __Item in __List)
        {
            var __Values = new object[__Props.Length];
            for (int i = 0; i < __Props.Length; i++)
            {
                //inserting property values to datatable rows
                __Values[i] = __Props[i].GetValue(__Item, null);
            }
            __DataTable.Rows.Add(__Values);
        }
        //put a breakpoint here and check datatable
        return __DataTable;
    }

    public static List<dynamic> ToDynamicObjectList<TEntity>(this IQueryable<TEntity> _Query, Action<dynamic> _Action = null)
    {
        DataTable __DataTable = _Query.ToDataTable();
        List<object> __Result = new List<object>();
        foreach (DataRow __Row in __DataTable.Rows)
        {

            ExpandoObject __Dynamic = new ExpandoObject();
            IDictionary<string, object> __UnderlyingObject = __Dynamic;

            for (int i = 0; i < __DataTable.Columns.Count; i++)
            {
                __UnderlyingObject.Add(__DataTable.Columns[i].ColumnName, __Row[__DataTable.Columns[i].ColumnName]);
            }

            __Result.Add(__Dynamic);
            _Action?.Invoke(__Dynamic);
        }
        return __Result;
    }

}

