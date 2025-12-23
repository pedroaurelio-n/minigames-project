using System;

public abstract class BaseMiniGameModel : IMiniGameModel
{
    public event Action OnMiniGameStarted;
    public event Action<bool> OnMiniGameEnded;
    public event Action OnMiniGameTimerEnded;
    
    public abstract MiniGameType Type { get; }
    public abstract TouchInputType InputTypes { get; }
    
    public string StringId => _Settings.StringId;
    public string Instructions => _Settings.Instructions;
    public bool HasCompleted { get; private set; }
    public bool IsActive { get; private set; }

    protected float CurrentDifficulty => _miniGameDifficultyModel.CurrentDifficultyLevel;

    protected readonly IMiniGameSettings _Settings;
    
    readonly IMiniGameDifficultyModel _miniGameDifficultyModel;
    readonly IMiniGameTimerModel _miniGameTimerModel;

    bool _forceFailed;

    public BaseMiniGameModel (
        IMiniGameSettings settings,
        IMiniGameDifficultyModel miniGameDifficultyModel,
        IMiniGameTimerModel miniGameTimerModel
    )
    {
        _Settings = settings;
        _miniGameDifficultyModel = miniGameDifficultyModel;
        _miniGameTimerModel = miniGameTimerModel;
    }

    public void Initialize ()
    {
        AddListeners();
    }

    public void LateInitialize ()
    {
        IsActive = true;
        OnMiniGameStarted?.Invoke();
    }

    public void Complete ()
    {
        if (!IsActive)
            return;
        
        HasCompleted = true;
        _miniGameTimerModel.ForceExpire(HasCompleted);
    }

    public void ForceFailure ()
    {
        if (!IsActive)
            return;
        
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
        IsActive = false;
        OnMiniGameTimerEnded?.Invoke();
        OnMiniGameEnded?.Invoke(HasCompleted);
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}