public class ButtonStopwatchMiniGameModel : BaseMiniGameModel, IButtonStopwatchMiniGameModel
{
    public int MaxTries => _Settings.BaseObjectiveMilestone.Value;
    
    public override MiniGameType Type => MiniGameType.ButtonStopwatch;
    public override TouchInputType InputTypes => TouchInputType.None;
    
    public ButtonStopwatchMiniGameModel (
        IMiniGameSettings settings,
        IMiniGameDifficultyModel miniGameDifficultyModel,
        IMiniGameTimerModel miniGameTimerModel
    ) : base(settings, miniGameDifficultyModel, miniGameTimerModel)
    {
    }
}