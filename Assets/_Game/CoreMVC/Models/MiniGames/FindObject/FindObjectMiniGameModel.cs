using UnityEngine;

public class FindObjectMiniGameModel : BaseMiniGameModel, IFindObjectMiniGameModel
{
    public int BaseStartObjects => _settings.BaseObjectCount.Value;

    public override MiniGameType Type => MiniGameType.FindObject;
    public override TouchInputType InputTypes => TouchInputType.TwoPointMove | TouchInputType.TwoPointZoom;
    
    readonly ICameraMoveModel _cameraMoveModel;
    readonly ITouchInputModel _touchInputModel;
    readonly FindObjectMiniGameOptions _options;
    
    public FindObjectMiniGameModel(
        IMiniGameSettings settings,
        IMiniGameTimerModel miniGameTimerModel,
        ICameraMoveModel cameraMoveModel,
        ITouchInputModel touchInputModel,
        FindObjectMiniGameOptions options
    ) : base(settings, miniGameTimerModel)
    {
        _cameraMoveModel = cameraMoveModel;
        _touchInputModel = touchInputModel;
        _options = options;
    }
    
    protected override void AddListeners()
    {
        base.AddListeners();
        _touchInputModel.OnTwoPointZoomPerformed += HandleTwoPointZoomPerformed;
        _touchInputModel.OnTwoPointMovePerformed += HandleTwoPointMovePerformed;
    }
    
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        _touchInputModel.OnTwoPointZoomPerformed -= HandleTwoPointZoomPerformed;
        _touchInputModel.OnTwoPointMovePerformed -= HandleTwoPointMovePerformed;
    }
    
    void HandleTwoPointZoomPerformed(float difference)
    {
        _cameraMoveModel.ZoomCamera(_options.ZoomSpeed * difference, _options.MinCameraZoom, _options.MaxCameraZoom);
    }

    void HandleTwoPointMovePerformed(Vector2 deltaPosition)
    {
        _cameraMoveModel.MoveCamera(deltaPosition * _options.RotationSpeed);
    }
}