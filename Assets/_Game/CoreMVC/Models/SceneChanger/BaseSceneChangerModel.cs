public abstract class BaseSceneChangerModel : ISceneChangerModel
{
    readonly ILoadingManager _loadingManager;

    public BaseSceneChangerModel (ILoadingManager loadingManager)
    {
        _loadingManager = loadingManager;
    }
    
    protected void ChangeScene (string newScene)
    {
        _loadingManager.LoadNewScene(newScene);
    }
}