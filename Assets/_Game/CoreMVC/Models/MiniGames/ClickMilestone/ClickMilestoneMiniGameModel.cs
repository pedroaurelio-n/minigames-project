public class ClickMilestoneMiniGameModel : BaseMiniGameModel, IClickMilestoneMiniGameModel
{
    public int ClickMilestone => _settings.BaseObjectiveMilestone.Value;
    
    public override MiniGameType Type => MiniGameType.ClickMilestone;
    public override TouchInputType InputTypes => TouchInputType.None;
    
    public ClickMilestoneMiniGameModel (
        IMiniGameSettings settings,
        IMiniGameTimerModel miniGameTimerModel
    ) : base(settings, miniGameTimerModel)
    {
    }
}