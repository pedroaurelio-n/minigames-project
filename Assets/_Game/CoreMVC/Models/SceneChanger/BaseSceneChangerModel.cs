public abstract class BaseSceneChangerModel : ISceneChangerModel
{
    readonly ILoadingManager _loadingManager;

    public BaseSceneChangerModel (ILoadingManager loadingManager)
    {
        _loadingManager = loadingManager;
    }

    public void ReloadGame ()
    {
        _loadingManager.ReloadFromStart();
    }
    
    protected void ChangeScene (string newScene)
    {
        _loadingManager.LoadNewScene(newScene);
    }
}