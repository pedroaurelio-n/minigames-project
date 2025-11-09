public class JoystickAimMiniGameModel : BaseMiniGameModel, IJoystickAimMiniGameModel
{
    public int BaseObjectsToSpawn => _settings.BaseObjectCount.Value;
    
    public override MiniGameType Type => MiniGameType.JoystickAim;
    public override TouchInputType InputTypes => TouchInputType.None;
    
    public JoystickAimMiniGameModel (
        IMiniGameSettings settings,
        IMiniGameTimerModel miniGameTimerModel
    ) : base(settings, miniGameTimerModel)
    {
    }
}