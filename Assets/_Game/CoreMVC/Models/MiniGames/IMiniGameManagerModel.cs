using System;

public interface IMiniGameManagerModel : IDisposable
{
    event Action OnMiniGameChanged;
    event Action OnSingleMiniGameEnded;
    
    IMiniGameModel ActiveMiniGame { get; }
    MiniGameType ActiveMiniGameType { get; }
    
    void Initialize ();
    void LateInitialize ();
}