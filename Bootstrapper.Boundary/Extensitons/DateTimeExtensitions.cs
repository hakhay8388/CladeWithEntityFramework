using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class DateTimeExtensitions
{
    public static bool IsEmpty(this DateTime date)
    {
        return date <= new DateTime(1900, 1, 1, 0, 0, 0) || date >= DateTime.MaxValue;
    }
    public static DateTime Round000000(this DateTime date)
    {
        return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
    }
    public static DateTime Round235959(this DateTime date)
    {
        return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
    }
    public static DateTime Round0000(this DateTime date)
    {
        return new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0);
    }
    public static DateTime Round00(this DateTime date)
    {
        return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0);
    }
    public static DateTime Round59(this DateTime date)
    {
        return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 59);
    }
    public static string ToISO(this DateTime date)
    {
        return date.ToString("s");
    }
    public static bool Between(this DateTime value, DateTime start, DateTime end)
    {
        return (value >= start && value <= end);
    }
     
    public static int DatetimeToUnixTimestamp(this DateTime value)
    {
        return (int)Math.Truncate((value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
    }
    public static long DatetimeToUnixTimestampLong(this DateTime value)
    {
        return Convert.ToInt64(DatetimeToUnixTimestamp(value));
    }
    public static DateTime UnixTimeStampToDateTime(this double unixTimeStamp)
    {
        DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        return dtDateTime;
    }
}

