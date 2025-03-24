using System;

public abstract class BaseMiniGameModel : IMiniGameModel
{
    public event Action OnMiniGameStarted;
    public event Action OnMiniGameEnded;
    
    public abstract MiniGameType Type { get; }
    public abstract TouchInputType InputTypes { get; }
    public abstract string Instructions { get; }

    readonly IMiniGameTimerModel _miniGameTimerModel;

    public BaseMiniGameModel (
        IMiniGameTimerModel miniGameTimerModel
    )
    {
        _miniGameTimerModel = miniGameTimerModel;
    }

    public void Initialize ()
    {
        AddListeners();
    }

    public void LateInitialize ()
    {
        OnMiniGameStarted?.Invoke();
    }

    public void Complete ()
    {
        _miniGameTimerModel.ForceComplete();
    }

    protected virtual void AddListeners ()
    {
        _miniGameTimerModel.OnTimerEnded += HandleTimerEnded;
    }

    protected virtual void RemoveListeners ()
    {
        _miniGameTimerModel.OnTimerEnded += HandleTimerEnded;
    }

    void HandleTimerEnded ()
    {
        OnMiniGameEnded?.Invoke();
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}