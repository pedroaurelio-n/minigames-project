using System;
using System.Collections;
using UnityEngine;

public class MiniGameTimerModel : IMiniGameTimerModel
{
    public event Action OnTimerStarted;
    public event Action OnTimerEnded;
    public event TimerChangeHandler OnTimerChanged;

    readonly IMiniGameSystemSettings _settings;
    readonly IMiniGameDifficultyModel _miniGameDifficultyModel;
    readonly UniqueCoroutine _timerCoroutine;
    readonly WaitForSeconds _waitForEnd;

    float _timer;
    bool _skipEndDelay;

    public MiniGameTimerModel (
        IMiniGameSystemSettings settings,
        IMiniGameDifficultyModel miniGameDifficultyModel,
        ICoroutineRunner coroutineRunner
    )
    {
        _settings = settings;
        _miniGameDifficultyModel = miniGameDifficultyModel;
        _timerCoroutine = new UniqueCoroutine(coroutineRunner);
        _waitForEnd = new WaitForSeconds(_settings.TimingSettings.EndGraceDuration);
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
        float maxTimer = Mathf.Max(
            _settings.TimingSettings.MinTimerDuration,
            _settings.TimingSettings.BaseDuration - _miniGameDifficultyModel.CurrentTimerDecrease
        );
        maxTimer *= _miniGameDifficultyModel.CurrentTimerModifier;
        
        _timer = maxTimer;

        while (_timer > 0f)
        {
            _timer -= Time.deltaTime;
            OnTimerChanged?.Invoke(_timer, maxTimer);
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