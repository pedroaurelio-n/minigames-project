using UnityEngine;

public class MoveFindMiniGameModel : BaseMiniGameModel, IMoveFindMiniGameModel
{
    public int BaseStartObjects => _Settings.BaseObjectCount.Value;

    public override MiniGameType Type => MiniGameType.MoveFind;
    public override TouchInputType InputTypes => TouchInputType.TwoPointMove | TouchInputType.TwoPointZoom;
    
    readonly ICameraMoveModel _cameraMoveModel;
    readonly ITouchInputModel _touchInputModel;
    readonly MoveFindMiniGameOptions _options;
    
    public MoveFindMiniGameModel(
        IMiniGameSettings settings,
        IMiniGameDifficultyModel miniGameDifficultyModel,
        IMiniGameTimerModel miniGameTimerModel,
        ICameraMoveModel cameraMoveModel,
        ITouchInputModel touchInputModel,
        MoveFindMiniGameOptions options
    ) : base(settings, miniGameDifficultyModel, miniGameTimerModel)
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