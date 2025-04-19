using System.Collections.Generic;

public interface IMiniGameSettings
{
    bool RandomOrder { get; }
    bool CanRepeatPrevious { get; }
    IReadOnlyList<MiniGameType> ActiveMiniGames { get; }
}