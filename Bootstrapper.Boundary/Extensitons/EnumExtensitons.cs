using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public static class EnumExtensitons
{
    public static TEnum ConvertEnum<TEnum>(this Enum source)
    {
        return (TEnum)Enum.Parse(typeof(TEnum), source.ToString(), true);
    }
}


