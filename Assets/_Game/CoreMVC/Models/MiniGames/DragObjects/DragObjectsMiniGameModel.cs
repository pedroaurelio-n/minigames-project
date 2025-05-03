public class DragObjectsMiniGameModel : BaseMiniGameModel, IDragObjectsMiniGameModel
{
    public int BaseStartObjects => _settings.BaseObjectCount.Value;
    
    public override MiniGameType Type => MiniGameType.DragObjects;
    public override TouchInputType InputTypes => TouchInputType.Drag;

    readonly IDragModel _dragModel;
    
    public DragObjectsMiniGameModel (
        IMiniGameSettings settings,
        IMiniGameTimerModel miniGameTimerModel,
        IDragModel dragModel
    ) : base(settings, miniGameTimerModel)
    {
        _dragModel = dragModel;
    }
}