namespace Ester.FarmetTracker.Common.Extensions;

public static class DateTimeExtensions
{
    public static DateTime StartOfDay(this DateTime date)
    {
        return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
    }

    public static DateTime EndOfDay(this DateTime date)
    {
        return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
    }

    public static string ToSqlFilter(this DateTime date)
    {
        return date.ToString("yyyy-MM-ddTHH:mm:sszzz");
    }


}
