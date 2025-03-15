using UnityEngine;

public class TouchInputController
{
    readonly ITouchInputModel _touchInputModel;
    readonly TapInputView _tapInputView;
    readonly SwipeInputView _swipeInputView;
    readonly TapInputOptions _tapInputOptions;
    readonly SwipeInputOptions _swipeInputOptions;
    
    float _startTapTime;
    Vector2 _startTapPosition;

    float _startSwipeTime;
    Vector2 _startSwipePosition;
    
    public TouchInputController (
        ITouchInputModel touchInputModel,
        TapInputView tapInputView,
        SwipeInputView swipeInputView
    )
    {
        _touchInputModel = touchInputModel;
        _tapInputView = tapInputView;
        _swipeInputView = swipeInputView;

        _tapInputOptions = GameGlobalOptions.Instance.TapInputOptions;
        _swipeInputOptions = GameGlobalOptions.Instance.SwipeInputOptions;
    }

    public void Initialize ()
    {
        AddListeners();
    }

    void AddListeners ()
    {
        _tapInputView.OnTapBegan += HandleTapBegan;
        _tapInputView.OnTapEnded += HandleTapEnded;

        _swipeInputView.OnSwipeBegan += HandleSwipeBegan;
        _swipeInputView.OnSwipeEnded += HandleSwipeEnded;
    }

    void RemoveListeners ()
    {
        //TODO pedro: 
    }

    void HandleTapBegan (Touch touch)
    {
        _startTapTime = Time.time;
        _startTapPosition = touch.position;
    }

    void HandleTapEnded (Touch touch)
    {
        float duration = Time.time - _startTapTime;
        float distanceSquared = (touch.position - _startTapPosition).sqrMagnitude;

        if (duration > _tapInputOptions.TimeThreshold
            || distanceSquared > _tapInputOptions.MovementThreshold * _tapInputOptions.MovementThreshold)
            return;

        _touchInputModel.PerformTap(touch.position);
    }

    void HandleSwipeBegan (Touch touch)
    {
        _startSwipeTime = Time.time;
        _startSwipePosition = touch.position;
    }

    void HandleSwipeEnded (Touch touch)
    {
        float duration = Time.time - _startSwipeTime;
        Vector2 endPosition = touch.position;
        Vector2 delta = endPosition - _startSwipePosition;

        if (duration > _swipeInputOptions.TimeThreshold
            || delta.sqrMagnitude < _swipeInputOptions.MovementThreshold * _swipeInputOptions.MovementThreshold)
            return;
        
        _touchInputModel.PerformSwipe(delta, _swipeInputOptions.DiagonalThreshold);
    }
}