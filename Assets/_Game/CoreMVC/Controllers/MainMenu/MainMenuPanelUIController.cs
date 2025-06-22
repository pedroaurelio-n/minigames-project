public class MainMenuPanelUIController : BaseMainMenuPanelUIController
{
    protected override MainMenuState State => MainMenuState.Menu;
    
    readonly MainMenuPanelUIView _view;
    readonly FadeToBlackManager _fadeToBlackManager;
    
    public MainMenuPanelUIController (
        IMainMenuModel model,
        MainMenuPanelUIView view,
        FadeToBlackManager fadeToBlackManager
    ) : base(model)
    {
        _view = view;
        _fadeToBlackManager = fadeToBlackManager;
    }
    
    public override void Initialize ()
    {
        base.Initialize();
        AddViewListeners();
    }
    
    protected override void Enable ()
    {
        _view.SetActive(true);
        _view.SetHighScoreText(Model.HighScore.ToString());
    }

    protected override void Disable ()
    {
        _view.SetActive(false);
    }

    void AddViewListeners ()
    {
        _view.OnPlayButtonClick += HandlePlayButtonClick;
        _view.OnLevelSelectButtonClick += HandleLevelSelectButtonClick;
        _view.OnStatisticsButtonClick += HandleStatisticsButtonClick;
    }
    
    void RemoveViewListeners ()
    {
        _view.OnPlayButtonClick -= HandlePlayButtonClick;
        _view.OnLevelSelectButtonClick -= HandleLevelSelectButtonClick;
        _view.OnStatisticsButtonClick -= HandleStatisticsButtonClick;
    }

    void HandlePlayButtonClick () => _fadeToBlackManager.FadeIn(() => Model.PlayGame(), true);

    void HandleLevelSelectButtonClick () => Model.ChangeMainMenuState(MainMenuState.LevelSelect);

    void HandleStatisticsButtonClick () => Model.ChangeMainMenuState(MainMenuState.Statistics);
    
    public override void Dispose ()
    {
        base.Dispose();
        RemoveViewListeners();
    }
}