using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class MiniGameSettings : IMiniGameSettings
{
    [JsonProperty]
    public int? BaseObjectCount { get; }
    
    [JsonProperty]
    public int? BaseObjectiveMilestone { get; }

    [JsonConstructor]
    public MiniGameSettings (
        int? baseObjectCount,
        int? baseObjectiveMilestone
    )
    {
        BaseObjectCount = baseObjectCount;
        BaseObjectiveMilestone = baseObjectiveMilestone;
    }
}