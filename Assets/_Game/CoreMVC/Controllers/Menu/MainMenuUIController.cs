using System;

public class MainMenuUIController : IDisposable
{
    readonly IMainMenuModel _model;
    readonly MainMenuUIView _view;
    readonly FadeToBlackManager _fadeToBlackManager;

    bool _initialized;
    
    public MainMenuUIController (
        IMainMenuModel model,
        MenuUIView view,
        FadeToBlackManager fadeToBlackManager
    )
    {
        _model = model;
        _view = view as MainMenuUIView;
        _fadeToBlackManager = fadeToBlackManager;

        _initialized = _view != null;
    }

    public void Initialize ()
    {
        if (!_initialized)
            return;
        
        AddViewListeners();
        SyncView();
    }
    
    void SyncView ()
    {
        _view.SetHighScoreText(_model.HighScore.ToString());
    }

    void AddViewListeners ()
    {
        _view.OnPlayButtonClick += HandlePlayButtonClick;
    }
    
    void RemoveViewListeners ()
    {
        _view.OnPlayButtonClick -= HandlePlayButtonClick;
    }

    void HandlePlayButtonClick ()
    {
        _fadeToBlackManager.FadeIn(() => _model.PlayGame(), true);
    }
    
    public void Dispose ()
    {
        if (!_initialized)
            return;
        RemoveViewListeners();
    }
}