using System;

public interface IMiniGameModel : IDisposable
{
    event Action OnMiniGameStarted;
    event Action<bool> OnMiniGameEnded;
    event Action OnMiniGameTimerEnded;
    
    MiniGameType Type { get; }
    TouchInputType InputTypes { get; }
    //TODO pedro: implement in a smarter way
    string Instructions { get; }
    bool HasCompleted { get; }

    void Initialize ();
    void LateInitialize ();
    void Complete ();
    void ForceFailure ();
}