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
    
    public override void ChangeSceneClick () => ChangeToNextMiniGame();
    
    protected override void AddListeners ()
    {
        _miniGameManagerModel.OnMiniGameChange += HandleMiniGameChange;
    }
    
    protected override void RemoveListeners ()
    {
        _miniGameManagerModel.OnMiniGameChange -= HandleMiniGameChange;
    }
    
    void HandleMiniGameChange () => ChangeToRandomMiniGame();
    
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

    void ChangeToNextMiniGame ()
    {
        if (IsChangingScene)
            return;
        
        IsChangingScene = true;
        _fadeToBlackManager.FadeIn(MiniGameSceneChangerModel.ChangeToNextMiniGame, true);
    }
}