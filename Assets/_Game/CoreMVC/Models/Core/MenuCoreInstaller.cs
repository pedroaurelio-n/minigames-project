using VContainer;
using VContainer.Unity;

public class MenuCoreInstaller : IInstaller
{
    readonly MenuView _mainMenuView;
    readonly MenuUIView _mainMenuUIView;
    readonly PoolableViewFactory _poolableViewFactory;
    readonly FadeToBlackManager _fadeToBlackManager;
    readonly IGameSessionInfoProvider _gameSessionInfoProvider;
    readonly ILoadingManager _loadingManager;
    readonly IPlayerInfoModel _playerInfoModel;
    readonly SettingsManager _settingsManager;
    readonly IRandomProvider _randomProvider;
    readonly IPhysicsProvider _physicsProvider;
    readonly ICameraProvider _cameraProvider;
    readonly ICoroutineRunner _coroutineRunner;
    
    public MenuCoreInstaller (
        MenuView mainMenuView,
        MenuUIView mainMenuUIView,
        PoolableViewFactory poolableViewFactory,
        FadeToBlackManager fadeToBlackManager,
        IGameSessionInfoProvider gameSessionInfoProvider,
        ILoadingManager loadingManager,
        IPlayerInfoModel playerInfoModel,
        SettingsManager settingsManager,
        IRandomProvider randomProvider,
        IPhysicsProvider physicsProvider,
        ICameraProvider cameraProvider,
        ICoroutineRunner coroutineRunner
    )
    {
        _mainMenuView = mainMenuView;
        _mainMenuUIView = mainMenuUIView;
        _poolableViewFactory = poolableViewFactory;
        _fadeToBlackManager = fadeToBlackManager;
        _gameSessionInfoProvider = gameSessionInfoProvider;
        _loadingManager = loadingManager;
        _playerInfoModel = playerInfoModel;
        _settingsManager = settingsManager;
        _randomProvider = randomProvider;
        _physicsProvider = physicsProvider;
        _cameraProvider = cameraProvider;
        _coroutineRunner = coroutineRunner;
    }
    
    public void Install (IContainerBuilder builder)
    {
        builder.RegisterInstance(_mainMenuView);
        builder.RegisterInstance(_mainMenuUIView);
        builder.RegisterInstance(_poolableViewFactory);
        builder.RegisterInstance(_fadeToBlackManager);
        builder.RegisterInstance(_gameSessionInfoProvider);
        builder.RegisterInstance(_loadingManager);
        builder.RegisterInstance(_playerInfoModel);
        builder.RegisterInstance(_settingsManager);
        builder.RegisterInstance(_randomProvider);
        builder.RegisterInstance(_physicsProvider);
        builder.RegisterInstance(_cameraProvider);
        builder.RegisterInstance(_coroutineRunner);
        
        builder.RegisterInstance(_settingsManager.MiniGameSettingsProvider);
        
        builder.RegisterInstance(_settingsManager.MiniGameSystemSettings.Instance);

        builder.Register<IMenuSceneChangerModel, MenuSceneChangerModel>(Lifetime.Singleton);
        builder.Register<IMainMenuModel, MainMenuModel>(Lifetime.Singleton);
        builder.Register<IGameOverModel, GameOverModel>(Lifetime.Singleton);
        
        builder.Register<IMenuModel, MenuModel>(Lifetime.Singleton);

        builder.Register<MenuSceneChangerController>(Lifetime.Singleton);
        builder.Register<MainMenuController>(Lifetime.Singleton);
        builder.Register<GameOverController>(Lifetime.Singleton);
        
        builder.Register<MainMenuUIController>(Lifetime.Singleton);
        builder.Register<GameOverUIController>(Lifetime.Singleton);
        
        builder.Register<MenuController>(Lifetime.Singleton);
        builder.Register<MenuUIController>(Lifetime.Singleton);
    }
}