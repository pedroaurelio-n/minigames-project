using System;

public class MainMenuUIController : IDisposable
{
    readonly IMainMenuModel _model;
    readonly MainMenuUIView _view;

    readonly MainMenuPanelUIController _mainMenuPanelUIController;
    readonly LevelSelectPanelUIController _levelSelectPanelUIController;

    bool _initialized;
    
    public MainMenuUIController (
        IMainMenuModel model,
        MenuUIView view,
        FadeToBlackManager fadeToBlackManager,
        PoolableViewFactory viewFactory,
        IMiniGameSystemSettings miniGameSystemSettings,
        IMiniGameSettingsAccessor miniGameSettingsAccessor
    )
    {
        _model = model;
        _view = view as MainMenuUIView;
        
        _initialized = _view != null;

        if (!_initialized)
            return;

        _mainMenuPanelUIController = new MainMenuPanelUIController(
            model,
            _view.MainMenuPanelUIView,
            fadeToBlackManager
        );
        _levelSelectPanelUIController = new LevelSelectPanelUIController(
            model,
            _view.LevelSelectPanelUIView,
            fadeToBlackManager,
            viewFactory,
            miniGameSystemSettings,
            miniGameSettingsAccessor       
        );
    }

    public void Initialize ()
    {
        if (!_initialized)
            return;
        
        _mainMenuPanelUIController.Initialize();
        _levelSelectPanelUIController.Initialize();
        
        _model.ChangeMainMenuState(MainMenuState.Menu);
    }
    
    public void Dispose ()
    {
        if (!_initialized)
            return;
        _mainMenuPanelUIController.Dispose();
        _levelSelectPanelUIController.Dispose();
    }
}