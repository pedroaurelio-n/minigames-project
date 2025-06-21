using System;

public interface IDateTimeProvider
{
    DateTime Now { get; }
    DateTime UtcNow { get; }
    DateTime Today { get; }
    long UnixTimeSeconds { get; }
    long UnixTimeMilliseconds { get; }
    
    DateTime FromUnixTimeSeconds(long seconds);
    DateTime FromUnixTimeMilliseconds(long milliseconds);
}