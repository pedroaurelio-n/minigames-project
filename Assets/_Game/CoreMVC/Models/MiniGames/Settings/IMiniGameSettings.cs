using System.Collections.Generic;

public interface IMiniGameSettings
{
    bool RandomOrder { get; }
    IReadOnlyList<MiniGameType> ActiveMiniGames { get; }
}