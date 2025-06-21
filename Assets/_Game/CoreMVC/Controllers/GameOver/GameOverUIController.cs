using System;

public class GameOverUIController : IDisposable
{
    readonly IGameOverModel _model;
    readonly GameOverUIView _view;
    readonly FadeToBlackManager _fadeToBlackManager;
    
    bool _initialized;
    
    public GameOverUIController (
        IGameOverModel model,
        MenuUIView view,
        FadeToBlackManager fadeToBlackManager
    )
    {
        _model = model;
        _view = view as GameOverUIView;
        _fadeToBlackManager = fadeToBlackManager;
        
        _initialized = _view != null;
    }

    public void Initialize ()
    {
        if (!_initialized)
            return;
        
        _model.RegisterHighScore();
        AddViewListeners();
        SyncView();
    }
    
    void SyncView ()
    {
        _view.SetLivesText(_model.Lives.ToString());
        _view.SetScoreText(_model.Score.ToString());
        _view.SetHighScoreText(_model.HighScore.ToString());
    }

    void AddViewListeners ()
    {
        _view.OnRestartButtonClick += HandleRestartButtonClick;
        _view.OnMenuButtonClick += HandleMenuButtonClick;
    }
    
    void RemoveViewListeners ()
    {
        _view.OnRestartButtonClick -= HandleRestartButtonClick;
        _view.OnMenuButtonClick -= HandleMenuButtonClick;
    }

    void HandleRestartButtonClick ()
    {
        _fadeToBlackManager.FadeIn(() => _model.RestartGame(), true);
    }
    
    void HandleMenuButtonClick ()
    {
        _fadeToBlackManager.FadeIn(() => _model.ReturnToMenu(), true);
    }
    
    public void Dispose ()
    {
        if (!_initialized)
            return;
        RemoveViewListeners();
    }
}