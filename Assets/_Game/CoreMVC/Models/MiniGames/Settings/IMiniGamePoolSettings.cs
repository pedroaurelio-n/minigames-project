using System.Collections.Generic;

public interface IMiniGamePoolSettings
{
    IReadOnlyList<IMiniGameSkillTierSettings> SkillTierSettings { get; }
    IReadOnlyList<IMiniGamePoolProbabilitySettings> ProbabilitySettings { get; }
}