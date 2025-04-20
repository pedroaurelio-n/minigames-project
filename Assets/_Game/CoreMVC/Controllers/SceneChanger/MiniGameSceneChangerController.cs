public class MiniGameSceneChangerController : BaseSceneChangerController
{
    IMiniGameSceneChangerModel MiniGameSceneChangerModel => Model as IMiniGameSceneChangerModel;
    
    readonly IMiniGameManagerModel _miniGameManagerModel;
    readonly IMiniGameSelectorModel _miniGameSelectorModel;
    readonly FadeToBlackManager _fadeToBlackManager;
    
    public MiniGameSceneChangerController (
        IMiniGameSceneChangerModel model,
        IMiniGameManagerModel miniGameManagerModel,
        IMiniGameSelectorModel miniGameSelectorModel,
        FadeToBlackManager fadeToBlackManager
    ) : base(model)
    {
        _miniGameManagerModel = miniGameManagerModel;
        _miniGameSelectorModel = miniGameSelectorModel;
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
        _fadeToBlackManager.FadeIn(
            () => MiniGameSceneChangerModel.ChangeToNewMiniGame(_miniGameSelectorModel.NextType),
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