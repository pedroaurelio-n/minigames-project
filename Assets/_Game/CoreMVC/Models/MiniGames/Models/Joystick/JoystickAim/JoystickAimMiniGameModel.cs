public class JoystickAimMiniGameModel : BaseMiniGameModel, IJoystickAimMiniGameModel
{
    public int BaseObjectsToSpawn => _Settings.BaseObjectCount.Value;
    
    public override MiniGameType Type => MiniGameType.JoystickAim;
    public override TouchInputType InputTypes => TouchInputType.None;
    
    public JoystickAimMiniGameModel (
        IMiniGameSettings settings,
        IMiniGameDifficultyModel miniGameDifficultyModel,
        IMiniGameTimerModel miniGameTimerModel
    ) : base(settings, miniGameDifficultyModel, miniGameTimerModel)
    {
    }
}