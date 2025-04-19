public class ClickMilestoneMiniGameModel : BaseMiniGameModel, IClickMilestoneMiniGameModel
{
    //TODO pedro: move to settings or options
    public int ClickMilestone => 30;
    
    public override MiniGameType Type => MiniGameType.ClickMilestone;
    public override TouchInputType InputTypes => TouchInputType.None;
    public override string Instructions => "Click until you reach the milestone!";
    
    public ClickMilestoneMiniGameModel (
        IMiniGameTimerModel miniGameTimerModel
    ) : base(miniGameTimerModel)
    {
    }
}