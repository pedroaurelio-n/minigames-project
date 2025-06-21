using System;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
    public DateTime UtcNow => DateTime.UtcNow;
    public DateTime Today => DateTime.Today;
    public long UnixTimeSeconds => DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    public long UnixTimeMilliseconds => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    
    public DateTime FromUnixTimeSeconds(long seconds) => DateTimeOffset.FromUnixTimeSeconds(seconds).UtcDateTime;

    public DateTime FromUnixTimeMilliseconds (long milliseconds) =>
        DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).UtcDateTime;
}