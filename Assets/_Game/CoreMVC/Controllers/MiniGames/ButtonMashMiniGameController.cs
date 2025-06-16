public class ButtonMashMiniGameController : BaseMiniGameController
{
    protected override MiniGameType MiniGameType => MiniGameType.ButtonMash;
    
    IButtonMashMiniGameModel MiniGameModel => _miniGameManagerModel.ActiveMiniGame as IButtonMashMiniGameModel;
    ButtonMashMiniGameUIController MiniGameUIController => UIController as ButtonMashMiniGameUIController;
    
    readonly IMiniGameManagerModel _miniGameManagerModel;

    int _count;

    public ButtonMashMiniGameController (
        IMiniGameManagerModel miniGameManagerModel,
        SceneView sceneView,
        SceneUIView sceneUIView
    ) : base(miniGameManagerModel, sceneView, sceneUIView)
    {
        _miniGameManagerModel = miniGameManagerModel;
    }

    public override void Initialize ()
    {
        if (_miniGameManagerModel.ActiveMiniGameType != MiniGameType)
            return;

        base.Initialize();
    }

    protected override void SetupMiniGame ()
    {
        UIController = new ButtonMashMiniGameUIController();
        base.SetupMiniGame();
        
        AddUIListeners();
        MiniGameUIController.SyncView(_count, MiniGameModel.ClickMilestone);
    }

    protected override bool CheckWinCondition (bool timerEnded)
    {
        return _count >= MiniGameModel.ClickMilestone;
    }

    protected override bool CheckFailCondition ()
    {
        return _count < MiniGameModel.ClickMilestone;
    }

    void AddUIListeners ()
    {
        MiniGameUIController.OnLeftButtonClick += HandleLeftButtonClick;
        MiniGameUIController.OnRightButtonClick += HandleRightButtonClick;
    }
    
    void RemoveUIListeners ()
    {
        MiniGameUIController.OnLeftButtonClick -= HandleLeftButtonClick;
        MiniGameUIController.OnRightButtonClick -= HandleRightButtonClick;
    }

    void HandleLeftButtonClick ()
    {
        _count++;
        MiniGameUIController.SyncView(_count, MiniGameModel.ClickMilestone);
        
        if (CheckWinCondition(false))
            MiniGameModel.Complete();
    }

    void HandleRightButtonClick ()
    {
        MiniGameModel.ForceFailure();
    }

    public override void Dispose ()
    {
        if (UIController != null)
            RemoveUIListeners();
        
        base.Dispose();
    }
}