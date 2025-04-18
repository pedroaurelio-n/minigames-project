public class MiniGameSceneChangerController : BaseSceneChangerController
{
    IMiniGameSceneChangerModel MiniGameSceneChangerModel => Model as IMiniGameSceneChangerModel;
    
    readonly IMiniGameManagerModel _miniGameManagerModel;
    readonly IMiniGameSelectorModel _miniGameSelectorModel;
    
    public MiniGameSceneChangerController (
        IMiniGameSceneChangerModel model,
        IMiniGameManagerModel miniGameManagerModel,
        IMiniGameSelectorModel miniGameSelectorModel,
        SceneUIView sceneUIView
    ) : base(model as ISceneChangerModel, sceneUIView)
    {
        _miniGameManagerModel = miniGameManagerModel;
        _miniGameSelectorModel = miniGameSelectorModel;
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
        SceneUIView.FadeToBlackManager.FadeIn(() =>
            MiniGameSceneChangerModel.ChangeToNewMiniGame(_miniGameSelectorModel.NextType));
    }

    void ChangeToNextMiniGame ()
    {
        if (IsChangingScene)
            return;
        
        IsChangingScene = true;
        SceneUIView.FadeToBlackManager.FadeIn(MiniGameSceneChangerModel.ChangeToNextMiniGame);
    }
}