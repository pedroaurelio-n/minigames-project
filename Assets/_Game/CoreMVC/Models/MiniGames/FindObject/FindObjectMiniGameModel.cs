using UnityEngine;

public class FindObjectMiniGameModel : BaseMiniGameModel, IFindObjectMiniGameModel
{
    //TODO pedro: move to options/settings
    public int BaseStartObjects => 10;

    public override MiniGameType Type => MiniGameType.FindObject;
    public override TouchInputType InputTypes => TouchInputType.TwoPointMove | TouchInputType.TwoPointZoom;
    public override string Instructions => "Find the colored object to win!";
    
    readonly ICameraProvider _cameraProvider;
    
    public FindObjectMiniGameModel(
        IMiniGameTimerModel miniGameTimerModel,
        ICameraProvider cameraProvider
    ) : base(miniGameTimerModel)
    {
        _cameraProvider = cameraProvider;
    }
}