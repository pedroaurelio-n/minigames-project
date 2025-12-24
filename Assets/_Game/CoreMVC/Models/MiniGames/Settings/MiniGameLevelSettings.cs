using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class MiniGameLevelSettings : IMiniGameLevelSettings
{
    [JsonProperty]
    public int Level { get; }
    
    [JsonProperty]
    public int? ObjectCount { get; }
    
    [JsonProperty]
    public int? MilestoneCount { get; }
    
    [JsonProperty]
    public float? TimerModifier { get; }
    
    [JsonProperty]
    public float? SpeedModifier { get; }
    
    [JsonProperty]
    public float? RateModifier { get; }
    
    [JsonConstructor]
    public MiniGameLevelSettings (
        int level,
        int? objectCount,
        int? milestoneCount,
        float? timerModifier,
        float? speedModifier,
        float? rateModifier
    )
    {
        Level = level;
        ObjectCount = objectCount;
        MilestoneCount = milestoneCount;
        TimerModifier = timerModifier;
        SpeedModifier = speedModifier;
        RateModifier = rateModifier;
    }
}