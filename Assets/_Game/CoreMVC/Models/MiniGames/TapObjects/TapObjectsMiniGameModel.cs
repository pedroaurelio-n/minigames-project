public class TapObjectsMiniGameModel : BaseMiniGameModel, ITapObjectsMiniGameModel
{
    public override MiniGameType Type => MiniGameType.TapObjects;
    public override TouchInputType InputTypes => TouchInputType.Tap;
    public override string Instructions => "Tap all objects to win!";

    public TapObjectsMiniGameModel (
        IMiniGameTimerModel miniGameTimerModel
    ) : base(miniGameTimerModel)
    {
    }
}