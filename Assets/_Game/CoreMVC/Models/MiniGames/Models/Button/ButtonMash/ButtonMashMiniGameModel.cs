public class ButtonMashMiniGameModel : BaseMiniGameModel, IButtonMashMiniGameModel
{
    public int ClickMilestone => CurrentLevelSettings.MilestoneCount.Value;
    
    public override MiniGameType Type => MiniGameType.ButtonMash;
    public override TouchInputType InputTypes => TouchInputType.None;
    
    public ButtonMashMiniGameModel (
        IMiniGameSettings settings,
        IMiniGameDifficultyModel miniGameDifficultyModel,
        IMiniGameTimerModel miniGameTimerModel
    ) : base(settings, miniGameDifficultyModel, miniGameTimerModel)
    {
    }
}