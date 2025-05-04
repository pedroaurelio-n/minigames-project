using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class MiniGameSettings : IMiniGameSettings
{
    [JsonProperty]
    public string Name { get; }
    
    [JsonProperty]
    public string Instructions { get; }
    
    [JsonProperty]
    public int? BaseObjectCount { get; }
    
    [JsonProperty]
    public int? BaseObjectiveMilestone { get; }

    [JsonConstructor]
    public MiniGameSettings (
        string name,
        string instructions,
        int? baseObjectCount,
        int? baseObjectiveMilestone
    )
    {
        Name = name;
        Instructions = instructions;
        BaseObjectCount = baseObjectCount;
        BaseObjectiveMilestone = baseObjectiveMilestone;
    }
}