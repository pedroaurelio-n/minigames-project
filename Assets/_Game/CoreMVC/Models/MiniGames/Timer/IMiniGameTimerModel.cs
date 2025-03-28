using System;

public interface IMiniGameTimerModel : IDisposable
{
    event Action<bool> OnTimerEnded;
    
    void Initialize ();
    void ForceComplete ();
}