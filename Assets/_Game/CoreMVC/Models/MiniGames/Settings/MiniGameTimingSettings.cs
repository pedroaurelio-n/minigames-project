using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class MiniGameTimingSettings : IMiniGameTimingSettings
{
    [JsonProperty]
    public float BaseDuration { get; }
    
    [JsonProperty]
    public float MinTimerDuration { get; }
    
    [JsonProperty]
    public float EndGraceDuration { get; }
    
    [JsonProperty]
    public float NextWaitDuration { get; }

    [JsonConstructor]
    public MiniGameTimingSettings (
        float baseDuration,
        float minTimerDuration,
        float endGraceDuration,
        float nextWaitDuration
    )
    {
        BaseDuration = baseDuration;
        MinTimerDuration = minTimerDuration;
        EndGraceDuration = endGraceDuration;
        NextWaitDuration = nextWaitDuration;
    }
}