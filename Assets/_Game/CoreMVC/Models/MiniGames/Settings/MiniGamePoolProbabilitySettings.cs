using System.Collections.Generic;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class MiniGamePoolProbabilitySettings : IMiniGamePoolProbabilitySettings
{
    [JsonProperty]
    public int DifficultyLevel { get; set; }
    
    [JsonProperty]
    public IReadOnlyList<IMiniGamePoolChanceSettings> Chances { get; set; }

    public MiniGamePoolProbabilitySettings (
        int difficultyLevel,
        MiniGamePoolChanceSettings[] chances
    )
    {
        DifficultyLevel = difficultyLevel;
        Chances = chances;
    }
}