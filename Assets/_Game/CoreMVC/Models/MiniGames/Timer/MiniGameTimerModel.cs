using System;
using System.Collections;
using UnityEngine;

public class MiniGameTimerModel : IMiniGameTimerModel
{
    public event Action OnTimerEnded;

    readonly MiniGameOptions _miniGameOptions;
    readonly UniqueCoroutine _timerCoroutine;

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

    IEnumerator TimerCoroutine ()
    {
        float timer = _miniGameOptions.BaseMiniGameDuration;

        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        
        OnTimerEnded?.Invoke();
    }

    public void Dispose ()
    {
        _timerCoroutine.Dispose();
    }
}