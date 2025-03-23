using System;

public interface IMiniGameTimerModel : IDisposable
{
    event Action OnTimerEnded;
    
    void Initialize ();
    void ForceComplete ();
}