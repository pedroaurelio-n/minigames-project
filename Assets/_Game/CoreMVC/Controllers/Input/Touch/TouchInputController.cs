using UnityEngine;

public class TouchInputController
{
    readonly ITouchInputModel _touchInputModel;
    readonly TapInputView _tapInputView;
    readonly SwipeInputView _swipeInputView;
    readonly LongPressInputView _longPressInputView;
    
    readonly TapInputOptions _tapInputOptions;
    readonly SwipeInputOptions _swipeInputOptions;
    readonly LongPressInputOptions _longPressInputOptions;
    
    float _startTapTime;
    Vector2 _startTapPosition;

    float _startSwipeTime;
    Vector2 _startSwipePosition;
    
    float _startLongPressTime;
    Vector2 _startLongPressPosition;
    bool _longPressCancelled;
    
    public TouchInputController (
        ITouchInputModel touchInputModel,
        TapInputView tapInputView,
        SwipeInputView swipeInputView,
        LongPressInputView longPressInputView
    )
    {
        _touchInputModel = touchInputModel;
        _tapInputView = tapInputView;
        _swipeInputView = swipeInputView;
        _longPressInputView = longPressInputView;

        _tapInputOptions = GameGlobalOptions.Instance.TapInputOptions;
        _swipeInputOptions = GameGlobalOptions.Instance.SwipeInputOptions;
        _longPressInputOptions = GameGlobalOptions.Instance.LongPressInputOptions;
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
        
        _longPressInputView.OnLongPressBegan += HandleLongPressBegan;
        _longPressInputView.OnLongPressUpdated += HandleLongPressUpdated;
        _longPressInputView.OnLongPressEnded += HandleLongPressEnded;
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

        if (duration > _tapInputOptions.MaxTimeThreshold
            || distanceSquared > _tapInputOptions.MaxMovementThreshold * _tapInputOptions.MaxMovementThreshold)
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

        if (duration > _swipeInputOptions.MaxTimeThreshold
            || delta.sqrMagnitude < _swipeInputOptions.MinMovementThreshold * _swipeInputOptions.MinMovementThreshold)
            return;
        
        _touchInputModel.PerformSwipe(delta, _swipeInputOptions.DiagonalThreshold);
    }
    
    void HandleLongPressBegan (Touch touch)
    {
        _startLongPressTime = Time.time;
        _startLongPressPosition = touch.position;
        _longPressCancelled = false;
    }
    
    void HandleLongPressUpdated (Touch touch)
    {
        float distanceSquared = (touch.position - _startLongPressPosition).sqrMagnitude;

        if (_touchInputModel.LongPressStarted)
        {
            if (distanceSquared > _longPressInputOptions.MaxMovementCancelThreshold
                * _longPressInputOptions.MaxMovementCancelThreshold)
            {
                _touchInputModel.CancelLongPress(touch.position);
                _longPressCancelled = true;
            }
            return;
        }
        
        float duration = Time.time - _startLongPressTime;
        if (duration < _longPressInputOptions.MinTimeThreshold
            || distanceSquared > _longPressInputOptions.MaxMovementStartThreshold
            * _longPressInputOptions.MaxMovementStartThreshold)
            return;
        
        _touchInputModel.StartLongPress(touch.position);
    }

    void HandleLongPressEnded (Touch touch)
    {
        if (_longPressCancelled)
            return;
        
        float duration = Time.time - _startLongPressTime;
        if (duration < _longPressInputOptions.MinTimeThreshold)
            return;
        
        if (!_touchInputModel.LongPressStarted)
        {
            _touchInputModel.CancelLongPress(touch.position);
            return;
        }

        float distanceSquared = (touch.position - _startLongPressPosition).sqrMagnitude;
        if (distanceSquared > _longPressInputOptions.MaxMovementCancelThreshold
            * _longPressInputOptions.MaxMovementCancelThreshold)
            return;

        _touchInputModel.EndLongPress(touch.position);
    }
}