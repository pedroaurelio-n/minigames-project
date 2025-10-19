public class ButtonStopwatchMiniGameModel : BaseMiniGameModel, IButtonStopwatchMiniGameModel
{
    public int MaxTries => _settings.BaseObjectiveMilestone.Value;
    
    public override MiniGameType Type => MiniGameType.ButtonStopwatch;
    public override TouchInputType InputTypes => TouchInputType.None;
    
    public ButtonStopwatchMiniGameModel (
        IMiniGameSettings settings,
        IMiniGameTimerModel miniGameTimerModel
    ) : base(settings, miniGameTimerModel)
    {
    }
}