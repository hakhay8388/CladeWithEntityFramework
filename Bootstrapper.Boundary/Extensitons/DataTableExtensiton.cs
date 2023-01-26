using System;
using System.Collections.Generic;
using System.Data;
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
}

