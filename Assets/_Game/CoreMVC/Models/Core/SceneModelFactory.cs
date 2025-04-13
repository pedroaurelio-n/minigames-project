using VContainer;
using VContainer.Unity;

public static class SceneModelFactory
{
    public static ISceneModel CreateScope (
        out LifetimeScope modelScope,
        LifetimeScope parentScope,
        ILoadingManager loadingManager,
        IPlayerInfoModel playerInfoModel,
        IGameSessionInfoProvider gameSessionInfoProvider,
        SettingsManager settingsManager,
        IRandomProvider randomProvider,
        IPhysicsProvider physicsProvider,
        ICameraProvider cameraProvider,
        ICoroutineRunner coroutineRunner
    )
    {
        SceneModelInstaller installer = new(
            loadingManager,
            playerInfoModel,
            gameSessionInfoProvider,
            settingsManager,
            randomProvider,
            physicsProvider,
            cameraProvider,
            coroutineRunner
        );
        modelScope = parentScope.CreateChild(installer, "ModelScope");

        ISceneModel sceneModel = modelScope.Container.Resolve<ISceneModel>();
        return sceneModel;
    }
}