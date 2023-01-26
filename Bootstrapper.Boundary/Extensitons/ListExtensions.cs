using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
/// <summary>
/// Eğer bir Extensition yazıyorsan namespace belirtmeden yazmazlısın.
/// </summary>
public static class ListExtensions
{
    public static DataTable ToDataTable<T>(this List<T> _List)
    {
        DataTable __Table = new DataTable();
        if (_List != null)
        {
            Type __Type = _List.GetType().GetGenericArguments()[0];
            foreach (FieldInfo __Field in __Type.GetFields())
            {
                if (!__Field.IsStatic)
                {
                    __Table.Columns.Add(__Field.Name, __Field.FieldType);
                }
            }
            for (int i = 0; i < _List.Count; i++)
            {
                DataRow __Row = __Table.NewRow();
                foreach (FieldInfo __Field in __Type.GetFields())
                {
                    if (!__Field.IsStatic)
                    {
                        __Row[__Field.Name] = __Field.GetValue(_List[i]);
                    }
                }
                __Table.Rows.Add(__Row);
            }
        }
        return __Table;
    }

    public static List<T> UseIsNotNullWhere<T>(this List<T> _List, Func<T, bool> predicate)
    {
        if (_List != null)
        {
            return _List.Where(predicate).ToList();
        }
        return new List<T>();
    }

    public static object[] ToObjectArray<T>(this List<T> _List)
    {
        object[] __Result = new object[_List.Count];
        for (int i = 0; i < _List.Count; i++)
        {
            __Result[i] = _List[i];
        }
        return __Result;
    }

    public static List<TTo> ConvertTo<TFrom, TTo>(this List<TFrom> _List, Action<TFrom, TTo> _Action)
    {
        List<TTo> __Result = new List<TTo>();
        for (int i = 0; i < _List.Count; i++)
        {
            TTo __Item = (TTo)typeof(TTo).CreateInstance();
            _Action(_List[i], __Item);
            __Result.Add(__Item);
        }
        return __Result;
    }

    public static void RemoveAll<T>(this List<T> _List)
    {
        _List.RemoveRange(0, _List.Count);
    }

    public static List<T> CloneOnlyList<T>(this List<T> _ListToClone)
    {
        List<T> __Result = new List<T>();
        _ListToClone.ForEach((_Item) =>
        {
            __Result.Add(_Item);
        });
        return __Result;
    }
    /// <summary>
    /// Breaks the list into groups with each group containing no more than the specified group size
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_Values">The values.</param>
    /// <param name="_GroupSize">Size of the group.</param>
    /// <returns></returns>
    public static List<List<T>> SplitList<T>(this IEnumerable<T> _Values, int _GroupSize, int? _MaxCount = null)
    {
        if (_GroupSize == 0)
        {
            _GroupSize = 1;
        }
        List<List<T>> result = new List<List<T>>();
        // Quick and special scenario
        if (_Values.Count() <= _GroupSize)
        {
            result.Add(_Values.ToList());
        }
        else
        {
            List<T> __ValueList = _Values.ToList();
            int __StartIndex = 0;
            int __Count = __ValueList.Count;
            int __ElementCount = 0;

            while (__StartIndex < __Count && (!_MaxCount.HasValue || (_MaxCount.HasValue && __StartIndex < _MaxCount)))
            {
                __ElementCount = (__StartIndex + _GroupSize > __Count) ? __Count - __StartIndex : _GroupSize;
                result.Add(__ValueList.GetRange(__StartIndex, __ElementCount));
                __StartIndex += __ElementCount;
            }
        }


        return result;
    }
}

