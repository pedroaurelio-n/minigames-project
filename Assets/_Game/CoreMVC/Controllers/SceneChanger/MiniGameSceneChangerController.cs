public class MiniGameSceneChangerController : BaseSceneChangerController
{
    IMiniGameSceneChangerModel MiniGameSceneChangerModel => Model as IMiniGameSceneChangerModel;
    
    readonly IMiniGameTimerModel _miniGameTimerModel;
    readonly IMiniGameSelectorModel _miniGameSelectorModel;
    
    public MiniGameSceneChangerController (
        IMiniGameSceneChangerModel model,
        IMiniGameTimerModel miniGameTimerModel,
        IMiniGameSelectorModel miniGameSelectorModel,
        SceneUIView sceneUIView
    ) : base(model as ISceneChangerModel, sceneUIView)
    {
        _miniGameTimerModel = miniGameTimerModel;
        _miniGameSelectorModel = miniGameSelectorModel;
    }
    
    public override void ChangeSceneClick () => ChangeToNextMiniGame();
    
    protected override void AddListeners ()
    {
        _miniGameTimerModel.OnTimerEnded += HandleTimerEnded;
    }
    
    protected override void RemoveListeners ()
    {
        _miniGameTimerModel.OnTimerEnded -= HandleTimerEnded;
    }
    
    void HandleTimerEnded (bool _) => ChangeToRandomMiniGame();
    
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