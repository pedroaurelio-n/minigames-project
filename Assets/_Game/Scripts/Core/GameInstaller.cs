using VContainer;
using VContainer.Unity;

public class GameInstaller : IInstaller
{
    readonly ILoadingManager _loadingManager;
    readonly IPlayerInfoModel _playerInfoModel;
    readonly IGameSessionInfoProvider _gameSessionInfoProvider;
    readonly GameUIView _gameUIView;
    readonly SceneView _sceneView;
    readonly PoolableViewFactory _poolableViewFactory;
    readonly SettingsManager _settingsManager;
    readonly IRandomProvider _randomProvider;
    readonly IPhysicsProvider _physicsProvider;
    readonly ICameraProvider _cameraProvider;
    readonly ICoroutineRunner _coroutineRunner;
    
    public GameInstaller (
        ILoadingManager loadingManager,
        IPlayerInfoModel playerInfoModel,
        IGameSessionInfoProvider gameSessionInfoProvider,
        GameUIView gameUIView,
        SceneView sceneView,
        PoolableViewFactory poolableViewFactory,
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
        _gameUIView = gameUIView;
        _sceneView = sceneView;
        _poolableViewFactory = poolableViewFactory;
        _settingsManager = settings;
        _randomProvider = randomProvider;
        _physicsProvider = physicsProvider;
        _cameraProvider = cameraProvider;
        _coroutineRunner = coroutineRunner;
    }
    
    public void Install (IContainerBuilder builder)
    {
        // builder.RegisterInstance(_settingsManager.CardListSettings.Instance);
        
        builder.RegisterInstance(_loadingManager);
        builder.RegisterInstance(_playerInfoModel);
        builder.RegisterInstance(_gameSessionInfoProvider);
        builder.RegisterInstance(_randomProvider);
        builder.RegisterInstance(_physicsProvider);
        builder.RegisterInstance(_cameraProvider);
        builder.RegisterInstance(_coroutineRunner);
        
        builder.RegisterInstance(_gameUIView);
        builder.RegisterInstance(_sceneView);
        builder.RegisterInstance(_sceneView.MouseInput);
        builder.RegisterInstance(_sceneView.TapInputView);
        builder.RegisterInstance(_sceneView.SwipeInputView);
        builder.RegisterInstance(_sceneView.LongPressInputView);
        builder.RegisterInstance(_sceneView.TwoPointMoveInputView);
        builder.RegisterInstance(_sceneView.TwoPointZoomInputView);
        builder.RegisterInstance(_sceneView.TouchDragInputView);
        builder.RegisterInstance(_poolableViewFactory);

        builder.RegisterInstance(GameGlobalOptions.Instance.LayerMaskOptions);
        builder.RegisterInstance(GameGlobalOptions.Instance.FadeTransitionOptions);
        builder.RegisterInstance(GameGlobalOptions.Instance.InputOptions.TapInputOptions);
        builder.RegisterInstance(GameGlobalOptions.Instance.InputOptions.SwipeInputOptions);
        builder.RegisterInstance(GameGlobalOptions.Instance.InputOptions.LongPressInputOptions);
        builder.RegisterInstance(GameGlobalOptions.Instance.InputOptions.TwoPointMoveInputOptions);
        builder.RegisterInstance(GameGlobalOptions.Instance.InputOptions.TwoPointZoomInputOptions);
        builder.RegisterInstance(GameGlobalOptions.Instance.MiniGameOptions);

        //TODO pedro: delete mouse input classes
        // builder.Register<IMouseInputModel, MouseInputModel>(Lifetime.Scoped);
        builder.Register<ITouchInputModel, TouchInputModel>(Lifetime.Scoped);
        builder.Register<IDragModel, DragModel>(Lifetime.Scoped);
        builder.Register<IPressModel, PressModel>(Lifetime.Scoped);

        builder.Register<ICameraMoveModel, CameraMoveModel>(Lifetime.Scoped);
        
        builder.Register<ISceneChangerModel, SceneChangerModel>(Lifetime.Scoped);

        builder.Register<IMiniGameTimerModel, MiniGameTimerModel>(Lifetime.Scoped);
        builder.Register<IMiniGameModelFactory, MiniGameModelFactory>(Lifetime.Scoped);
        builder.Register<IMiniGameManagerModel, MiniGameManagerModel>(Lifetime.Scoped);
        
        builder.Register<IGameModel, GameModel>(Lifetime.Scoped);

        // builder.Register<MouseInputController>(Lifetime.Scoped);
        builder.Register<TouchInputController>(Lifetime.Scoped);
        
        builder.Register<SceneChangerUIController>(Lifetime.Scoped);

        builder.Register<TapObjectsMiniGameController>(Lifetime.Scoped);
        builder.Register<ThrowObjectsMiniGameController>(Lifetime.Scoped);
        builder.Register<DragObjectsMiniGameController>(Lifetime.Scoped);
        builder.Register<FindObjectMiniGameController>(Lifetime.Scoped);
        
        builder.Register<GameController>(Lifetime.Scoped);
    }
}
