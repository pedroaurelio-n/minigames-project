using System.Collections.Generic;

public interface IMiniGameSystemSettings
{
    bool RandomOrder { get; }
    bool CanRepeatPrevious { get; }
    IReadOnlyList<MiniGameType> ActiveMiniGames { get; }
}