using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class MiniGamePoolChanceSettings : IMiniGamePoolChanceSettings
{
    [JsonProperty]
    public MiniGameSkillTier Tier { get; }
    
    [JsonProperty]
    public float Chance { get; }

    public MiniGamePoolChanceSettings (
        MiniGameSkillTier tier,
        float chance
    )
    {
        Tier = tier;
        Chance = chance;
    }
}