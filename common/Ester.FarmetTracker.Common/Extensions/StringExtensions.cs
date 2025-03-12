namespace Ester.FarmetTracker.Common.Extensions;

public static class StringExtensions
{
    public static TimeSpan ToTimeSpan(this string str)
    {
        return TimeSpan.Parse(str);
    }
}
