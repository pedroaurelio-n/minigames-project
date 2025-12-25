using System.Collections.Generic;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class MiniGamePoolSettings : IMiniGamePoolSettings
{
    [JsonProperty]
    public IReadOnlyList<IMiniGameSkillTierSettings> SkillTierSettings { get; }
    
    [JsonProperty]
    public IReadOnlyList<IMiniGamePoolProbabilitySettings> ProbabilitySettings { get; }

    public MiniGamePoolSettings (
        MiniGameSkillTierSettings[] skillTierSettings,
        MiniGamePoolProbabilitySettings[] probabilitySettings
    )
    {
        SkillTierSettings = skillTierSettings;
        ProbabilitySettings = probabilitySettings;
    }
}