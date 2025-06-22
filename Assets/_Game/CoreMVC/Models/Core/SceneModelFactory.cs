using VContainer;
using VContainer.Unity;

public static class SceneModelFactory
{
    public static ISceneModel CreateScope (
        out LifetimeScope modelScope,
        GameSessionData gameSessionData,
        IPersistenceModel persistenceModel,
        LifetimeScope parentScope,
        ILoadingManager loadingManager,
        IPlayerInfoModel playerInfoModel,
        IGameSessionInfoProvider gameSessionInfoProvider,
        SettingsManager settingsManager,
        IRandomProvider randomProvider,
        IPhysicsProvider physicsProvider,
        ICameraProvider cameraProvider,
        IDateTimeProvider dateTimeProvider,
        ICoroutineRunner coroutineRunner
    )
    {
        SceneModelInstaller installer = new(
            gameSessionData,
            persistenceModel,
            loadingManager,
            playerInfoModel,
            gameSessionInfoProvider,
            settingsManager,
            randomProvider,
            physicsProvider,
            cameraProvider,
            dateTimeProvider,
            coroutineRunner
        );
        modelScope = parentScope.CreateChild(installer, "ModelScope");

        ISceneModel sceneModel = modelScope.Container.Resolve<ISceneModel>();
        return sceneModel;
    }
}