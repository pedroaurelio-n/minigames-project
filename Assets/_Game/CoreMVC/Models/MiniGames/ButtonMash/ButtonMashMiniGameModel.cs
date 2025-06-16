public class ButtonMashMiniGameModel : BaseMiniGameModel, IButtonMashMiniGameModel
{
    public int ClickMilestone => _settings.BaseObjectiveMilestone.Value;
    
    public override MiniGameType Type => MiniGameType.ButtonMash;
    public override TouchInputType InputTypes => TouchInputType.None;
    
    public ButtonMashMiniGameModel (
        IMiniGameSettings settings,
        IMiniGameTimerModel miniGameTimerModel
    ) : base(settings, miniGameTimerModel)
    {
    }
}