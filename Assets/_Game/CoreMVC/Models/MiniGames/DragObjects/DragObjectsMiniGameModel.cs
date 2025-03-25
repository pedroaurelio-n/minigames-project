public class DragObjectsMiniGameModel : BaseMiniGameModel, IDragObjectsMiniGameModel
{
    //TODO pedro: move to settings or options
    public int BaseStartObjects => 3;
    
    public override MiniGameType Type => MiniGameType.DragObjects;
    public override TouchInputType InputTypes => TouchInputType.Drag;
    public override string Instructions => "Move the objects to their colored boxes!";

    readonly IDragModel _dragModel;
    
    public DragObjectsMiniGameModel (
        IMiniGameTimerModel miniGameTimerModel,
        IDragModel dragModel
    ) : base(miniGameTimerModel)
    {
        _dragModel = dragModel;
    }
}