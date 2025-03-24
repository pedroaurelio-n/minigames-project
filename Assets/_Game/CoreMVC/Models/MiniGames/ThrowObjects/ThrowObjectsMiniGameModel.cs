using System;
using UnityEngine;

public class ThrowObjectsMiniGameModel : BaseMiniGameModel, IThrowObjectsMiniGameModel
{
    public event Action<Vector3> OnSwipePerformed;
    
    public override MiniGameType Type => MiniGameType.ThrowObjects;
    public override TouchInputType InputTypes => TouchInputType.Swipe;
    public override string Instructions => "Score a point to win!";

    readonly ITouchInputModel _touchInputModel;
    
    public ThrowObjectsMiniGameModel (
        IMiniGameTimerModel miniGameTimerModel,
        ITouchInputModel touchInputModel
    ) : base(miniGameTimerModel)
    {
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
        Vector3 swipeDirection = ((Vector3)rawDirection * 5f / Screen.dpi) + _touchInputModel.MainCamera.transform.forward * 25f;
        OnSwipePerformed?.Invoke(swipeDirection);
    }
}