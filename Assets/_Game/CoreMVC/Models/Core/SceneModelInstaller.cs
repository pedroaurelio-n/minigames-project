using VContainer;
using VContainer.Unity;

public class SceneModelInstaller : IInstaller
{
    readonly ILoadingManager _loadingManager;
    readonly IPlayerInfoModel _playerInfoModel;
    readonly IGameSessionInfoProvider _gameSessionInfoProvider;
    readonly SettingsManager _settingsManager;
    readonly IRandomProvider _randomProvider;
    readonly IPhysicsProvider _physicsProvider;
    readonly ICameraProvider _cameraProvider;
    readonly ICoroutineRunner _coroutineRunner;
    
    public SceneModelInstaller (
        ILoadingManager loadingManager,
        IPlayerInfoModel playerInfoModel,
        IGameSessionInfoProvider gameSessionInfoProvider,
        SettingsManager settings,
        IRandomProvider randomProvider,
        IPhysicsProvider physicsProvider,
        ICameraProvider cameraProvider,
        ICoroutineRunner coroutineRunner
    )
    {
        _loadingManager = loadingManager;
        _playerInfoModel = playerInfoModel;
        _gameSessionInfoProvider = gameSessionInfoProvider;
        _settingsManager = settings;
        _randomProvider = randomProvider;
        _physicsProvider = physicsProvider;
        _cameraProvider = cameraProvider;
        _coroutineRunner = coroutineRunner;
    }
    
    public void Install (IContainerBuilder builder)
    {
        builder.RegisterInstance(_loadingManager);
        builder.RegisterInstance(_playerInfoModel);
        builder.RegisterInstance(_gameSessionInfoProvider);
        builder.RegisterInstance(_randomProvider);
        builder.RegisterInstance(_physicsProvider);
        builder.RegisterInstance(_cameraProvider);
        builder.RegisterInstance(_coroutineRunner);
        
        builder.RegisterInstance(_settingsManager.MiniGameSettings.Instance);

        builder.RegisterInstance(GameGlobalOptions.Instance.LayerMaskOptions);
        builder.RegisterInstance(GameGlobalOptions.Instance.FadeTransitionOptions);
        builder.RegisterInstance(GameGlobalOptions.Instance.InputOptions.TapInputOptions);
        builder.RegisterInstance(GameGlobalOptions.Instance.InputOptions.SwipeInputOptions);
        builder.RegisterInstance(GameGlobalOptions.Instance.InputOptions.LongPressInputOptions);
        builder.RegisterInstance(GameGlobalOptions.Instance.InputOptions.TwoPointMoveInputOptions);
        builder.RegisterInstance(GameGlobalOptions.Instance.InputOptions.TwoPointZoomInputOptions);
        builder.RegisterInstance(GameGlobalOptions.Instance.MiniGameOptions);

        //TODO pedro: delete mouse input classes
        // builder.Register<IMouseInputModel, MouseInputModel>(Lifetime.Singleton);
        builder.Register<ITouchInputModel, TouchInputModel>(Lifetime.Singleton);
        builder.Register<IDragModel, DragModel>(Lifetime.Singleton);
        builder.Register<IPressModel, PressModel>(Lifetime.Singleton);

        builder.Register<ICameraMoveModel, CameraMoveModel>(Lifetime.Singleton);
        
        builder.Register<IMiniGameSceneChangerModel, MiniGameSceneChangerModel>(Lifetime.Singleton);

        builder.Register<IMiniGameTimerModel, MiniGameTimerModel>(Lifetime.Singleton);
        builder.Register<IMiniGameModelFactory, MiniGameModelFactory>(Lifetime.Singleton);
        builder.Register<IMiniGameManagerModel, MiniGameManagerModel>(Lifetime.Singleton);
        builder.Register<IMiniGameSelectorModel, MiniGameSelectorModel>(Lifetime.Singleton);
        
        builder.Register<ISceneModel, SceneModel>(Lifetime.Singleton);
    }
}