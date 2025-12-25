using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class MiniGameSystemSettings : IMiniGameSystemSettings
{
    [JsonProperty]
    public IMiniGameTimingSettings TimingSettings { get; }
    
    [JsonProperty]
    public IMiniGameDifficultySettings DifficultySettings { get; }

    [JsonProperty]
    public bool RandomOrder { get; }
    
    [JsonProperty]
    public bool CanRepeatPrevious { get; }
    
    [JsonProperty]
    public bool MiniGameSkillPoolActive { get; }
    
    [JsonProperty]
    public IMiniGamePoolSettings PoolSettings { get; }

    [JsonConstructor]
    public MiniGameSystemSettings (
        MiniGameTimingSettings timingSettings,
        MiniGameDifficultySettings difficultySettings,
        bool randomOrder,
        bool canRepeatPrevious,
        bool miniGameSkillPoolActive,
        MiniGamePoolSettings poolSettings
    )
    {
        TimingSettings = timingSettings;
        DifficultySettings = difficultySettings;
        RandomOrder = randomOrder;
        CanRepeatPrevious = canRepeatPrevious;
        MiniGameSkillPoolActive = miniGameSkillPoolActive;
        PoolSettings = poolSettings;
    }
}