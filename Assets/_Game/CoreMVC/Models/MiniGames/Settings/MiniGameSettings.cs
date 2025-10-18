using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class MiniGameSettings : IMiniGameSettings
{
    [JsonProperty]
    public string Name { get; }
    
    [JsonProperty]
    public string StringId { get; }
    
    [JsonProperty]
    public bool HasCustomScene { get; }
    
    [JsonProperty]
    public string Instructions { get; }
    
    [JsonProperty]
    public int? BaseObjectCount { get; }
    
    [JsonProperty]
    public int? BaseObjectiveMilestone { get; }

    [JsonConstructor]
    public MiniGameSettings (
        string name,
        string stringId,
        bool hasCustomScene,
        string instructions,
        int? baseObjectCount,
        int? baseObjectiveMilestone
    )
    {
        Name = name;
        StringId = stringId;
        HasCustomScene = hasCustomScene;
        Instructions = instructions;
        BaseObjectCount = baseObjectCount;
        BaseObjectiveMilestone = baseObjectiveMilestone;
    }
}