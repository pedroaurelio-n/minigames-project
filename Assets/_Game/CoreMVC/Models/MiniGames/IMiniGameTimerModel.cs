using System;

public interface IMiniGameTimerModel : IDisposable
{
    event Action OnTimerStarted;
    event Action OnTimerEnded;
    event Action<float, float> OnTimerChanged;
    
    void Initialize ();
    void ForceExpire (bool skipEndDelay);
}