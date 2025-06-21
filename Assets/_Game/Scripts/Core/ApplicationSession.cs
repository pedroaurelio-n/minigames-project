using System;
using UnityEngine;

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
        GameVersion gameVersion = GameVersion.Parse(Application.version);

        GameSession = new GameSession(
            gameVersion,
            _loadingManager,
            currentScene
        );
        
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
    
    //TODO pedro: maybe flush save on application quit or game session dispose?
}
