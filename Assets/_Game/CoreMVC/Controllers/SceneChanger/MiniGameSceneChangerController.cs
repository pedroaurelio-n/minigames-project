public class MiniGameSceneChangerController : BaseSceneChangerController
{
    IMiniGameSceneChangerModel MiniGameSceneChangerModel => Model as IMiniGameSceneChangerModel;
    
    readonly IMiniGameManagerModel _miniGameManagerModel;
    readonly IGameSessionInfoProvider _gameSessionInfoProvider;
    readonly FadeToBlackManager _fadeToBlackManager;
    
    public MiniGameSceneChangerController (
        IMiniGameSceneChangerModel model,
        IMiniGameManagerModel miniGameManagerModel,
        IGameSessionInfoProvider gameSessionInfoProvider,
        FadeToBlackManager fadeToBlackManager
    ) : base(model)
    {
        _miniGameManagerModel = miniGameManagerModel;
        _gameSessionInfoProvider = gameSessionInfoProvider;
        _fadeToBlackManager = fadeToBlackManager;
    }
    
    protected override void AddListeners ()
    {
        _miniGameManagerModel.OnMiniGameChanged += HandleMiniGameChanged;
        _miniGameManagerModel.OnSingleMiniGameChange += HandleSingleMiniGameChange;
    }
    
    protected override void RemoveListeners ()
    {
        _miniGameManagerModel.OnMiniGameChanged -= HandleMiniGameChanged;
        _miniGameManagerModel.OnSingleMiniGameChange -= HandleSingleMiniGameChange;
    }
    
    void HandleMiniGameChanged () => ChangeToRandomMiniGame();
    
    void HandleSingleMiniGameChange () => GoToMainMenu();
    
    void ChangeToRandomMiniGame ()
    {
        if (IsChangingScene)
            return;
        
        IsChangingScene = true;
        _gameSessionInfoProvider.CurrentMiniGameType = _gameSessionInfoProvider.NextMiniGameType;
        _fadeToBlackManager.FadeIn(
            () => MiniGameSceneChangerModel.ChangeToNewMiniGame(_gameSessionInfoProvider.CurrentMiniGameType),
            true
        );
    }

    void GoToMainMenu ()
    {
        if (IsChangingScene)
            return;
        
        IsChangingScene = true;
        _fadeToBlackManager.FadeIn(() => MiniGameSceneChangerModel.ChangeToMainMenu(), true);
    }
}