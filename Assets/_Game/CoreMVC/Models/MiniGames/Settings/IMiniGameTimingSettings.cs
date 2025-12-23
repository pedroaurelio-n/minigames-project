public interface IMiniGameTimingSettings
{
    float BaseDuration { get; }
    float MinTimerDuration { get; }
    float EndGraceDuration { get; }
    float NextWaitDuration { get; }
}