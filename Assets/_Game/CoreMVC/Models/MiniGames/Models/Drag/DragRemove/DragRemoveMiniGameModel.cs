public class DragRemoveMiniGameModel : BaseMiniGameModel, IDragRemoveMiniGameModel
{
    public int BaseStartObjects => _Settings.BaseObjectCount.Value;
    
    public override MiniGameType Type => MiniGameType.DragRemove;
    public override TouchInputType InputTypes => TouchInputType.Drag;

    //TODO pedro: evaluate this model events/properties/methods
    readonly IDragModel _dragModel;
    
    public DragRemoveMiniGameModel (
        IMiniGameSettings settings,
        IMiniGameDifficultyModel miniGameDifficultyModel,
        IMiniGameTimerModel miniGameTimerModel,
        IDragModel dragModel
    ) : base(settings, miniGameDifficultyModel, miniGameTimerModel)
    {
        _dragModel = dragModel;
    }
}