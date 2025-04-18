using System;

public interface IMiniGameManagerModel : IDisposable
{
    event Action OnMiniGameChange;
    
    IMiniGameModel ActiveMiniGame { get; }
    MiniGameType ActiveMiniGameType { get; }
    
    void Initialize ();
    void LateInitialize ();
}