using System.Collections.Generic;

public interface IMiniGamePoolProbabilitySettings
{
    int DifficultyLevel { get; }
    IReadOnlyList<IMiniGamePoolChanceSettings> Chances { get; }
}