using System;

public delegate void TimerChangeHandler (
    float currentTimer,
    float maxTimer
);

public interface IMiniGameTimerModel : IDisposable
{
    event Action OnTimerStarted;
    event Action OnTimerEnded;
    event TimerChangeHandler OnTimerChanged;
    
    void Initialize ();
    void ForceExpire (bool skipEndDelay);
}