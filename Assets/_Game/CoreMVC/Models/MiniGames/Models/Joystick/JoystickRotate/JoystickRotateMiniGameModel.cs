public class JoystickRotateMiniGameModel : BaseMiniGameModel, IJoystickRotateMiniGameModel
{
    public override MiniGameType Type => MiniGameType.JoystickRotate;
    public override TouchInputType InputTypes => TouchInputType.None;
    
    public JoystickRotateMiniGameModel (
        IMiniGameSettings settings,
        IMiniGameDifficultyModel miniGameDifficultyModel,
        IMiniGameTimerModel miniGameTimerModel
    ) : base(settings, miniGameDifficultyModel, miniGameTimerModel)
    {
    }
}