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
        IInputRaycastProvider inputRaycastProvider,
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
            inputRaycastProvider,
            coroutineRunner
        );
        modelScope = parentScope.CreateChild(installer, "ModelScope");

        ResolveUnorderedDependencies(modelScope.Container);

        ISceneModel sceneModel = modelScope.Container.Resolve<ISceneModel>();
        return sceneModel;
    }

    static void ResolveUnorderedDependencies (IObjectResolver container)
    {
        IMiniGameDifficultyModel miniGameDifficultyModel = container.Resolve<IMiniGameDifficultyModel>();
        IMiniGameManagerModel miniGameManagerModel = container.Resolve<IMiniGameManagerModel>();
        
        miniGameDifficultyModel.UpdateDependencies(miniGameManagerModel);
    }
}