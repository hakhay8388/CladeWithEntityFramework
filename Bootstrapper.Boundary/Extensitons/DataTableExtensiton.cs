using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// DataTablê'dan verdiğin sınıf şeklinde bir liste oluşturmaya çalışır.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <param name="_Table"></param>
/// <returns>List<TEntity></returns>
/// 
public static class DataTableExtensiton
{
    public static List<TEntity> ToList<TEntity>(this DataTable _Table)
    {
        List<TEntity> __Result = new List<TEntity>();
        foreach (DataRow __Row in _Table.Rows)
        {
            __Result.Add(__Row.Fill<TEntity>());
        }
        return __Result;
    }

    public static List<dynamic> ToDynamicObjectList(this DataTable _Table, Action<dynamic> _Action = null)
    {
        List<object> __Result = new List<object>();
        foreach (DataRow __Row in _Table.Rows)
        {

            ExpandoObject __Dynamic = new ExpandoObject();
            IDictionary<string, object> __UnderlyingObject = __Dynamic;

            for (int i = 0; i < _Table.Columns.Count; i++)
            {
                __UnderlyingObject.Add(_Table.Columns[i].ColumnName, __Row[_Table.Columns[i].ColumnName]);
            }

            __Result.Add(__Dynamic);
            _Action?.Invoke(__Dynamic);
        }
        return __Result;
    }
}

