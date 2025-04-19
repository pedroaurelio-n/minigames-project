using System;

public abstract class BaseMiniGameController : IDisposable
{
    protected abstract MiniGameType MiniGameType { get; }
    
    protected BaseMiniGameUIController UIController { get; set; }
    
    protected bool Initialized { get; private set; }

    IMiniGameModel ActiveMiniGame => _miniGameManagerModel.ActiveMiniGame;

    readonly IMiniGameManagerModel _miniGameManagerModel;
    readonly SceneView _sceneView;
    readonly SceneUIView _sceneUIView;

    public BaseMiniGameController (
        IMiniGameManagerModel miniGameManagerModel,
        SceneView sceneView,
        SceneUIView sceneUIView
    )
    {
        _miniGameManagerModel = miniGameManagerModel;
        _sceneView = sceneView;
        _sceneUIView = sceneUIView;
    }

    public virtual void Initialize ()
    {
        _sceneView.SetActiveInputs(ActiveMiniGame.InputTypes);
        SetupMiniGame();
        AddListeners();
    }
    
    protected virtual void SetupMiniGame ()
    {
        Initialized = true;

        if (UIController != null)
            UIController.Setup(_sceneUIView);
    }

    protected virtual bool CheckWinCondition (bool timerEnded)
    {
        _miniGameManagerModel.ActiveMiniGame.Complete();
        return true;
    }

    protected virtual bool CheckFailCondition ()
    {
        return false;
    }

    protected virtual void AddListeners ()
    {
        if (!Initialized)
            return;
        ActiveMiniGame.OnMiniGameStarted += HandleMiniGameStarted;
        ActiveMiniGame.OnMiniGameTimerEnded += HandleMiniGameTimerEnded;
    }

    protected virtual void RemoveListeners ()
    {
        if (!Initialized)
            return;
        ActiveMiniGame.OnMiniGameStarted -= HandleMiniGameStarted;
        ActiveMiniGame.OnMiniGameTimerEnded -= HandleMiniGameTimerEnded;
    }

    void HandleMiniGameStarted ()
    {
    }

    void HandleMiniGameTimerEnded ()
    {
        if (CheckFailCondition())
            return;
        
        if (!_miniGameManagerModel.ActiveMiniGame.HasCompleted)
            CheckWinCondition(true);
    }
    
    public virtual void Dispose ()
    {
        UIController?.Dispose();
        RemoveListeners();
    }
}