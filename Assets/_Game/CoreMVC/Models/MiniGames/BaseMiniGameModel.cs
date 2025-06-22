using System;

public abstract class BaseMiniGameModel : IMiniGameModel
{
    public event Action OnMiniGameStarted;
    public event Action<bool> OnMiniGameEnded;
    public event Action OnMiniGameTimerEnded;
    
    public abstract MiniGameType Type { get; }
    public abstract TouchInputType InputTypes { get; }
    
    public string StringId => _settings.StringId;
    public string Instructions => _settings.Instructions;
    public bool HasCompleted { get; private set; }

    protected readonly IMiniGameSettings _settings;

    readonly IMiniGameTimerModel _miniGameTimerModel;

    bool _forceFailed;

    public BaseMiniGameModel (
        IMiniGameSettings settings,
        IMiniGameTimerModel miniGameTimerModel
    )
    {
        _settings = settings;
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