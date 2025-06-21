using System;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;

//TODO pedro: maybe separate GameSession into various parts (IGameScopeInitializer, ISceneNavigator, ICoreFactoryCreator, etc)
public class GameSession : IGameSessionInfoProvider, IDisposable
{
    public event Action OnInitializationComplete;
    
    public string CurrentScene { get; private set; }
    //TODO pedro: maybe not expose set property
    public MiniGameType CurrentMiniGameType { get; set; }
    public MiniGameType NextMiniGameType { get; set; }
    public bool HasStartedGameRun { get; set; }

    public IPlayerInfoModel PlayerInfoModel { get; private set; }

    GameLifetimeScope MainScope => GameLifetimeScope.Instance;

    readonly GameVersion _gameVersion;
    readonly ILoadingManager _loadingManager;
    
    LifetimeScope _gameScope;

    ICoreModule _currentCore;
    
    FadeToBlackManager _fadeToBlackManager;
    PoolableViewFactory _poolableViewFactory;
    
    SettingsManager _settingsManager;
    IRandomProvider _randomProvider;
    IPhysicsProvider _physicsProvider;
    ICameraProvider _cameraProvider;
    IDateTimeProvider _dateTimeProvider;
    CoroutineRunner _coroutineRunner;
    
    IPersistenceModel _persistenceModel;
    
    public GameSession (
        GameVersion gameVersion,
        ILoadingManager loadingManager,
        string startScene
    )
    {
        _gameVersion = gameVersion;
        _loadingManager = loadingManager;
        CurrentScene = startScene;
    }

    public void Initialize ()
    {
        _gameScope = MainScope.CreateChild(childScopeName: "GameScope");
        
        CreateProviders();

        _persistenceModel = new PersistenceModel(_gameVersion, _dateTimeProvider);

        PlayerInfoModel = new PlayerInfoModel(
            _persistenceModel.Data,
            _settingsManager.PlayerSettings.Instance,
            this
        );

        _fadeToBlackManager = Object.Instantiate(
            Resources.Load<FadeToBlackManager>("FadeToBlackManager"),
            _gameScope.transform
        );
        
        GameObject poolParent = new("ViewFactory");
        poolParent.transform.SetParent(_gameScope.transform);
        _poolableViewFactory = new PoolableViewFactory(poolParent.transform);
        
        CreateCore();
    }

    public void ChangeScene (string newScene)
    {
        CurrentScene = newScene;
        CreateCore();
    }
    
    void CreateProviders ()
    {
        _settingsManager = new SettingsManager();
        _randomProvider = new RandomProvider();
        _physicsProvider = new PhysicsProvider();
        _cameraProvider = new CameraProvider();
        _dateTimeProvider = new DateTimeProvider();

        _coroutineRunner = new GameObject("CoroutineRunner").AddComponent<CoroutineRunner>();
        _coroutineRunner.transform.SetParent(_gameScope.transform);
    }

    void CreateCore ()
    {
        if (CurrentScene.StartsWith(SceneManagerUtils.MiniGameScenePrefix))
            CreateGameCore();
        else
            CreateMenuCore();
    }

    void CreateGameCore ()
    {
        _currentCore = new GameCore(
            _gameScope,
            _persistenceModel,
            this,
            _loadingManager,
            PlayerInfoModel,
            _fadeToBlackManager,
            _poolableViewFactory,
            _settingsManager,
            _randomProvider,
            _physicsProvider,
            _cameraProvider,
            _dateTimeProvider,
            _coroutineRunner
        );
        _currentCore.OnInitializationComplete += HandleCoreInitializationComplete;
        _currentCore.Initialize();
    }

    void CreateMenuCore ()
    {
        CurrentMiniGameType = MiniGameType.None;
        
        _currentCore = new MenuCore(
            _gameScope,
            _persistenceModel,
            this,
            _loadingManager,
            PlayerInfoModel,
            _fadeToBlackManager,
            _poolableViewFactory,
            _settingsManager,
            _randomProvider,
            _physicsProvider,
            _cameraProvider,
            _dateTimeProvider,
            _coroutineRunner
        );
        _currentCore.OnInitializationComplete += HandleCoreInitializationComplete;
        _currentCore.Initialize();
    }

    void HandleCoreInitializationComplete ()
    {
        _currentCore.OnInitializationComplete -= HandleCoreInitializationComplete;
        OnInitializationComplete?.Invoke();
    }

    public void Dispose ()
    {
        _currentCore?.Dispose();
        _currentCore = null;
    }
}
