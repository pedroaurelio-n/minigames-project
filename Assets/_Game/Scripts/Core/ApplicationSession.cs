using System;

public class ApplicationSession
{
    public event Action OnInitializationComplete;
    
    public GameSession GameSession { get; private set; }

    readonly ILoadingManager _loadingManager;

    public ApplicationSession (ILoadingManager loadingManager)
    {
        _loadingManager = loadingManager;
    }

    public void Initialize (string currentScene)
    {
        GameSession = new GameSession(_loadingManager, currentScene);
        GameSession.OnInitializationComplete += HandleInitializationComplete;
        GameSession.Initialize();
    }
    
    public void ChangeScene (string newScene)
    {
        GameSession.OnInitializationComplete += HandleInitializationComplete;
        GameSession.ChangeScene(newScene);
    }

    public void DisposeCurrentScope()
    {
        GameSession.Dispose();
    }

    void HandleInitializationComplete ()
    {
        GameSession.OnInitializationComplete -= HandleInitializationComplete;
        OnInitializationComplete?.Invoke();
    }
}
