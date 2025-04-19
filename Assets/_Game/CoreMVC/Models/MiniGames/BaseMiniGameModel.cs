using System;

public abstract class BaseMiniGameModel : IMiniGameModel
{
    public event Action OnMiniGameStarted;
    public event Action<bool> OnMiniGameEnded;
    public event Action OnMiniGameTimerEnded;
    
    public abstract MiniGameType Type { get; }
    public abstract TouchInputType InputTypes { get; }
    public abstract string Instructions { get; }
    
    public bool HasCompleted { get; private set; }

    readonly IMiniGameTimerModel _miniGameTimerModel;

    bool _forceFailed;

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
        HasCompleted = true;
        _miniGameTimerModel.ForceExpire(HasCompleted);
    }

    public void ForceFailure ()
    {
        _forceFailed = true;
        _miniGameTimerModel.ForceExpire(_forceFailed);
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
        OnMiniGameTimerEnded?.Invoke();
        OnMiniGameEnded?.Invoke(HasCompleted);
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}