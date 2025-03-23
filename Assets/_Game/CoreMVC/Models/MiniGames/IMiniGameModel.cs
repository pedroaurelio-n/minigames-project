using System;

public interface IMiniGameModel : IDisposable
{
    event Action OnMiniGameStarted;
    event Action OnMiniGameEnded;
    
    MiniGameType Type { get; }
    TouchInputType InputTypes { get; }
    //TODO pedro: implement in a smarter way
    string Instructions { get; }

    void Initialize ();
    void LateInitialize ();
    void Complete ();
}