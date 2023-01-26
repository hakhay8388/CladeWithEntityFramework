using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

public static class StringExtensitions
{
    public static string FormatEx(this string value, params object[] _Args)
    {
        if (!value.IsNullOrEmpty() && _Args.Length > 0)
        {
            return string.Format(value, _Args);
        }
        return value;
    }
    public static String ConvertToEnglishCharacter(this string _Word)
    {
        Encoding __Iso = Encoding.GetEncoding("iso-8859-1");
        Encoding __Utf8 = Encoding.UTF8;
        byte[] __UtfBytes = __Utf8.GetBytes(_Word);
        byte[] __IsoBytes = Encoding.Convert(__Utf8, __Iso, __UtfBytes);
        string __Result = __Iso.GetString(__IsoBytes);
        return __Result;
    }
    public static String LowerConvertToEnglishCharacter(this string _Word)
    {
        return _Word.ToLower().ConvertToEnglishCharacter();
    }
    public static bool IsNullOrEmpty(this string value)
    {
        return string.IsNullOrEmpty(value);
    }

    public static string ToString_NoThrowIfNull(this string value)
    {
        return value != null ? value : "";
    }
    public static string Mid(this string value, int startIndex)
    {
        if (value == null)
            return null;

        return value.Mid(startIndex, value.Length);
    }
    public static string Mid(this string value, int startIndex, int length)
    {
        if (value == null)
            return null;

        if (startIndex >= 0 && startIndex < value.Length)
        {
            if ((startIndex + length - 1) < value.Length)
                return value.Substring(startIndex, length);
            else
                return value.Substring(startIndex, value.Length - startIndex);
        }
        else
            return null;
    }
    public static string Left(this string _Value, int _Length)
    {
        if (_Value == null)
            return null;

        if (_Value.Length > _Length)
            return _Value.Substring(0, _Length);
        else
            return _Value;
    }
    public static string Right(this string _Value, int _Length)
    {
        if (_Value == null)
            return null;

        if (_Value.Length > _Length)
            return _Value.Substring(_Value.Length - _Length, _Length);
        else
            return _Value;
    }
    public static string ToMd5(this string _Input)
    { 
        using (System.Security.Cryptography.MD5 __Md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] __InputBytes = System.Text.Encoding.ASCII.GetBytes(_Input);
            byte[] __HashBytes = __Md5.ComputeHash(__InputBytes); 
            
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < __HashBytes.Length; i++)
            {
                sb.Append(__HashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
    public static long ToLong(this string _Value)
    {
        long __longValue = 0;
        long.TryParse(_Value, out __longValue);
        return __longValue;
    }
    public static int ToInt(this string _Value)
    {
        int _IntValue = 0;
        int.TryParse(_Value, out _IntValue);
        return _IntValue;
    }
    public static double ToDouble(this string _Value)
    {
        double __DoubleValue = 0.0;
        double.TryParse(_Value, out __DoubleValue);
        return __DoubleValue;
    }
    public static decimal ToDecimal(this string __Value)
    {
        if (string.IsNullOrEmpty(__Value))
        {
            __Value = "0";
        }
        __Value = __Value.RemoveOneMoreThan(',');
        return Convert.ToDecimal(__Value);
    }
    public static string Remove(this string _Source, string _Remove, int _FirstN)
    {
        if (_FirstN <= 0 || string.IsNullOrEmpty(_Source) || string.IsNullOrEmpty(_Remove))
        {
            return _Source;
        }
        int index = _Source.IndexOf(_Remove);
        return index < 0 ? _Source : _Source.Remove(index, _Remove.Length).Remove(_Remove, --_FirstN);
    }
    public static string RemoveOneMoreThan(this string _Source, char _Str)
    {
        if (_Source.Split(_Str).Count() > 1)
        {
            return Remove(_Source, _Str.ToString(), _Source.Split(_Str).Count() - 2);
        }
        else
        {
            return _Source;
        }
    }
    public static bool IsTrue(this string _Value)
    {
        return (_Value == "true" || _Value == "1" || _Value == "yes");
    }
    public static bool IsFalse(this string _Value)
    {
        return (_Value == "false" || _Value == "0" || _Value == "no");
    }
    public static T ToEnum<T>(this string value, T defaultValue) where T : struct, IConvertible
    {
        T enumValue = default(T);

        if (Enum.TryParse(value, true, out enumValue))
            return enumValue;

        return defaultValue;
    }
    public static T ToEnum<T>(this string _Value) where T : struct, IConvertible
    {
        T __enumValue = default(T);

        if (Enum.TryParse(_Value, true, out __enumValue))
            return __enumValue;

        //throw new CoreException(RS.Message.E1037_InvalidEnumValue, typeof(T).Name, value);
        throw new Exception("aa");
    }
    public static DateTime ToDateTimeFromISO(this string _Value)
    {
        // value: iso8601
        DateTime __dtValue = default(DateTime);
        DateTime.TryParse(_Value, out __dtValue);
        return __dtValue;
    }
    public static string CaseInsenstiveReplace(this string _originalString, string _oldValue, string _newValue)
    {
        Regex __RegEx = new Regex(_oldValue, RegexOptions.IgnoreCase | RegexOptions.Multiline);
        return __RegEx.Replace(_originalString, _newValue);
    }
    public static string WildcardToRegex(this string _Value)
    {
        string __Result = Regex.Escape(_Value).
            Replace(@"\*", ".+?").
            Replace(@"\?", ".");

        if (__Result.EndsWith(".+?"))
        {
            __Result = __Result.Remove(__Result.Length - 3, 3);
            __Result += ".*";
        }

        return __Result;
    }
    public static string ToNormalizedFileName(this string _Value)
    {
        if (_Value == null)
            return null;

        string ret = String.Copy(_Value);
        char[] invalidChars = Path.GetInvalidFileNameChars();

        for (int i = 0; i < invalidChars.Length; i++)
            ret = ret.Replace(invalidChars[i], '_');

        return ret;
    }
    public static bool IsNumeric(this string _Value)
    {
        if (string.IsNullOrEmpty(_Value))
        {
            return false;
        }
        foreach (char c in _Value)
            if (!((Int16)c > 42 && (Int16)c < 58)) return false;
        return true;
    }
    public static string ClearNonNumeric(this string _Text)
    {
        string __newString = Regex.Replace(_Text, "[^.0-9]", "");
        return __newString;
    }

}

