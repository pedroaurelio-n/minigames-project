public class MiniGameSceneChangerController : BaseSceneChangerController
{
    IMiniGameSceneChangerModel MiniGameSceneChangerModel => Model as IMiniGameSceneChangerModel;
    
    readonly IMiniGameTimerModel _miniGameTimerModel;
    readonly IMiniGameSelectorModel _miniGameSelectorModel;
    
    public MiniGameSceneChangerController (
        IMiniGameSceneChangerModel model,
        IMiniGameTimerModel miniGameTimerModel,
        IMiniGameSelectorModel miniGameSelectorModel,
        GameUIView gameUIView
    ) : base(model as ISceneChangerModel, gameUIView)
    {
        _miniGameTimerModel = miniGameTimerModel;
        _miniGameSelectorModel = miniGameSelectorModel;
    }
    
    protected override void AddListeners ()
    {
        _miniGameTimerModel.OnTimerEnded += HandleTimerEnded;
    }
    
    protected override void RemoveListeners ()
    {
        _miniGameTimerModel.OnTimerEnded -= HandleTimerEnded;
    }

    protected override void ChangeSceneClick () => ChangeToNextMiniGame();

    void HandleTimerEnded (bool _) => ChangeToRandomMiniGame();
    
    void ChangeToRandomMiniGame ()
    {
        if (IsChangingScene)
            return;
        
        IsChangingScene = true;
        GameUIView.FadeToBlackManager.FadeIn(() =>
            MiniGameSceneChangerModel.ChangeToNewMiniGame(_miniGameSelectorModel.NextType));
    }

    void ChangeToNextMiniGame ()
    {
        if (IsChangingScene)
            return;
        
        IsChangingScene = true;
        GameUIView.FadeToBlackManager.FadeIn(MiniGameSceneChangerModel.ChangeToNextMiniGame);
    }
}