using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MenuCore : ICoreModule
{
    public event Action OnInitializationComplete;
    
    public IMenuModel MenuModel { get; private set; }
    public MenuController MenuController { get; private set; }
    public MenuUIController MenuUIController { get; private set; }
    
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
    readonly FirebaseManager _firebaseManager;
    
    MenuView _menuView;
    MenuUIView _menuUIView;
    
    LifetimeScope _menuScope;

    public MenuCore (
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
        ICoroutineRunner coroutineRunner,
        FirebaseManager firebaseManager
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
        _firebaseManager = firebaseManager;
    }
    
    public void Initialize ()
    {
        MenuCoreFactory.CreateScope(
            out _menuView,
            out _menuUIView,
            out _menuScope,
            _gameScope,
            _fadeToBlackManager,
            _poolableViewFactory,
            _gameSessionInfoProvider,
            _loadingManager,
            _playerInfoModel,
            _settingsManager,
            _randomProvider,
            _physicsProvider,
            _cameraProvider,
            _coroutineRunner,
            _firebaseManager
        );

        _cameraProvider.SetMainCamera(Camera.main);

        MenuModel = _menuScope.Container.Resolve<IMenuModel>();
        
        MenuController = _menuScope.Container.Resolve<MenuController>();
        
        MenuUIController = _menuScope.Container.Resolve<MenuUIController>();
        MenuUIController.Initialize();

        _fadeToBlackManager.FadeOut(null);
        OnInitializationComplete?.Invoke();
    }
    
    public void Dispose ()
    {
        _menuScope.Dispose();
    }
}