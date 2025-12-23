using System.Collections.Generic;

public interface IMiniGameSystemSettings
{
    IMiniGameTimingSettings TimingSettings { get; }
    IMiniGameDifficultySettings DifficultySettings { get; }
    bool RandomOrder { get; }
    bool CanRepeatPrevious { get; }
    IReadOnlyList<MiniGameType> ActiveMiniGames { get; }
}