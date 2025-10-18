public abstract class BaseSceneChangerModel : ISceneChangerModel
{
    readonly ILoadingManager _loadingManager;
    IGameSessionInfoProvider _gameSessionInfoProvider;

    public BaseSceneChangerModel (
        ILoadingManager loadingManager,
        IGameSessionInfoProvider gameSessionInfoProvider
    )
    {
        _loadingManager = loadingManager;
        _gameSessionInfoProvider = gameSessionInfoProvider;
    }

    public void ReloadGame ()
    {
        _loadingManager.ReloadFromStart();
    }
    
    protected void ChangeScene (string newScene, string sceneViewName = null)
    {
        _gameSessionInfoProvider.CurrentSceneViewName = sceneViewName ?? newScene;
        _loadingManager.LoadNewScene(newScene);
    }
}