using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class MiniGameSettings : IMiniGameSettings
{
    [JsonProperty]
    public int? BaseObjectCount { get; }
    
    [JsonProperty]
    public int? BaseObjectiveMilestone { get; }
    
    [JsonProperty]
    public string Instructions { get; }

    [JsonConstructor]
    public MiniGameSettings (
        int? baseObjectCount,
        int? baseObjectiveMilestone,
        string instructions
    )
    {
        BaseObjectCount = baseObjectCount;
        BaseObjectiveMilestone = baseObjectiveMilestone;
        Instructions = instructions;
    }
}