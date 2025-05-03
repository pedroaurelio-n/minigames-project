using System.Collections.Generic;

public interface IMiniGameSystemSettings
{
    float BaseDuration { get; }
    float EndGraceDuration { get; }
    bool RandomOrder { get; }
    bool CanRepeatPrevious { get; }
    IReadOnlyList<MiniGameType> ActiveMiniGames { get; }
}