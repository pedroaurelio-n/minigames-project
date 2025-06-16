using System;
using Newtonsoft.Json.Linq;
using UnityEngine;
using VContainer.Unity;

public class GameCore : ICoreModule
{
    public event Action OnInitializationComplete;

    public ISceneModel SceneModel { get; private set; }
    public SceneController SceneController { get; private set; }
    public SceneUIController SceneUIController { get; private set; }

    readonly LifetimeScope _gameScope;
    readonly IGameSessionInfoProvider _gameSessionInfoProvider;
    readonly ILoadingManager _loadingManager;
    readonly IPlayerInfoModel _playerInfoModel;
    readonly FadeToBlackManager _fadeToBlackManager;
    readonly PoolableViewFactory _poolableViewFactory;
    readonly SettingsManager _settingsManager;
    readonly IRandomProvider _randomProvider;
    readonly IPhysicsProvider _physicsProvider;
    readonly ICameraProvider _cameraProvider;
    readonly ICoroutineRunner _coroutineRunner;
    
    SceneView _sceneView;
    SceneUIView _sceneUIView;
    
    LifetimeScope _viewScope;
    LifetimeScope _uiViewScope;
    LifetimeScope _modelScope;
    LifetimeScope _controllerScope;
    LifetimeScope _uiControllerScope;

    public GameCore (
        LifetimeScope gameScope,
        IGameSessionInfoProvider gameSessionInfoProvider,
        ILoadingManager loadingManager,
        IPlayerInfoModel playerInfoModel,
        FadeToBlackManager fadeToBlackManager,
        PoolableViewFactory poolableViewFactory,
        SettingsManager settingsManager,
        IRandomProvider randomProvider,
        IPhysicsProvider physicsProvider,
        ICameraProvider cameraProvider,
        ICoroutineRunner coroutineRunner
    )
    {
        _gameScope = gameScope;
        _gameSessionInfoProvider = gameSessionInfoProvider;
        _loadingManager = loadingManager;
        _playerInfoModel = playerInfoModel;
        _fadeToBlackManager = fadeToBlackManager;
        _poolableViewFactory = poolableViewFactory;
        _settingsManager = settingsManager;
        _randomProvider = randomProvider;
        _physicsProvider = physicsProvider;
        _cameraProvider = cameraProvider;
        _coroutineRunner = coroutineRunner;
    }

    public void Initialize ()
    {
        SceneViewsFactory.CreateScopes(
            out _sceneView,
            out _sceneUIView,
            out _viewScope,
            out _uiViewScope,
            _gameScope,
            _fadeToBlackManager,
            _poolableViewFactory,
            _gameSessionInfoProvider
        );

        _cameraProvider.SetMainCamera(Camera.main);

        InitializeSettings();

        SceneModel = SceneModelFactory.CreateScope(
            out _modelScope,
            _uiViewScope,
            _loadingManager,
            _playerInfoModel,
            _gameSessionInfoProvider,
            _settingsManager,
            _randomProvider,
            _physicsProvider,
            _cameraProvider,
            _coroutineRunner
        );
        SceneModel.Initialize();
        
        SceneController = SceneControllerFactory.CreateScope(out _controllerScope, _modelScope);
        SceneController.Initialize();
        
        SceneUIController = SceneUIControllerFactory.CreateScope(out _uiControllerScope, _controllerScope);
        SceneUIController.Initialize();

        SceneModel.LateInitialize();
        SceneController.LateInitialize();
        SceneUIController.LateInitialize();

        _fadeToBlackManager.FadeOut(null);
        OnInitializationComplete?.Invoke();
    }

    void InitializeSettings ()
    {
        _settingsManager.MiniGameSettingsProvider.SelectSettings((int)_gameSessionInfoProvider.CurrentMiniGameType);
    }

    public void Dispose ()
    {
        _uiControllerScope.Dispose();
        _controllerScope.Dispose();
        _modelScope.Dispose();
        
        _uiViewScope.Dispose();
        
        _sceneView.Dispose();
        _viewScope.Dispose();
    }
}