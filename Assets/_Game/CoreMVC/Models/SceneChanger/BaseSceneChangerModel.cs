public abstract class BaseSceneChangerModel : ISceneChangerModel
{
    readonly ILoadingManager _loadingManager;

    public BaseSceneChangerModel (ILoadingManager loadingManager)
    {
        _loadingManager = loadingManager;
    }
    
    public void ChangeScene (string newScene)
    {
        _loadingManager.LoadNewScene(newScene);
    }
}