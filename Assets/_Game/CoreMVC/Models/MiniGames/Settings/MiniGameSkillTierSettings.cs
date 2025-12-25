using System.Collections.Generic;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class MiniGameSkillTierSettings : IMiniGameSkillTierSettings
{
    [JsonProperty]
    public MiniGameSkillTier Tier { get; set; }
    
    [JsonProperty]
    public IReadOnlyList<MiniGameType> ActiveMiniGames { get; set; }

    public MiniGameSkillTierSettings (
        MiniGameSkillTier tier,
        MiniGameType[] activeMiniGames
    )
    {
        Tier = tier;
        ActiveMiniGames = activeMiniGames;
    }
}