public class JoystickAimMiniGameModel : BaseMiniGameModel, IJoystickAimMiniGameModel
{
    public int BaseObjectsToSpawn => CurrentLevelSettings.MilestoneCount.Value;
    
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