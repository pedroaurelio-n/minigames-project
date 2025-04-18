using System;

public interface IMiniGameTimerModel : IDisposable
{
    event Action OnTimerStarted;
    event Action<bool> OnTimerEnded;
    event Action<float, float> OnTimerChanged;
    
    void Initialize ();
    void ForceComplete ();
}