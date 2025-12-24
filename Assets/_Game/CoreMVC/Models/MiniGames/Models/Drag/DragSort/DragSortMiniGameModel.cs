public class DragSortMiniGameModel : BaseMiniGameModel, IDragSortMiniGameModel
{
    public int BaseStartObjects => CurrentLevelSettings.MilestoneCount.Value;
    
    public override MiniGameType Type => MiniGameType.DragSort;
    public override TouchInputType InputTypes => TouchInputType.Drag;

    readonly IDragModel _dragModel;
    
    public DragSortMiniGameModel (
        IMiniGameSettings settings,
        IMiniGameDifficultyModel miniGameDifficultyModel,
        IMiniGameTimerModel miniGameTimerModel,
        IDragModel dragModel
    ) : base(settings, miniGameDifficultyModel, miniGameTimerModel)
    {
        _dragModel = dragModel;
    }
}