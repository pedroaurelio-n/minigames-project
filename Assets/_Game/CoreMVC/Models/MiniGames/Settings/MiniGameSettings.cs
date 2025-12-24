using System.Collections.Generic;
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
    public IReadOnlyList<IMiniGameLevelSettings> LevelSettings { get; }

    [JsonConstructor]
    public MiniGameSettings (
        string name,
        string stringId,
        bool hasCustomScene,
        string instructions,
        MiniGameLevelSettings[] levelSettings
    )
    {
        Name = name;
        StringId = stringId;
        HasCustomScene = hasCustomScene;
        Instructions = instructions;
        LevelSettings = levelSettings;
    }
}