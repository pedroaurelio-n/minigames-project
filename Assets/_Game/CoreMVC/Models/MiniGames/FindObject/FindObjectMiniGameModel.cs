public class FindObjectMiniGameModel : BaseMiniGameModel, IFindObjectMiniGameModel
{
    public int BaseStartObjects => _settings.BaseObjectCount.Value;

    public override MiniGameType Type => MiniGameType.FindObject;
    public override TouchInputType InputTypes => TouchInputType.TwoPointMove | TouchInputType.TwoPointZoom;
    
    //TODO pedro: maybe do some camera logic here? (and other input logic on mini game models)
    readonly ICameraProvider _cameraProvider;
    
    public FindObjectMiniGameModel(
        IMiniGameSettings settings,
        IMiniGameTimerModel miniGameTimerModel,
        ICameraProvider cameraProvider
    ) : base(settings, miniGameTimerModel)
    {
        _cameraProvider = cameraProvider;
    }
}