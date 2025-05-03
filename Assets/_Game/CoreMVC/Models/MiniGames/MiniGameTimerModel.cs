using System;
using System.Collections;
using UnityEngine;

public class MiniGameTimerModel : IMiniGameTimerModel
{
    public event Action OnTimerStarted;
    public event Action OnTimerEnded;
    public event Action<float, float> OnTimerChanged;

    readonly IMiniGameSystemSettings _settings;
    readonly UniqueCoroutine _timerCoroutine;
    readonly WaitForSeconds _waitForEnd;

    float _timer;
    bool _skipEndDelay;

    public MiniGameTimerModel (
        IMiniGameSystemSettings settings,
        ICoroutineRunner coroutineRunner
    )
    {
        _settings = settings;
        _timerCoroutine = new UniqueCoroutine(coroutineRunner);
        _waitForEnd = new WaitForSeconds(_settings.EndGraceDuration);
    }
    
    public void Initialize ()
    {
        _timerCoroutine.Start(TimerCoroutine());
        OnTimerStarted?.Invoke();
    }

    public void ForceExpire (bool skipEndDelay)
    {
        _skipEndDelay = skipEndDelay;
        _timer = 0;
    }

    IEnumerator TimerCoroutine ()
    {
        _timer = _settings.BaseDuration;

        while (_timer > 0f)
        {
            _timer -= Time.deltaTime;
            OnTimerChanged?.Invoke(_timer, _settings.BaseDuration);
            yield return null;
        }
        
        if (!_skipEndDelay)
            yield return _waitForEnd;
        
        OnTimerEnded?.Invoke();
    }

    public void Dispose ()
    {
        _timerCoroutine.Dispose();
    }
}