using System.Collections.Generic;

public interface IMiniGameSkillTierSettings
{
    MiniGameSkillTier Tier { get; }
    IReadOnlyList<MiniGameType> ActiveMiniGames { get; }
}