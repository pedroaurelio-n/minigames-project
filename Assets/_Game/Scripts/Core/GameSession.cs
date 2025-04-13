using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

public class GameSession : IGameSessionInfoProvider, IDisposable
{
    public event Action OnInitializationComplete;
    
    public string CurrentScene { get; private set; }
    public int CurrentSceneIndex => SceneManager.GetSceneByName(CurrentScene).buildIndex;
    //TODO pedro: maybe not expose set property
    public MiniGameType CurrentMiniGameType { get; set; }
    
    public IPlayerInfoModel PlayerInfoModel { get; private set; }

    GameLifetimeScope MainScope => GameLifetimeScope.Instance;

    readonly ILoadingManager _loadingManager;
    
    LifetimeScope _gameScope;
    
    GameCore _gameCore;
    
    SettingsManager _settingsManager;
    IRandomProvider _randomProvider;
    IPhysicsProvider _physicsProvider;
    ICameraProvider _cameraProvider;
    CoroutineRunner _coroutineRunner;
    
    public GameSession (
        ILoadingManager loadingManager,
        string startScene
    )
    {
        _loadingManager = loadingManager;
        CurrentScene = startScene;
    }

    public void Initialize ()
    {
        _gameScope = MainScope.CreateChild(childScopeName: "GameScope");
        
        CreateProviders();

        PlayerInfoModel = new PlayerInfoModel();
        PlayerInfoModel.Initialize();
        
        CreateGameCore();
    }

    public void ChangeScene (string newScene)
    {
        CurrentScene = newScene;
        CreateGameCore();
    }
    
    void CreateProviders ()
    {
        _settingsManager = new SettingsManager();
        _randomProvider = new RandomProvider();
        _physicsProvider = new PhysicsProvider();
        _cameraProvider = new CameraProvider();

        _coroutineRunner = new GameObject("CoroutineRunner").AddComponent<CoroutineRunner>();
        _coroutineRunner.transform.SetParent(_gameScope.transform);
    }

    void CreateGameCore ()
    {
        _gameCore = new GameCore(
            _gameScope,
            this,
            _loadingManager,
            PlayerInfoModel,
            _settingsManager,
            _randomProvider,
            _physicsProvider,
            _cameraProvider,
            _coroutineRunner
        );
        _gameCore.OnInitializationComplete += HandleCoreInitializationComplete;
        _gameCore.Initialize();
    }

    void HandleCoreInitializationComplete ()
    {
        _gameCore.OnInitializationComplete -= HandleCoreInitializationComplete;
        OnInitializationComplete?.Invoke();
    }

    public void Dispose ()
    {
        _gameCore.Dispose();
    }
}
