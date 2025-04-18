using System;
using System.Collections;
using UnityEngine;

public class MiniGameTimerModel : IMiniGameTimerModel
{
    const float END_DELAY = 0.5f;
    
    public event Action OnTimerStarted;
    public event Action<bool> OnTimerEnded;
    public event Action<float, float> OnTimerChanged;

    readonly MiniGameOptions _miniGameOptions;
    readonly UniqueCoroutine _timerCoroutine;
    readonly WaitForSeconds _waitForEnd;

    float _timer;
    bool _hasCompleted;

    public MiniGameTimerModel (
        MiniGameOptions miniGameOptions,
        ICoroutineRunner coroutineRunner
    )
    {
        _miniGameOptions = miniGameOptions;
        _timerCoroutine = new UniqueCoroutine(coroutineRunner);
        _waitForEnd = new WaitForSeconds(END_DELAY);
    }
    
    public void Initialize ()
    {
        _timerCoroutine.Start(TimerCoroutine());
        OnTimerStarted?.Invoke();
    }

    public void ForceComplete ()
    {
        _hasCompleted = true;
        _timer = 0;
    }

    IEnumerator TimerCoroutine ()
    {
        _timer = _miniGameOptions.BaseMiniGameDuration;

        while (_timer > 0f)
        {
            _timer -= Time.deltaTime;
            OnTimerChanged?.Invoke(_timer, _miniGameOptions.BaseMiniGameDuration);
            yield return null;
        }
        
        if (!_hasCompleted)
            yield return _waitForEnd;
        
        OnTimerEnded?.Invoke(_hasCompleted);
    }

    public void Dispose ()
    {
        _timerCoroutine.Dispose();
    }
}