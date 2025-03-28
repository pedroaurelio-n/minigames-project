using System;

public interface IMiniGameManagerModel : IDisposable
{
    IMiniGameModel ActiveMiniGame { get; }
    MiniGameType ActiveMiniGameType { get; }
    
    void Initialize ();
    void LateInitialize ();
}