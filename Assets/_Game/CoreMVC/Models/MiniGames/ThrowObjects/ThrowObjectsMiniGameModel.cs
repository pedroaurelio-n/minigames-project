using System;
using UnityEngine;

public class ThrowObjectsMiniGameModel : BaseMiniGameModel, IThrowObjectsMiniGameModel
{
    public event Action<Vector3, Vector3> OnSwipePerformed;
    
    public override MiniGameType Type => MiniGameType.ThrowObjects;
    public override TouchInputType InputTypes => TouchInputType.Swipe;

    readonly ICameraProvider _cameraProvider;
    readonly ITouchInputModel _touchInputModel;
    
    public ThrowObjectsMiniGameModel (
        IMiniGameSettings settings,
        IMiniGameTimerModel miniGameTimerModel,
        ICameraProvider cameraProvider,
        ITouchInputModel touchInputModel
    ) : base(settings, miniGameTimerModel)
    {
        _cameraProvider = cameraProvider;
        _touchInputModel = touchInputModel;
    }

    protected override void AddListeners ()
    {
        base.AddListeners();
        _touchInputModel.OnSwipePerformed += HandleSwipePerformed;
    }

    protected override void RemoveListeners ()
    {
        base.RemoveListeners();
        _touchInputModel.OnSwipePerformed -= HandleSwipePerformed;
    }

    void HandleSwipePerformed (
        Vector2 startPosition,
        Vector2 endPosition,
        Vector2 normalizedDirection,
        Vector2 rawDirection,
        float duration
    )
    {
        OnSwipePerformed?.Invoke(rawDirection, _cameraProvider.MainCamera.transform.forward);
    }
}