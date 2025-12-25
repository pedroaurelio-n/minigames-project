using System.Collections.Generic;

public interface IMiniGameSelectorModel
{
    List<MiniGameType> ActiveMiniGames { get; }
    
    void Initialize ();
}