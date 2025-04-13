using System;

public abstract class BaseMiniGameController : IDisposable
{
    protected abstract MiniGameType MiniGameType { get; }
    
    protected bool Initialized { get; private set; }

    IMiniGameModel ActiveMiniGame => _miniGameManagerModel.ActiveMiniGame;

    readonly IMiniGameManagerModel _miniGameManagerModel;
    readonly SceneView _sceneView;

    public BaseMiniGameController (
        IMiniGameManagerModel miniGameManagerModel,
        SceneView sceneView
    )
    {
        _miniGameManagerModel = miniGameManagerModel;
        _sceneView = sceneView;
    }

    public virtual void Initialize ()
    {
        _sceneView.SetActiveInputs(ActiveMiniGame.InputTypes);
        SetupMiniGame();
        AddListeners();
    }

    protected abstract bool CheckWinCondition (bool timerEnded);

    protected virtual void SetupMiniGame ()
    {
        Initialized = true;
    }

    protected virtual void AddListeners ()
    {
        if (!Initialized)
            return;
        ActiveMiniGame.OnMiniGameStarted += HandleMiniGameStarted;
        ActiveMiniGame.OnMiniGameEnded += HandleMiniGameEnded;
    }

    protected virtual void RemoveListeners ()
    {
        if (!Initialized)
            return;
        ActiveMiniGame.OnMiniGameStarted -= HandleMiniGameStarted;
        ActiveMiniGame.OnMiniGameEnded -= HandleMiniGameEnded;
    }

    void HandleMiniGameStarted ()
    {
        DebugUtils.Log(ActiveMiniGame.Instructions);
    }

    void HandleMiniGameEnded (bool hasCompleted)
    {
        if (!hasCompleted)
            CheckWinCondition(true);
    }
    
    public virtual void Dispose ()
    {
        RemoveListeners();
    }
}