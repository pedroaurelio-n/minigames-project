using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

public class GameCore : IDisposable
{
    public event Action OnInitializationComplete;

    public IGameModel GameModel { get; private set; }
    public GameController GameController { get; private set; }
    public GameUIView GameUIView { get; private set; }
    
    public SceneView SceneView { get; private set; }

    readonly LifetimeScope _gameScope;
    readonly IGameSessionInfoProvider _gameSessionInfoProvider;
    readonly ILoadingManager _loadingManager;
    readonly IPlayerInfoModel _playerInfoModel;
    readonly SettingsManager _settingsManager;
    readonly IRandomProvider _randomProvider;
    readonly IPhysicsProvider _physicsProvider;
    readonly ICoroutineRunner _coroutineRunner;

    LifetimeScope _coreScope;

    public GameCore (
        LifetimeScope gameScope,
        IGameSessionInfoProvider gameSessionInfoProvider,
        ILoadingManager loadingManager,
        IPlayerInfoModel playerInfoModel,
        SettingsManager settingsManager,
        IRandomProvider randomProvider,
        IPhysicsProvider physicsProvider,
        ICoroutineRunner coroutineRunner
    )
    {
        _gameScope = gameScope;
        _gameSessionInfoProvider = gameSessionInfoProvider;
        _loadingManager = loadingManager;
        _playerInfoModel = playerInfoModel;
        _settingsManager = settingsManager;
        _randomProvider = randomProvider;
        _physicsProvider = physicsProvider;
        _coroutineRunner = coroutineRunner;
    }

    public void Initialize ()
    {
        _coreScope = CreateGameScope();

        GameModel = _coreScope.Container.Resolve<IGameModel>();
        GameModel.Initialize();
    
        GameController = _coreScope.Container.Resolve<GameController>();
        GameController.Initialize();

        GameModel.LateInitialize();
        GameController.LateInitialize();

        GameUIView.FadeToBlackManager.FadeOut(null);
        OnInitializationComplete?.Invoke();
    }

    LifetimeScope CreateGameScope ()
    {
        //TODO pedro: don't recreate persistent ui view
        GameUIView = Object.Instantiate(Resources.Load<GameUIView>("GameUIView"));

        SceneView = Object.Instantiate(Resources.Load<SceneView>($"{_gameSessionInfoProvider.CurrentScene}View"));
        SceneView.Initialize();
    
        //TODO pedro: don't recreate disabled pool transform parent
        UIViewFactory uiViewFactory = new();
        
        GameInstaller installer = new(
            _loadingManager,
            _playerInfoModel,
            _gameSessionInfoProvider,
            GameUIView,
            SceneView,
            uiViewFactory,
            _settingsManager,
            _randomProvider,
            _physicsProvider,
            _coroutineRunner
        );
        
        return _gameScope.CreateChild(installer, $"CoreScope");
    }

    public void Dispose ()
    {
        _coreScope.Dispose();
    }
}