using System;

public delegate void MiniGameEndedHandler (
    bool hasCompleted,
    bool isDuringGameRun
);

public interface IMiniGameManagerModel : IDisposable
{
    event MiniGameEndedHandler OnMiniGameEnded;
    event Action OnMiniGameChanged;
    event Action OnSingleMiniGameChange;
    
    IMiniGameModel ActiveMiniGame { get; }
    MiniGameType ActiveMiniGameType { get; }
    
    void Initialize ();
    void LateInitialize ();
    void ForceCompleteMiniGame ();
    void ForceFailMiniGame ();
}