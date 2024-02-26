namespace MyFinances.Common.Core.Extensions;

public static class DateTimeExtensions
{
    public static bool IsOlderThan(this DateTime dateTime, DateTime otherDateTime)
    {
        return dateTime < otherDateTime;
    }

    public static bool IsOlderThan(this DateTime dateTime, TimeSpan timeSpan)
    {
        return dateTime < DateTime.UtcNow - timeSpan;
    }
}