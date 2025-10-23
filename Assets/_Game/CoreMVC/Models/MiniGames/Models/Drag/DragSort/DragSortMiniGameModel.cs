public class DragSortMiniGameModel : BaseMiniGameModel, IDragSortMiniGameModel
{
    public int BaseStartObjects => _settings.BaseObjectCount.Value;
    
    public override MiniGameType Type => MiniGameType.DragSort;
    public override TouchInputType InputTypes => TouchInputType.Drag;

    readonly IDragModel _dragModel;
    
    public DragSortMiniGameModel (
        IMiniGameSettings settings,
        IMiniGameTimerModel miniGameTimerModel,
        IDragModel dragModel
    ) : base(settings, miniGameTimerModel)
    {
        _dragModel = dragModel;
    }
}