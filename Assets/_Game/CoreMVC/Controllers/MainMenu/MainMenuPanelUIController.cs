using System;

public class MainMenuPanelUIController : IDisposable
{
    readonly IMainMenuModel _model;
    readonly MainMenuPanelUIView _view;
    readonly FadeToBlackManager _fadeToBlackManager;
    
    public MainMenuPanelUIController (
        IMainMenuModel model,
        MainMenuPanelUIView view,
        FadeToBlackManager fadeToBlackManager
    )
    {
        _model = model;
        _view = view;
        _fadeToBlackManager = fadeToBlackManager;
    }

    public void Initialize ()
    {
        AddListeners();
        AddViewListeners();
    }
    
    void Enable ()
    {
        _view.SetActive(true);
        _view.SetHighScoreText(_model.HighScore.ToString());
    }

    void Disable ()
    {
        _view.SetActive(false);
    }

    void AddListeners ()
    {
        _model.OnMainMenuStateChanged += HandleMainMenuStateChanged;
    }
    
    void RemoveListeners ()
    {
        _model.OnMainMenuStateChanged -= HandleMainMenuStateChanged;
    }

    void AddViewListeners ()
    {
        _view.OnPlayButtonClick += HandlePlayButtonClick;
        _view.OnLevelSelectButtonClick += HandleLevelSelectButtonClick;
    }
    
    void RemoveViewListeners ()
    {
        _view.OnPlayButtonClick -= HandlePlayButtonClick;
        _view.OnLevelSelectButtonClick -= HandleLevelSelectButtonClick;
    }

    void HandleMainMenuStateChanged (MainMenuState newState)
    {
        if (newState == MainMenuState.Menu)
        {
            Enable();
            return;
        }
        
        Disable();
    }

    void HandlePlayButtonClick ()
    {
        _fadeToBlackManager.FadeIn(() => _model.PlayGame(), true);
    }
    
    void HandleLevelSelectButtonClick ()
    {
        _model.ChangeMainMenuState(MainMenuState.LevelSelect);
    }
    
    public void Dispose ()
    {
        RemoveListeners();
        RemoveViewListeners();
    }
}