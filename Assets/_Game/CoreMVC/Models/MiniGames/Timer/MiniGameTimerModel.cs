using System;
using System.Collections;
using UnityEngine;

public class MiniGameTimerModel : IMiniGameTimerModel
{
    public event Action OnTimerEnded;

    readonly MiniGameOptions _miniGameOptions;
    readonly UniqueCoroutine _timerCoroutine;

    float _timer;

    public MiniGameTimerModel (
        MiniGameOptions miniGameOptions,
        ICoroutineRunner coroutineRunner
    )
    {
        _miniGameOptions = miniGameOptions;
        _timerCoroutine = new UniqueCoroutine(coroutineRunner);
    }
    
    public void Initialize ()
    {
        _timerCoroutine.Start(TimerCoroutine());
    }

    public void ForceComplete ()
    {
        _timer = 0;
    }

    IEnumerator TimerCoroutine ()
    {
        _timer = _miniGameOptions.BaseMiniGameDuration;

        while (_timer > 0f)
        {
            _timer -= Time.deltaTime;
            yield return null;
        }
        
        OnTimerEnded?.Invoke();
    }

    public void Dispose ()
    {
        _timerCoroutine.Dispose();
    }
}